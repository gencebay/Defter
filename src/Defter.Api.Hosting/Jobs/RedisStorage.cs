﻿using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Defter.Api.Hosting
{
    internal class RedisStorage
    {
        private const string SetScript = (@"
                redis.call('HMSET', KEYS[1], 'type', ARGV[1], 'data', ARGV[2])
                return 1");

        private const string HmGetScript = (@"return redis.call('HMGET', KEYS[1], unpack(ARGV))");

        private const string DelScript = (@"return redis.call('del', unpack(redis.call('keys', ARGV[1])))");

        private volatile ConnectionMultiplexer _connection;
        private IDatabase _redisDatabase;

        private const string DataKey = "data";
        private const string TypeKey = "type";
        private readonly RedisStorageOptions _options;
        private readonly string _instance;
        private static readonly ConcurrentDictionary<string, Assembly> _cache = new ConcurrentDictionary<string, Assembly>();

        private readonly SemaphoreSlim _connectionLock = new SemaphoreSlim(initialCount: 1, maxCount: 1);
        protected List<Assembly> AssemblyList { get; }

        public RedisStorage(IOptions<RedisStorageOptions> optionsAccessor, AssemblyOptions assemblyOptions)
        {
            if (optionsAccessor == null)
            {
                throw new ArgumentNullException(nameof(optionsAccessor));
            }

            if (assemblyOptions == null)
            {
                throw new ArgumentNullException(nameof(assemblyOptions));
            }


            _options = optionsAccessor.Value;

            if (string.IsNullOrEmpty(_options.InstanceName))
            {
                throw new ArgumentNullException(nameof(_options.InstanceName));
            }

            _instance = _options.InstanceName + ":";
            AssemblyList = assemblyOptions.AssemblyList;
        }

        private Type TryGetType(string fullname)
        {
            if(_cache.TryGetValue(fullname, out Assembly assembly))
            {
                return assembly.GetType(fullname, false);
            }

            foreach (var item in AssemblyList)
            {
                Type type = item.GetType(fullname, false);
                if (type != null)
                {
                    if (!_cache.ContainsKey(fullname))
                    {
                        _cache.TryAdd(fullname, item);
                    }

                    return type;
                }
            }

            throw new ArgumentOutOfRangeException(fullname);
        }

        private RedisValue[] GetRedisMembers(params string[] members)
        {
            var redisMembers = new RedisValue[members.Length];
            for (int i = 0; i < members.Length; i++)
            {
                redisMembers[i] = (RedisValue)members[i];
            }

            return redisMembers;
        }

        private RedisValue[] HashMemberGet(string key, params string[] members)
        {
            var result = _redisDatabase.ScriptEvaluate(
                HmGetScript,
                new RedisKey[] { key },
                GetRedisMembers(members));

            return (RedisValue[])result;
        }

        private async Task<RedisValue[]> HashMemberGetAsync(string key, params string[] members)
        {
            var result = await _redisDatabase.ScriptEvaluateAsync(
                HmGetScript,
                new RedisKey[] { key },
                GetRedisMembers(members));

            return (RedisValue[])result;
        }
        
        public StorageResult Get(string key)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            Connect();

            RedisValue[] results = HashMemberGet(key, TypeKey, DataKey);

            if (results.Length >= 2 && results[1].HasValue)
            {
                return new StorageResult
                {
                    Type = results[0],
                    Data = results[1]
                };
            }

            return null;
        }

        public async Task<StorageResult> GetAsync(string key)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }


            await ConnectAsync();

            RedisValue[] results = await HashMemberGetAsync(_instance + key, TypeKey, DataKey);

            if (results.Length >= 2 && results[1].HasValue)
            {
                return new StorageResult
                {
                    Type = results[0],
                    Data = results[1]
                };
            }

            return null;
        }

        public void Set(string key, object instance)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (instance == null)
            {
                throw new ArgumentNullException(nameof(instance));
            }

            Connect();

            byte[] value = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(instance));

            var result = _redisDatabase.ScriptEvaluate(SetScript, new RedisKey[] { _instance + key },
               new RedisValue[]
               {
                        instance.GetType().FullName,
                        value
               });
        }

        public async Task SetAsync(string key, object instance)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (instance == null)
            {
                throw new ArgumentNullException(nameof(instance));
            }

            await ConnectAsync();

            byte[] value = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(instance));

            await _redisDatabase.ScriptEvaluateAsync(SetScript, new RedisKey[] { _instance + key },
               new RedisValue[]
               {
                        instance.GetType().FullName,
                        value
               });
        }

        private void Connect()
        {
            if (_connection != null)
            {
                return;
            }

            _connectionLock.Wait();
            try
            {
                if (_connection == null)
                {
                    _connection = ConnectionMultiplexer.Connect(_options.Configuration);
                    _redisDatabase = _connection.GetDatabase();
                }
            }
            finally
            {
                _connectionLock.Release();
            }
        }

        private async Task ConnectAsync()
        {
            if (_connection != null)
            {
                return;
            }

            await _connectionLock.WaitAsync();
            try
            {
                if (_connection == null)
                {
                    _connection = await ConnectionMultiplexer.ConnectAsync(_options.Configuration);
                    _redisDatabase = _connection.GetDatabase();
                }
            }
            finally
            {
                _connectionLock.Release();
            }
        }

        public void Remove(string key)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            Connect();

            _redisDatabase.KeyDelete(_instance + key);
        }

        public async Task RemoveAsync(string key)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            await ConnectAsync();

            await _redisDatabase.KeyDeleteAsync(_instance + key);
        }

        public async Task RemoveKeysAsync(IEnumerable<IJob> jobs)
        {
            if (jobs != null && jobs.Any())
            {
                foreach (var item in jobs)
                {
                    await RemoveAsync(item.Id);
                }
            }
        }

        public async Task RemoveAllKeysAsync()
        {
            await ConnectAsync();

            var list = new List<string>();
            await _redisDatabase.ScriptEvaluateAsync(DelScript, values: new RedisValue[] { _instance + "*" });
        }

        public async Task<List<string>> GetAllKeysAsync()
        {
            Connect();
            var list = new List<string>();
            var keysScript = @"return redis.call('KEYS', ARGV[1])";
            var result = await _redisDatabase.ScriptEvaluateAsync(keysScript, values: new RedisValue[] { _instance + "*" });
            if (result.IsNull)
                return list;

            try
            {
                var values = (RedisValue[])result;
                return values.Select(p => (string)p).ToList();
            }
            catch (Exception)
            {
            }

            return list;
        }

        public async Task<List<IJob>> GetJobsAsync()
        {
            List<IJob> jobs = new List<IJob>();
            var keys = await GetAllKeysAsync();
            if (keys != null && keys.Any())
            {
                foreach (var key in keys)
                {
                    StorageResult redisResult = Get(key);
                    if (redisResult != null)
                    {
                        var type = TryGetType(redisResult.Type);
                        var job = (IJob)JsonConvert.DeserializeObject((string)redisResult.Data, type);
                        jobs.Add(job);
                    }
                }
            }

            return jobs;
        }

        public async Task<List<T>> GetTypedJobsAsync<T>() where T : IJob
        {
            List<T> jobs = new List<T>();
            var keys = await GetAllKeysAsync();
            if (keys != null && keys.Any())
            {
                var assembly = typeof(T).Assembly;
                foreach (var key in keys)
                {
                    StorageResult redisResult = Get(key);
                    if (redisResult != null)
                    {
                        var fullname = redisResult.Type;
                        var typedEvent = typeof(T).FullName;

                        if (fullname == typedEvent)
                        {
                            var type = assembly.GetType(redisResult.Type, false);
                            var job = (T)JsonConvert.DeserializeObject((string)redisResult.Data, type);
                            jobs.Add(job);
                        }
                    }
                }
            }

            return jobs;
        }

        public void Dispose()
        {
            if (_connection != null)
            {
                _connection.Close();
            }
        }
    }
}
