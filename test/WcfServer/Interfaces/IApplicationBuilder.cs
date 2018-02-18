using System;

namespace WcfServer
{
    public interface IApplicationBuilder
    {
        IServiceProvider ApplicationServices { get; set; }
    }
}