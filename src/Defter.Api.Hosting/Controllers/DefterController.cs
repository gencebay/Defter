using Defter.SharedLibrary;
using Microsoft.AspNetCore.Mvc;
using NetCoreStack.Contracts;
using NetCoreStack.WebSockets;
using System;
using System.Threading.Tasks;

namespace Defter.Api.Hosting.Controllers
{
    [Route("api/[controller]")]
    public class DefterController : ControllerBase, IDefterApi
    {
        private readonly LogManager _logManager;
        private readonly IConnectionManager _connectionManager;
        private readonly InMemoryMessageQueue _messageQueue;

        public DefterController(LogManager logManager, 
            IConnectionManager connectionManager,
            InMemoryMessageQueue messageQueue)
        {
            _logManager = logManager;
            _connectionManager = connectionManager;
            _messageQueue = messageQueue;
        }

        [HttpGet(nameof(Sorgu))]
        public async Task<CollectionResult<DefterGenericMessage>> Sorgu(CollectionRequest request)
        {
            await Task.CompletedTask;
            return _logManager.GetDefterLogCollection(request);
        }

        [HttpPost(nameof(Yaz))]
        public async Task<ApiResult> Yaz([FromBody]DefterGenericMessage model)
        {
            if (model == null)
            {
                return new ApiResult();
            }

            try
            {
                // Burada oluşabilecek hatayı yakalayıp explicit 500 dönüyor ve API Gateway' in logu kendi
                // veri tabanına yazmasını sağlıyoruz.
                _logManager.SaveLog(model.ConvertToDefterLog());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                var webSocketContext = new WebSocketMessageContext { Command = WebSocketCommands.DataSend, Value = model };
                _messageQueue.Enqueue(webSocketContext);
            }

            await Task.CompletedTask;

            return new ApiResult();
        }
    }
}