using System;

using Microsoft.Extensions.DependencyInjection;

using Persistence.Interfaces;

namespace Persistence
{
    /// <summary>
    /// Helper methods for adding things to a service collection.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Add a database context factory to a service collection.
        /// </summary>
        /// <param name="services">The service collection to add the factory to.</param>
        /// <param name="configure">Configuration for the factory.</param>
        public static void AddDatabaseContextFactory(
            this IServiceCollection services,
            Action<DatabaseContextFactoryOptions> configure)
        {
            services.Configure(configure);
            services
                .AddSingleton<IDatabaseContextFactory<DatabaseContext>, DatabaseContextFactory>();
        }
    }
}
