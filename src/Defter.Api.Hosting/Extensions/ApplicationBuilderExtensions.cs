using Defter.Api.Hosting.Middleware;
using Defter.SharedLibrary;
using Defter.SharedLibrary.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using NetCoreStack.Data.Interfaces;
using NetCoreStack.WebSockets;
using System.Globalization;

namespace Defter.Api.Hosting
{
    public static class ApplicationBuilderExtensions
    {
        public static void UseApi(this IApplicationBuilder app)
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

        public static void CreateIndices(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                using (IMongoUnitOfWork unitOfWork = scope.ServiceProvider.GetService<IMongoUnitOfWork>())
                {
                    var uniqueIndexOptions = new CreateIndexOptions() { Unique = true };

                    IndexKeysDefinition<DefterLog> idDefinition =
                        new IndexKeysDefinitionBuilder<DefterLog>().Descending(p => p.RequestId);

                    var idIndexModel = new CreateIndexModel<DefterLog>(idDefinition, uniqueIndexOptions);

                    IndexKeysDefinition<DefterLog> name = new IndexKeysDefinitionBuilder<DefterLog>()
                        .Descending(p => p.Name);

                    var nameIndexModel = new CreateIndexModel<DefterLog>(name);

                    IndexKeysDefinition<DefterLog> operationName = new IndexKeysDefinitionBuilder<DefterLog>()
                        .Descending(p => p.OperationName);

                    var operationNameIndexModel = new CreateIndexModel<DefterLog>(operationName);

                    unitOfWork.RepositoryManager<DefterLog>().CreateIndices(new[] {
                        idIndexModel, 
                        nameIndexModel,
                        operationNameIndexModel
                    });
                }
            }
        }
    }
}