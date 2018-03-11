using Microsoft.Extensions.DependencyInjection;
using NetCoreStack.Contracts;

namespace Defter.SharedLibrary
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDomainEvents(this IServiceCollection services)
        {
            services.AddSingleton<IEventDispatcher, DomainEventDispatcher>();
            services.AddScoped<IScopeEventRegistrar, DefaultScopeEventRegistrar>();

            return services;
        }
    }
}
