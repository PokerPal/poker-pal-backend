namespace Persistence
{
    /// <summary>
    /// Options for creating a database context using a <see cref="DatabaseContextFactory" />.
    /// </summary>
    public class DatabaseContextFactoryOptions
    {
        internal string InMemoryDatabaseName { get; private set; }

        internal bool InMemory { get; private set; }

        internal string ConnectionString { get; private set; }

        /// <summary>
        /// Creates a set of options for using an in-memory database.
        /// </summary>
        /// <param name="inMemoryDatabaseName">The name of the in-memory database (default:
        /// "default_in_memory_database").</param>
        /// <returns>The created options.</returns>
        public DatabaseContextFactoryOptions UseInMemoryDatabase(
            string inMemoryDatabaseName = "default_in_memory_database")
        {
            this.InMemory = true;
            this.InMemoryDatabaseName = inMemoryDatabaseName;

            return this;
        }

        /// <summary>
        /// Creates a set of options for connecting to a database with the given connection string.
        /// </summary>
        /// <param name="connectionString">The connection string to use to connect to the database.</param>
        /// <returns>The created options.</returns>
        public DatabaseContextFactoryOptions UseConnectionString(string connectionString)
        {
            this.InMemory = false;
            this.ConnectionString = connectionString;

            return this;
        }
    }
}
