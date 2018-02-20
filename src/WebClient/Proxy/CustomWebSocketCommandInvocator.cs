using NetCoreStack.WebSockets;
using NetCoreStack.WebSockets.ProxyClient;
using System.Threading.Tasks;

namespace WebClient.Proxy
{
    public class CustomWebSocketCommandInvocator : IClientWebSocketCommandInvocator
    {
        private readonly IConnectionManager _connectionManager;
        public CustomWebSocketCommandInvocator(IConnectionManager connectionManager)
        {
            _connectionManager = connectionManager;
        }

        public Task InvokeAsync(WebSocketMessageContext context)
        {
            // Sending incoming data from backend to the Browser(s)
            _connectionManager.BroadcastAsync(context);
            return Task.CompletedTask;
        }
    }
}
