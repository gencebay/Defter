using Microsoft.Extensions.Caching.Redis;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Defter.SharedLibrary
{
    public class RedisDomainEventStorage
    {
        private static readonly string _keyPrefix = "DomainEvent";
        private static int _redisPort = 6379;

        private static readonly Assembly _assembly = typeof(DomainEventBase).GetTypeInfo().Assembly;

        protected IOptions<RedisCacheOptions> RedisCacheOptions { get; }

        public RedisDomainEventStorage()
        {
            RedisCacheOptions = Options.Create(new RedisCacheOptions
            {
                Configuration = $"localhost:{_redisPort}",
                InstanceName = _keyPrefix
            });
        }

        private RedisManager GetStorage()
        {
            return new RedisManager(RedisCacheOptions);
        }

        public async Task SaveEventAsync(IDomainEventIdentity domainEvent)
        {
            using (var storage = GetStorage())
            {
                await storage.SetAsync(domainEvent.Id, domainEvent);
            }
        }

        public async Task<List<T>> GetTypedEventsAsync<T>() where T : IDomainEventIdentity
        {
            List<T> events = new List<T>();
            using (var storage = GetStorage())
            {
                var keys = await storage.GetAllKeysAsync();
                if (keys != null && keys.Any())
                {
                    foreach (var key in keys)
                    {
                        RedisStorageResult redisResult = storage.Get(key);
                        if (redisResult != null)
                        {
                            var fullname = redisResult.Type;
                            var typedEvent = typeof(T).FullName;

                            if (fullname == typedEvent)
                            {
                                var type = _assembly.GetType(fullname, false);
                                var domainEvent = (T)JsonConvert.DeserializeObject(redisResult.Data, type);
                                events.Add(domainEvent);
                            }
                        }
                    }
                }
            }

            return events;
        }

        public async Task<List<IDomainEventIdentity>> GetEventsAsync()
        {
            List<IDomainEventIdentity> events = new List<IDomainEventIdentity>();
            using (var storage = GetStorage())
            {
                var keys = await storage.GetAllKeysAsync();
                if (keys != null && keys.Any())
                {
                    foreach (var key in keys)
                    {
                        RedisStorageResult redisResult = storage.Get(key);
                        if (redisResult != null)
                        {
                            var type = _assembly.GetType(redisResult.Type, false);
                            var domainEvent = (IDomainEventIdentity)JsonConvert.DeserializeObject(redisResult.Data, type);
                            events.Add(domainEvent);
                        }
                    }
                }
            }

            return events;
        }

        public async Task RemoveAllEvents()
        {
            using (var storage = GetStorage())
            {
                await storage.RemoveAllKeysAsync();
            }
        }

        public async Task RemoveEvents(IEnumerable<IDomainEventIdentity> domainEvents)
        {
            using (var storage = GetStorage())
            {
                await storage.RemoveKeysAsync(domainEvents);
            }
        }
    }
}
