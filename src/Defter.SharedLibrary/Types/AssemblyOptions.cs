using System.Collections.Generic;
using System.Reflection;

namespace Defter.SharedLibrary
{
    public class AssemblyOptions
    {
        public List<Assembly> AssemblyList { get; }

        public AssemblyOptions(List<Assembly> assemblyList)
        {
            AssemblyList = assemblyList;
        }
    }
}
