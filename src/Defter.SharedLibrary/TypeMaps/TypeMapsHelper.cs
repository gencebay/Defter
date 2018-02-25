using Defter.SharedLibrary.Models;
using NetCoreStack.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

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

            return new DefterLog
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
                SavedRequestContentLength = message.SavedRequestContentLength,
                SavedResponseContentLength = message.SavedResponseContentLength,
                ServiceOid = message.ServiceOid,
                Signature = message.Signature,
                Status = message.Status,
                Time = message.Time,
                Type = message.Type,
                UserId = message.UserId,
                UserIdProv = message.UserIdProv,
                UserName = message.UserName,
                UtcCreatedDate = DateTime.UtcNow
            };
        }
    }
}
