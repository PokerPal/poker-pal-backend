using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.DependencyInjection;

using Persistence;
using Persistence.Interfaces;

namespace Migrations
{
    /// <summary>
    /// This is used by the Entity Framework tools to construct a database context in order to make a new migration.
    /// </summary>
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<DatabaseContext>
    {
        /// <summary>
        /// Creates a new <see cref="DatabaseContext"/>.
        /// </summary>
        /// <param name="args">Arguments.</param>
        /// <returns>A new database context.</returns>
        public DatabaseContext CreateDbContext(string[] args)
        {
            var connectionString = "postgres://none:none@postgres/none";

            var serviceCollection = new ServiceCollection();
            serviceCollection.AddDatabaseContextFactory(options =>
            {
                options.UseConnectionString(connectionString);
            });

            var sp = serviceCollection.BuildServiceProvider();
            var factory = sp.GetService<IDatabaseContextFactory<DatabaseContext>>();
            return factory.CreateDatabaseContext();
        }
    }
}
