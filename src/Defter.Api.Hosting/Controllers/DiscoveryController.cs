using Defter.SharedLibrary;
using Microsoft.AspNetCore.Mvc;
using NetCoreStack.WebSockets;
using System.Linq;
using System.Net.WebSockets;
using System.Threading.Tasks;

namespace Defter.Api.Hosting.Controllers
{
    [Route("api/[controller]")]
    public class DiscoveryController : ControllerBase, IDiscoveryApi
    {
        private readonly IConnectionManager _connectionManager;

        public DiscoveryController(IConnectionManager connectionManager)
        {
            _connectionManager = connectionManager;
        }

        [HttpGet(nameof(GetConnections))]
        public async Task<ApiResult> GetConnections()
        {
            await Task.CompletedTask;
            var connections = _connectionManager.Connections.Select(c => new
            {
                c.Value.ConnectionId,
                c.Value.ConnectorName,
                c.Value.WebSocket.State
            }).ToList();

            return new ApiResult
            {
                Value = connections
            };
        }

        [HttpPost(nameof(BroadcastAsync))]
        public async Task<ApiResult> BroadcastAsync([FromBody]BroadcastContext context)
        {
            await _connectionManager.BroadcastAsync(new WebSocketMessageContext
            {
                Command = WebSocketCommands.DataSend,
                MessageType = WebSocketMessageType.Text,
                Value = context
            });

            return new ApiResult();
        }
    }
}