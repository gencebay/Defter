using NetCoreStack.Contracts;
using NetCoreStack.Data.Contracts;

namespace Defter.SharedLibrary
{
    public abstract class MessageBase : EntityIdentityBson
    {
        [PropertyDescriptor(EnableFilter = false)]
        public string RequestId { get; set; }

        [PropertyDescriptor(EnableFilter = false)]
        public string NodeId { get; set; }

        public string Time { get; set; }

        public string Type { get; set; }

        public AuditLogLevel Level { get; set; }

        public string Name { get; set; }

        [PropertyDescriptor(EnableFilter = false)]
        public string Message { get; set; }

        public string IpAddress { get; set; }

        [PropertyDescriptor(EnableFilter = false)]
        public string UserName { get; set; }

        [PropertyDescriptor(EnableFilter = false)]
        public string UserId { get; set; }

        [PropertyDescriptor(EnableFilter = false)]
        public string UserIdProv { get; set; }

        [PropertyDescriptor(EnableFilter = false)]
        public string Signature { get; set; }

        [PropertyDescriptor(EnableFilter = false)]
        public string EntityClass { get; set; }

        [PropertyDescriptor(EnableFilter = false)]
        public string EntityOid { get; set; }

        [PropertyDescriptor(EnableFilter = false)]
        public string Status { get; set; }

        [PropertyDescriptor(EnableFilter = false)]
        public string ServiceOid { get; set; }

        public string OperationName { get; set; }

        [PropertyDescriptor(EnableFilter = false)]
        public string Authenticated { get; set; }

        [PropertyDescriptor(EnableFilter = false)]
        public string AuthType { get; set; }

        [PropertyDescriptor(EnableFilter = false)]
        public int SavedRequestContentLength { get; set; }

        [PropertyDescriptor(EnableFilter = false)]
        public int SavedResponseContentLength { get; set; }

        [PropertyDescriptor(EnableFilter = false)]
        public int ResponseStatus { get; set; }

        [PropertyDescriptor(EnableFilter = false)]
        public int RoutingLatency { get; set; }

        [PropertyDescriptor(EnableFilter = false)]
        public string ComponentId { get; set; }

        public string Action { get; set; }
    }
}