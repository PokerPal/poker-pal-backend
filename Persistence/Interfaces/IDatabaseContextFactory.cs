namespace Persistence.Interfaces
{
    /// <summary>
    /// Factory for creating database contexts.
    /// </summary>
    /// <typeparam name="TContext">The badgeType of database context that is created.</typeparam>
    public interface IDatabaseContextFactory<out TContext>
    {
        /// <summary>
        /// Create a database context.
        /// </summary>
        /// <returns>The created context.</returns>
        public TContext CreateDatabaseContext();
    }
}
