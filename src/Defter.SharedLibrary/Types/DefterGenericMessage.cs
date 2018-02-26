using NetCoreStack.Contracts;
using System;
using System.ComponentModel.DataAnnotations;

namespace Defter.SharedLibrary
{
    public class DefterGenericMessage : MessageBase
    {
        [PropertyDescriptor(EnableFilter = false)]
        public string RequestContent { get; set; }

        [PropertyDescriptor(EnableFilter = false)]
        public string ResponseContent { get; set; }

        [Display(Name = "Created Date")]
        public DateTime UtcCreatedDate { get; set; }
    }
}