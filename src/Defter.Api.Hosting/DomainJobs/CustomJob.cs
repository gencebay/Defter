using System.Threading.Tasks;

namespace Defter.Api.Hosting
{
    public class CustomJob : IJob
    {
        private readonly InMemoryMessageQueue _messageQueue;

        public string Id => "CustomJob";

        public CustomJob(InMemoryMessageQueue messageQueue)
        {
            _messageQueue = messageQueue;
        }

        public async Task InvokeAsync(TaskContext context)
        {
            await _messageQueue.Dequeue(context);
        }
    }
}