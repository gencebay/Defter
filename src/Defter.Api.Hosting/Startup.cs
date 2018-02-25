using Defter.Api.Hosting.Middleware;
using Defter.SharedLibrary;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetCoreStack.Data.Context;
using NetCoreStack.Data;
using NetCoreStack.WebSockets;
using Swashbuckle.AspNetCore.Swagger;
using Defter.SharedLibrary.Models;
using Defter.Api.Hosting.Extensions;

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

            services.AddScoped<LogManager>();

            services.AddNetCoreStackMongoDb<MongoDbContext>(Configuration);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Defter API", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseNativeWebSockets();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Defter API v1");
            });

            app.UseMiddleware<DefterMiddleware>();

            app.UseMvc();

            app.CreateIndices();
        }
    }
}
