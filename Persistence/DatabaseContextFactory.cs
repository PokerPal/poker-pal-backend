using System;
using System.Text;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

using Persistence.Interfaces;

namespace Persistence
{
    /// <inheritdoc />
    public class DatabaseContextFactory : IDatabaseContextFactory<DatabaseContext>
    {
        private readonly DbContextOptions contextOptions;

        /// <summary>
        /// Initializes a new instance of the <see cref="DatabaseContextFactory"/> class.
        /// </summary>
        /// <param name="options">Options for the creation of database contexts.</param>
        /// <param name="serviceProvider">The service provider.</param>
        public DatabaseContextFactory(
            IOptions<DatabaseContextFactoryOptions> options,
            IServiceProvider serviceProvider)
        {
            if (options.Value.InMemory)
            {
                var builder = new DbContextOptionsBuilder()
                    .UseInMemoryDatabase(options.Value.InMemoryDatabaseName);

                this.contextOptions = builder.Options;
            }
            else
            {
                var connectionString =
                    Encoding.UTF8.GetString(
                        Convert.FromBase64String(options.Value.ConnectionString));

                var builder = new DbContextOptionsBuilder()
                    .UseNpgsql(connectionString);
                this.contextOptions = builder.Options;
            }
        }

        /// <inheritdoc/>
        public DatabaseContext CreateDatabaseContext()
        {
            return new DatabaseContext(this.contextOptions);
        }
    }
}
