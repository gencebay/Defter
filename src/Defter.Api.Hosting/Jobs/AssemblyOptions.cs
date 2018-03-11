using System.Collections.Generic;
using System.Reflection;

namespace Defter.Api.Hosting
{
    internal class AssemblyOptions
    {
        internal List<Assembly> AssemblyList { get; }

        public AssemblyOptions(List<Assembly> assemblyList)
        {
            AssemblyList = assemblyList;
        }
    }
}
