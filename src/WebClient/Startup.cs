using Defter.SharedLibrary;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetCoreStack.Mvc;
using NetCoreStack.Proxy;
using NetCoreStack.WebSockets;
using WebClient.Core;

namespace WebClient
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddNetCoreProxy(Configuration, options =>
            {
                // Register the API to use as a Proxy
                options.Register<IDefterApi>();
            });

            services.AddNativeWebSockets<WebSocketDataStream>();

            services.AddNetCoreStackMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseNativeWebSockets();

            app.UseMvcWithDefaultRoute();
        }
    }
}
