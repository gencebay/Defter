using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Linq;
using System.Reflection;

namespace Defter.Api.Hosting
{
    public static class BackgroundJobServerServiceCollectionExtensions
    {
        private static readonly MethodInfo registryDelegate = 
            typeof(BackgroundJobServerServiceCollectionExtensions).GetTypeInfo()
            .GetDeclaredMethod(nameof(BackgroundJobServerServiceCollectionExtensions.RegisterJob));

        public static IServiceCollection AddJobs(this IServiceCollection services, Action<JobBuilderOptions> setup)
        {
            services.AddMemoryCache();
            services.AddOptions();

            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (setup == null)
            {
                throw new ArgumentNullException(nameof(setup));
            }

            services.AddSingleton<BackgroundJobServerMarkerService>();

            var opts = new JobBuilderOptions();
            setup?.Invoke(opts);
            foreach (var item in opts.JobList)
            {
                var type = item.Type;
                var genericRegistry = registryDelegate.MakeGenericMethod(type);
                genericRegistry.Invoke(null, new object[] { services, type });

                if (item.ThrottlerType != null)
                {
                    services.AddTransient(item.ThrottlerType);
                }
            }

            services.AddSingleton(opts);

            var assemblies = opts.JobList.Select(j => j.GetType().Assembly).Distinct().ToList();
            var assemblyOptions = new AssemblyOptions(assemblies);
            services.AddSingleton(assemblyOptions);

            return services;
        }

        internal static void RegisterJob<TJob>(IServiceCollection services, Type type) where TJob : IJob
        {
            services.AddTransient(typeof(TJob), type);
        }
    }
}
