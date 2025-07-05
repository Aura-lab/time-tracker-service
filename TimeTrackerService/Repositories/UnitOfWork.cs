using TimeTrackerService.Data;

namespace TimeTrackerService.Repositories
{
    /// <summary>
    /// Implements the unit of work pattern for committing changes to the database.
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitOfWork"/> class.
        /// </summary>
        /// <param name="context">The database context.</param>
        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Commits all changes made in the current unit of work to the database.
        /// </summary>
        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
