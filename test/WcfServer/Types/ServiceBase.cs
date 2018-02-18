using System;
using System.Security.Principal;

namespace WcfServer
{
    public abstract class ServiceBase : IServiceBase
    {
        public IServiceProvider ApplicationServices { get; set; }

        public IPrincipal Principal { get; set; }
    }
}