using System.Diagnostics.CodeAnalysis;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Persistence
{
    /// <inheritdoc />
    public class DesignTimeContextFactory : IDesignTimeDbContextFactory<DatabaseContext>
    {
        /// <inheritdoc />
        public DatabaseContext CreateDbContext([NotNull] string[] args)
        {
            const string connectionString =
                "Host=localhost;Port=5032;Database=poker-pal;Username=group2;Password=password";
            var options = new DbContextOptionsBuilder().UseNpgsql(connectionString).Options;

            return new DatabaseContext(options);
        }
    }
}
