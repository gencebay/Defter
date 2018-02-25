﻿using Defter.SharedLibrary;
using Microsoft.AspNetCore.Mvc;
using NetCoreStack.WebSockets;
using System.Threading.Tasks;

namespace Defter.Api.Hosting.Controllers
{
    [Route("api/[controller]")]
    public class DefterController : ControllerBase, IDefterApi
    {
        private readonly LogManager _logManager;
        private readonly IConnectionManager _connectionManager;

        public DefterController(LogManager logManager, IConnectionManager connectionManager)
        {
            _logManager = logManager;
            _connectionManager = connectionManager;
        }

        [HttpPost(nameof(Yaz))]
        public async Task<ApiResult> Yaz([FromBody]DefterGenericMessage model)
        {
            if (model == null)
            {
                return new ApiResult();
            }

            _logManager.SaveLog(model.ConvertToDefterLog());
            var webSocketContext = new WebSocketMessageContext { Command = WebSocketCommands.DataSend, Value = model };
            await _connectionManager.BroadcastAsync(webSocketContext);

            return new ApiResult();
        }
    }
}