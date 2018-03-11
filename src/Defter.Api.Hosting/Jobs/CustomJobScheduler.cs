using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace Defter.Api.Hosting
{
    public class CustomJobScheduler : IBackgroundTask
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IThrottler _throttler;
        private readonly Func<TaskContext, Task> _func;

        public CustomJobScheduler(IServiceProvider serviceProvider,
            IThrottler throttler,
            Func<TaskContext, Task> func)
        {
            _serviceProvider = serviceProvider;
            _throttler = throttler;
            _func = func;
        }

        private async Task RunAsync(ProcessableCronJob job, TaskContext context)
        {
            using (var scopedContext = _serviceProvider.CreateScope())
            {
                var provider = scopedContext.ServiceProvider;
                var cronJob = (IJob)provider.GetService(job.Type);
                try
                {
                    await cronJob.InvokeAsync(context);
                }
                catch (Exception ex)
                {
                }
            }
        }

        public void Invoke(TaskContext context)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));

            _throttler.Throttle(context.CancellationToken);
            
            Task.WhenAll(_func.Invoke(context));

            _throttler.Delay(context.CancellationToken);
        }
    }
}
