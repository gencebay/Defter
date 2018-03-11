using Defter.SharedLibrary.Helpers;
using NetCoreStack.WebSockets;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace Defter.Api.Hosting
{
    public class InMemoryMessageQueue
    {
        private readonly SocketQueue _socketQueue;
        private readonly IConnectionManager _connectionManager;

        public string Id => throw new System.NotImplementedException();

        public InMemoryMessageQueue(IConnectionManager connectionManager)
        {
            _socketQueue = new SocketQueue();
            _connectionManager = connectionManager;
        }

        public void Enqueue(WebSocketMessageContext context)
        {
            _socketQueue.Enqueue(context);
        }

        public async Task Dequeue(TaskContext taskContext)
        {
            if(_socketQueue.TryDequeue(out WebSocketMessageContext context))
            {
                await _connectionManager.BroadcastAsync(JsonHelper.SerializeToBytes(context, camelCase: true));
            }
        }

        private class SocketQueue : ConcurrentQueue<WebSocketMessageContext>
        {
            
        } 
    }
}
