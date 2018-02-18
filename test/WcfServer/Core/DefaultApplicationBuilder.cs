using System;

namespace WcfServer
{
    public class DefaultApplicationBuilder : IApplicationBuilder
    {
        public IServiceProvider ApplicationServices { get; set; }

        public DefaultApplicationBuilder()
        {
        }
    }
}