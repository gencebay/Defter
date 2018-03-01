using Defter.SharedLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Defter.Api.Hosting
{
    public class ProcessServer : IBackgroundTask
    {
        List<IBackgroundTask> _processes;
        private readonly InMemoryMessageQueue _messageQueue;

        public ProcessServer(InMemoryMessageQueue messageQueue, CancellationToken cancellationToken)
        {
            _processes = new List<IBackgroundTask>
            {
                new BackgroundTask(new ThirdSecondThrottler(), messageQueue.Dequeue)
            };

            var context = new TaskContext { CancellationToken = cancellationToken };
            CreateTask(WrapProcess(this), context);
            _messageQueue = messageQueue;
        }

        private IBackgroundTask WrapProcess(IBackgroundTask process)
        {
            return new InfiniteLoopTask(process);
        }

        private void RunProcess(IBackgroundTask process, TaskContext context)
        {
            try
            {
                Console.WriteLine($"Background process '{process}' started.");
                process.Invoke(context);
            }
            catch (Exception)
            {
            }

            Console.WriteLine($"Background process '{process}' stopped.");
        }

        public void Invoke(TaskContext context)
        {
            try
            {
                var tasks = _processes
                    .Select(WrapProcess)
                    .Select(process => CreateTask(process, context))
                    .ToArray();

                Task.WaitAll(tasks);
            }
            finally
            {

            }
        }

        public Task CreateTask(IBackgroundTask process, TaskContext context)
        {
            if (process == null) throw new ArgumentNullException(nameof(process));

            if (!(process is IBackgroundTask))
            {
                throw new ArgumentOutOfRangeException(nameof(process), "Long-running process must be of type IBackgroundTask.");
            }

            return Task.Factory.StartNew(
                () => RunProcess(process, context),
                TaskCreationOptions.LongRunning);
        }
    }
}
