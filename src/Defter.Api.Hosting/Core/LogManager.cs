using Defter.SharedLibrary;
using Defter.SharedLibrary.Models;
using NetCoreStack.Contracts;
using NetCoreStack.Data.Interfaces;
using System.Linq;
using NetCoreStack.Mvc.Extensions;

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
            _unitOfWork.Repository<DefterLog>().SaveAllChanges(model);
        }

        public CollectionResult<DefterGenericMessage> GetCollection(CollectionRequest request)
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
                SavedRequestContentLength = d.SavedRequestContentLength,
                SavedResponseContentLength = d.SavedResponseContentLength,
                ServiceOid = d.ServiceOid,
                Signature =d.Signature,
                Status = d.Status,
                Time = d.Time,
                Type = d.Type,
                UserId = d.UserId,
                UserIdProv = d.UserIdProv,
                UserName = d.UserName,
                UtcCreatedDate = d.UtcCreatedDate
            });

            return query.ToCollectionResult(request);
        }
    }
}
