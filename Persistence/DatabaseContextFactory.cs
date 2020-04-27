using System;
using System.Web;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

using Npgsql;

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
                var connectionString = this.ConvertConnectionString(options.Value.ConnectionString);

                var builder = new DbContextOptionsBuilder()
                    .UseNpgsql(
                        connectionString);

                // TODO: Once migrations have been set up, use these options.
                /*
                var builder = new DbContextOptionsBuilder()
                    .UseNpgsql(
                        connectionString,
                        pgOptions => { pgOptions.MigrationsAssembly("Persistence.Migrations"); });
                */

                this.contextOptions = builder.Options;
            }
        }

        /// <inheritdoc/>
        public DatabaseContext CreateDatabaseContext()
        {
            return new DatabaseContext(this.contextOptions);
        }

        private string ConvertConnectionString(string oldString)
        {
            var databaseUri = new Uri(oldString);
            var userInfo = databaseUri.UserInfo.Split(':');

            var queryString = HttpUtility.ParseQueryString(databaseUri.Query);
            var isSsl = queryString["ssl"] == "true";

            return new NpgsqlConnectionStringBuilder
            {
                Host = databaseUri.Host,
                Port = databaseUri.IsDefaultPort ? 5432 : databaseUri.Port,
                Username = userInfo[0],
                Password = userInfo[1],
                Database = databaseUri.LocalPath.TrimStart('/'),
                SslMode = isSsl ? SslMode.Prefer : 0,
            }.ToString();
        }
    }
}
