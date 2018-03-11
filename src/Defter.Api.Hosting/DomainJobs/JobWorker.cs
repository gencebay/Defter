using System.Threading.Tasks;

namespace Defter.Api.Hosting
{
    public class JobWorker : IJob
    {
        public string Id => "1";

        public Task InvokeAsync(TaskContext context)
        {
            return Task.CompletedTask;
        }
    }
}
