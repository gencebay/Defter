using System.Runtime.Serialization;

namespace Wcf.Contracts
{
    [DataContract]
    public class CompositeType
    {
        [DataMember]
        public bool BoolValue { get; set; }

        [DataMember]
        public string StringValue { get; set; }
    }
}
