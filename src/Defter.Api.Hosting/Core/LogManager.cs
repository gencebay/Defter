using Defter.SharedLibrary;
using Defter.SharedLibrary.Models;
using NetCoreStack.Contracts;
using NetCoreStack.Data.Interfaces;
using System.Linq;
using NetCoreStack.Mvc.Extensions;
using System.Threading;

namespace Defter.Api.Hosting
{
    public class LogManager
    {
        private readonly IMongoUnitOfWork _unitOfWork;

        public LogManager(IMongoUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void SaveLog(DefterLog model)
        {
            _unitOfWork.Repository<DefterLog>().Insert(model);
        }

        public CollectionResult<DefterGenericMessage> GetDefterLogCollection(CollectionRequest request)
        {
            var query = _unitOfWork.Repository<DefterLog>().Select(d => new DefterGenericMessage
            {
                Action = d.Action,
                Authenticated = d.Authenticated,
                AuthType = d.AuthType,
                ComponentId = d.ComponentId,
                EntityClass = d.EntityClass,
                EntityOid = d.EntityOid,
                Id = d.Id,
                IpAddress = d.IpAddress,
                Level = d.Level,
                Message = d.Message,
                Name = d.Name,
                NodeId = d.NodeId,
                OperationName = d.OperationName,
                RequestContent = d.RequestContent,
                RequestId = d.RequestId,
                ResponseContent = d.ResponseContent,
                ResponseStatus = d.ResponseStatus,
                RoutingLatency = d.RoutingLatency,
                RequestContentLength = d.RequestContentLength,
                ResponseContentLength = d.ResponseContentLength,
                ServiceOid = d.ServiceOid,
                Signature =d.Signature,
                Status = d.Status,
                Time = d.Time,
                Type = d.Type,
                UserId = d.UserId,
                UserIdProv = d.UserIdProv,
                UserName = d.UserName,
                CreatedDateTime = d.CreatedDateTime,
                UtcCreatedDateTime = d.UtcCreatedDateTime
            });

            var cultureInfo = Thread.CurrentThread.CurrentCulture;


            return query.ToCollectionResult(request);
        }
    }
}
