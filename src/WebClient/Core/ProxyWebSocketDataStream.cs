using NetCoreStack.WebSockets;
using NetCoreStack.WebSockets.ProxyClient;
using System.Threading.Tasks;

namespace WebClient.Core
{
    public class ProxyWebSocketDataStream : IClientWebSocketCommandInvocator
    {
        private readonly IConnectionManager _connectionManager;

        public ProxyWebSocketDataStream(IConnectionManager connectionManager)
        {
            _connectionManager = connectionManager;
        }
        
        public async Task InvokeAsync(WebSocketMessageContext context)
        {
            await _connectionManager.BroadcastAsync(context);
        }
    }
}
