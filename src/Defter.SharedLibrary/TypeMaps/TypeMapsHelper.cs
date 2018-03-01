using Defter.SharedLibrary.Helpers;
using Defter.SharedLibrary.Models;
using NetCoreStack.Contracts;
using System;

namespace Defter.SharedLibrary
{
    public static class TypeMapsHelper
    {
        public static DefterLog ConvertToDefterLog(this DefterGenericMessage message)
        {
            if (message == null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            var log = new DefterLog
            {
                Action = message.Action,
                Authenticated = message.Authenticated,
                AuthType = message.AuthType,
                ComponentId = message.ComponentId,
                EntityClass = message.EntityClass,
                EntityOid = message.EntityOid,
                RequestId = message.RequestId,
                IpAddress = message.IpAddress,
                Level = message.Level,
                Message = message.Message,
                Name = message.Name,
                NodeId = message.NodeId,
                ObjectState = ObjectState.Added,
                OperationName = message.OperationName,
                RequestContent = message.RequestContent,
                ResponseContent = message.ResponseContent,
                ResponseStatus = message.ResponseStatus,
                RoutingLatency = message.RoutingLatency,
                RequestContentLength = message.RequestContentLength,
                ResponseContentLength = message.ResponseContentLength,
                ServiceOid = message.ServiceOid,
                Signature = message.Signature,
                Status = message.Status,
                Time = message.Time,
                Type = message.Type,
                UserId = message.UserId,
                UserIdProv = message.UserIdProv,
                UserName = message.UserName
            };

            if (message.Time != default(double))
            {
                log.UtcCreatedDateTime = DateTimeHelper.JavaTimeStampToDateTime(message.Time);

                // DateTimeKind.Local
                log.CreatedDateTime = DateTimeHelper.JavaTimeStampToDateTime(message.Time);
            }

            return log;
        }
    }
}
