using System;
using System.Threading.Tasks;

namespace Defter.Api.Hosting
{
    internal class InfiniteLoopProcess : IBackgroundProcessWrapper
    {
        public InfiniteLoopProcess(IBackgroundTask innerProcess)
        {
            InnerProcess = innerProcess ?? throw new ArgumentNullException(nameof(innerProcess));
        }

        public IBackgroundTask InnerProcess { get; }

        public void Invoke(TaskContext context)
        {
            while (!context.IsShutdownRequested)
            {
                InnerProcess.Invoke(context);
            }
        }

        public override string ToString()
        {
            return InnerProcess.ToString();
        }
    }
}
