using System;

namespace Defter.SharedLibrary
{
    public class InfiniteLoopTask : IBackgroundProcessWrapper
    {
        public InfiniteLoopTask(IBackgroundTask innerProcess)
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