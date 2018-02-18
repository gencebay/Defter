﻿using Microsoft.Extensions.DependencyInjection;
using System;

namespace WcfServer
{
    public interface IStartup
    {
        IServiceProvider ConfigureServices(IServiceCollection services);

        void Configure(IApplicationBuilder app);
    }
}