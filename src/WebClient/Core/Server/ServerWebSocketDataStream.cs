using NetCoreStack.WebSockets;
using System.Threading.Tasks;

namespace WebClient.Core
{
    public class ServerWebSocketDataStream : IServerWebSocketCommandInvocator
    {
        public async Task InvokeAsync(WebSocketMessageContext context)
        {
            await Task.CompletedTask;
        }
    }
}