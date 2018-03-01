using System;
using System.Threading.Tasks;

namespace Defter.SharedLibrary
{
    public class BackgroundTask : IBackgroundTask
    {
        private readonly ThirdSecondThrottler _throttler;
        private readonly Func<TaskContext, Task> _task;

        public BackgroundTask(ThirdSecondThrottler throttler, Func<TaskContext, Task> task)
        {
            _throttler = throttler;
            _task = task;
        }

        public void Invoke(TaskContext context)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));

            _throttler.Throttle(context.CancellationToken);

            Task.WhenAll(_task.Invoke(context));

            _throttler.Delay(context.CancellationToken);
        }
    }
}