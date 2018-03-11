using Defter.Api.Hosting.Middleware;
using Defter.SharedLibrary;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetCoreStack.Data;
using NetCoreStack.Data.Context;
using NetCoreStack.Mvc;
using NetCoreStack.WebSockets;
using Swashbuckle.AspNetCore.Swagger;
using System.Globalization;

namespace Defter.Api.Hosting
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

            services.AddNativeWebSockets<CommandInvocator>();

            services.AddDomainEvents();

            services.AddSingleton<InMemoryMessageQueue>();
            services.AddSingleton<ProcessServer>();
            services.AddScoped<LogManager>();

            services.AddLocalization();

            services.AddNetCoreStack();

            services.AddJobs(setup =>
            {
                setup.Register<JobWorker>(Cron.Minutely());
                setup.Register<CustomJob, ThirdSecondThrottler>();
            });

            services.AddNetCoreStackMongoDb<MongoDbContext>(Configuration);

            services.AddComposers(setup =>
            {
                setup.CreateMap<DefterGenericMessage, DefterLogComposer>();
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Defter API", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseNativeWebSockets();

            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture(new CultureInfo(HostingFactory.DefaultCulture)),
                SupportedCultures = HostingFactory.SupportedCultures,
                SupportedUICultures = HostingFactory.SupportedCultures
            });

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Defter API v1");
            });

            app.UseMiddleware<DefterMiddleware>();

            app.UseMvc();

            app.CreateIndices();

            app.UseJobServer();
        }
    }
}
