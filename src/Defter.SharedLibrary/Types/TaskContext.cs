using System.Threading;

namespace Defter.SharedLibrary
{
    public class TaskContext
    {
        public CancellationToken CancellationToken { get; set; }

        public bool IsShutdownRequested => CancellationToken.IsCancellationRequested;
    }
}
