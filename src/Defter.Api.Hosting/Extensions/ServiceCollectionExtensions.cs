using Defter.SharedLibrary;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetCoreStack.Data;
using NetCoreStack.Data.Context;
using NetCoreStack.Mvc;
using NetCoreStack.WebSockets;
using Swashbuckle.AspNetCore.Swagger;

namespace Defter.Api.Hosting
{
    public static class ServiceCollectionExtensions
    {
        public static void AddApiFeatures(this IServiceCollection services, IConfiguration configuration)
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

            services.AddNetCoreStackMongoDb<MongoDbContext>(configuration);

            services.AddComposers(setup =>
            {
                setup.CreateMap<DefterGenericMessage, DefterLogComposer>();
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Defter API", Version = "v1" });
            });
        }
    }
}
