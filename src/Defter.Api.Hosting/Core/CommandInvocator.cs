using NetCoreStack.WebSockets;
using System.Threading.Tasks;

namespace Defter.Api.Hosting
{
    public class CommandInvocator : IServerWebSocketCommandInvocator
    {
        /// <summary>
        /// Incoming data processing
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task InvokeAsync(WebSocketMessageContext context)
        {
            await Task.CompletedTask;
        }
    }
}
