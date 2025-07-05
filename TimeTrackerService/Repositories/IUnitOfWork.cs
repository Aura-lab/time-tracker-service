namespace TimeTrackerService.Repositories
{
    /// <summary>
    /// Defines the unit of work contract for committing database changes.
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        /// Commits all changes made in the current unit of work to the database.
        /// </summary>
        Task CommitAsync();
    }
}
