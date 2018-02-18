using NetCoreStack.Contracts;

namespace Defter.SharedLibrary
{
    public class DefterGenericMessage : MessageBase
    {
        [PropertyDescriptor(EnableFilter = false)]
        public string RequestContent { get; set; }

        [PropertyDescriptor(EnableFilter = false)]
        public string ResponseContent { get; set; }
    }
}
