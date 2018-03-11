using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Defter.Api.Hosting
{
    internal class ProcessServer : IBackgroundTask, IDisposable
    {
        private readonly IList<IBackgroundTask> _processes;
        private readonly IServiceProvider _serviceProvider;
        private readonly ILoggerFactory _loggerFactory;        
        private readonly JobBuilderOptions _options;
        private readonly Task _bootstrapTask;
        private readonly CancellationTokenSource _cts = new CancellationTokenSource();
        public static readonly TimeSpan DefaultShutdownTimeout = TimeSpan.FromSeconds(15);

        private static void TrySetThreadName(string name)
        {
            try
            {
                Thread.CurrentThread.Name = name;
            }
            catch (InvalidOperationException)
            {
            }
        }

        private IBackgroundTask WrapProcess(IBackgroundTask process)
        {
            return new InfiniteLoopProcess(process);
        }

        private string GetGloballyUniqueServerId()
        {
            var serverName = Environment.GetEnvironmentVariable("COMPUTERNAME") ?? 
                Environment.GetEnvironmentVariable("HOSTNAME");

            var guid = Guid.NewGuid().ToString();

            return !String.IsNullOrWhiteSpace(serverName)
                ? $"{serverName.ToLowerInvariant()}:{guid}"
                : guid;
        }

        public ProcessServer(IServiceProvider serviceProvider, 
            JobBuilderOptions options,
            ILoggerFactory loggerFactory)
        {            
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(_serviceProvider));
            _options = options ?? throw new ArgumentNullException(nameof(options));
            _loggerFactory = loggerFactory ?? throw new ArgumentNullException(nameof(loggerFactory));

            var properties = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);

            var cronJobDictionary = new ConcurrentDictionary<string, ProcessableCronJob>(
                _options.JobList.Where(j => j.Cron != null && j.ThrottlerType == null)
                .ToDictionary(p => p.Id, p => new ProcessableCronJob(p)));

            var customThrottlerJobDictionary = new ConcurrentDictionary<string, ProcessableCronJob>(
                _options.JobList.Where(j => j.ThrottlerType != null && j.Cron == null)
                .ToDictionary(p => p.Id, p => new ProcessableCronJob(p)));

            _processes = new List<IBackgroundTask>
            {
                new CronJobScheduler(_serviceProvider, new EveryMinuteThrottler(), ScheduleInstant.Factory, cronJobDictionary, _loggerFactory)
            };

            foreach (KeyValuePair<string, ProcessableCronJob> entry in customThrottlerJobDictionary)
            {
                var throttler = (IThrottler)serviceProvider.GetService(entry.Value.ThrottlerType);
                var invoker = (IJob)serviceProvider.GetService(entry.Value.Type);
                _processes.Add(new CustomJobScheduler(serviceProvider, throttler, invoker.InvokeAsync));
            }

            var context = new TaskContext(
               GetGloballyUniqueServerId(),
               properties,
               _cts.Token);

            _bootstrapTask = WrapProcess(this).CreateTask(_loggerFactory, context);
        }

        public void Invoke(TaskContext context)
        {
            try
            {
                var tasks = _processes
                    .Select(WrapProcess)
                    .Select(process => process.CreateTask(_loggerFactory, context))
                    .ToArray();

                Task.WaitAll(tasks);
            }
            finally
            {
                
            }
        }

        public void SendStop()
        {
            _cts.Cancel();
        }

        public void Dispose()
        {
            SendStop();

            if (!_bootstrapTask.Wait(DefaultShutdownTimeout))
            {
                var logger = _loggerFactory.CreateLogger(typeof(ProcessServer));
                logger.LogWarning("Processing server takes too long to shutdown. Performing ungraceful shutdown.");
            }
        }

        public override string ToString()
        {
            return GetType().Name;
        }
    }
}
