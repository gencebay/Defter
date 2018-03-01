using Defter.SharedLibrary;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetCoreStack.Mvc;
using NetCoreStack.Proxy;
using NetCoreStack.WebSockets;
using NetCoreStack.WebSockets.ProxyClient;
using System.Globalization;
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

                options.CultureFactory = () =>
                {
                    return System.Threading.Thread.CurrentThread.CurrentCulture;
                };
            });

            services.AddNativeWebSockets<ServerWebSocketDataStream>();

            services.AddLocalization();

            services.AddNetCoreStackMvc();

            services.Configure<ApiSettings>(Configuration.GetSection(nameof(ApiSettings)));

            services.AddProxyWebSockets()
                .Register<ProxyWebSocketDataStream, DefaultClientInvocatorContextFactory>();
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

            app.UseRequestLocalization(new RequestLocalizationOptions {
                DefaultRequestCulture = new RequestCulture(new CultureInfo(HostingFactory.DefaultCulture)),
                SupportedCultures = HostingFactory.SupportedCultures,
                SupportedUICultures = HostingFactory.SupportedCultures
            });

            app.UseStaticFiles();

            app.UseNativeWebSockets();

            app.UseMvcWithDefaultRoute();

            app.UseProxyWebSockets();
        }
    }
}
