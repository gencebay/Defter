using Defter.SharedLibrary;
using Microsoft.AspNetCore.Mvc;
using NetCoreStack.WebSockets;
using System.Threading.Tasks;

namespace Defter.Api.Hosting.Controllers
{
    [Route("api/[controller]")]
    public class DefterController : ControllerBase, IDefterApi
    {
        private readonly IConnectionManager _connectionManager;

        public DefterController(IConnectionManager connectionManager)
        {
            _connectionManager = connectionManager;
        }

        [HttpPost(nameof(GenericCapture))]
        public async Task<ApiResult> GenericCapture([FromBody]DefterGenericMessage model)
        {
            if (ModelState.IsValid)
            {

            }

            var webSocketContext = new WebSocketMessageContext { Command = WebSocketCommands.DataSend, Value = model };
            await _connectionManager.BroadcastAsync(webSocketContext);

            await Task.CompletedTask;
            return new ApiResult();
        }
    }
}