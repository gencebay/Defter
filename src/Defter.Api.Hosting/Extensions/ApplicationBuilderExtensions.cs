using Defter.SharedLibrary.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using NetCoreStack.Data.Interfaces;

namespace Defter.Api.Hosting.Extensions
{
    public static class ApplicationBuilderExtensions
    {
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
