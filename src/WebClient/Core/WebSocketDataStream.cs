using NetCoreStack.WebSockets;
using System.Threading.Tasks;

namespace WebClient.Core
{
    public class WebSocketDataStream : IServerWebSocketCommandInvocator
    {
        public async Task InvokeAsync(WebSocketMessageContext context)
        {
            await Task.CompletedTask;
        }
    }
}
