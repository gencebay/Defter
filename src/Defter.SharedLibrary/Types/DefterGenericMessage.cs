using MongoDB.Bson.Serialization.Attributes;
using NetCoreStack.Contracts;
using System;
using System.ComponentModel.DataAnnotations;

namespace Defter.SharedLibrary
{
    public class DefterGenericMessage : CollectionModelBson
    {
        [PropertyDescriptor(EnableFilter = false)]
        public string RequestContent { get; set; }

        [PropertyDescriptor(EnableFilter = false)]
        public string ResponseContent { get; set; }

        [PropertyDescriptor(EnableFilter = false)]
        public DateTime UtcCreatedDateTime { get; set; }

        [Display(Name = "İstek Id")]
        [PropertyDescriptor(EnableFilter = false)]
        public string RequestId { get; set; }

        [PropertyDescriptor(EnableFilter = false)]
        public string NodeId { get; set; }

        [Display(Name = "Gateway Zaman Damgası")]
        [PropertyDescriptor(EnableFilter = false)]
        public double Time { get; set; }

        [PropertyDescriptor(EnableFilter = false)]
        public string Type { get; set; }

        [Display(Name = "Log Tipi")]
        [PropertyDescriptor(IsSelectable = true)]
        public AuditLogLevel Level { get; set; }

        [Display(Name = "Servis")]
        public string Name { get; set; }

        [PropertyDescriptor(EnableFilter = false)]
        public string Message { get; set; }

        [Display(Name = "İp Adresi")]
        public string IpAddress { get; set; }

        [Display(Name = "Kullanıcı Adı")]
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

        [Display(Name = "Metot Adı")]
        public string OperationName { get; set; }

        [PropertyDescriptor(EnableFilter = false)]
        public string Authenticated { get; set; }

        [PropertyDescriptor(EnableFilter = false)]
        public string AuthType { get; set; }

        [PropertyDescriptor(EnableFilter = false)]
        public int RequestContentLength { get; set; }

        [PropertyDescriptor(EnableFilter = false)]
        public int ResponseContentLength { get; set; }

        [Display(Name = "Cevap")]
        public int ResponseStatus { get; set; }

        [PropertyDescriptor(EnableFilter = false)]
        public int RoutingLatency { get; set; }

        [PropertyDescriptor(EnableFilter = false)]
        public string ComponentId { get; set; }

        [Display(Name = "Aksiyon")]
        [PropertyDescriptor(EnableFilter = false)]
        public string Action { get; set; }

        [Display(Name = "Log Kayıt Tarihi")]
        [PropertyDescriptor(IsFullDateTime = true)]
        public DateTime CreatedDateTime { get; set; }
    }
}