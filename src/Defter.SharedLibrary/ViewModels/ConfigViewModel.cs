using NetCoreStack.Contracts;

namespace Defter.SharedLibrary
{
    public class ConfigViewModel : CollectionModelBson
    {
        public string Name { get; set; }
        public string OperationName { get; set; }
    }
}