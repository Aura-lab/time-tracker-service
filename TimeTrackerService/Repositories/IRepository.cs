namespace TimeTrackerService.Repositories
{
    /// <summary>
    /// Generic repository interface for basic CRUD operations.
    /// </summary>
    /// <typeparam name="T">The entity type.</typeparam>
    public interface IRepository<T> where T : class
    {
        /// <summary>
        /// Gets all entities, optionally including related entities.
        /// </summary>
        Task<IEnumerable<T>> GetAllAsync(Func<IQueryable<T>, IQueryable<T>>? include = null);

        /// <summary>
        /// Gets an entity by ID, optionally including related entities.
        /// </summary>
        Task<T?> GetByIdAsync(int id, Func<IQueryable<T>, IQueryable<T>>? include = null);

        /// <summary>
        /// Adds a new entity.
        /// </summary>
        Task AddAsync(T entity);

        /// <summary>
        /// Updates an existing entity.
        /// </summary>
        Task UpdateAsync(T entity);

        /// <summary>
        /// Deletes an entity.
        /// </summary>
        Task DeleteAsync(T entity);

        /// <summary>
        /// Checks if an entity exists by ID.
        /// </summary>
        Task<bool> ExistsAsync(int id);
    }
}
