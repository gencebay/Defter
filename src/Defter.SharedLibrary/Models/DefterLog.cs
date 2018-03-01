using MongoDB.Bson.Serialization.Attributes;
using NetCoreStack.Data.Contracts;
using System;

namespace Defter.SharedLibrary.Models
{
    public class DefterLog : EntityIdentityBson
    {
        public string RequestId { get; set; }

        public string NodeId { get; set; }

        public double Time { get; set; }

        public string Type { get; set; }

        public AuditLogLevel Level { get; set; }

        public string Name { get; set; }

        public string Message { get; set; }

        public string IpAddress { get; set; }

        public string UserName { get; set; }

        public string UserId { get; set; }

        public string UserIdProv { get; set; }

        public string Signature { get; set; }

        public string EntityClass { get; set; }

        public string EntityOid { get; set; }

        public string Status { get; set; }

        public string ServiceOid { get; set; }

        public string OperationName { get; set; }

        public string Authenticated { get; set; }

        public string AuthType { get; set; }

        public int RequestContentLength { get; set; }

        public int ResponseContentLength { get; set; }

        public int ResponseStatus { get; set; }

        public int RoutingLatency { get; set; }

        public string ComponentId { get; set; }

        public string Action { get; set; }

        public string RequestContent { get; set; }

        public string ResponseContent { get; set; }
        
        public DateTime UtcCreatedDateTime { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime CreatedDateTime { get; set; }
    }
}