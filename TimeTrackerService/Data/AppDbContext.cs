using Microsoft.EntityFrameworkCore;
using TimeTrackerService.Models;

namespace TimeTrackerService.Data
{
    /// <summary>
    /// Application database context for managing entities and database operations.
    /// </summary>
    public class AppDbContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AppDbContext"/> class.
        /// </summary>
        /// <param name="options">The options to be used by a <see cref="DbContext"/>.</param>
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        /// <summary>
        /// Gets or sets the People table.
        /// </summary>
        public DbSet<Person> People { get; set; }

        /// <summary>
        /// Gets or sets the Tasks table.
        /// </summary>
        public DbSet<TaskItem> Tasks { get; set; }

        /// <summary>
        /// Gets or sets the TimeEntries table.
        /// </summary>
        public DbSet<TimeEntry> TimeEntries { get; set; }

        /// <summary>
        /// Configures the entity models and seeds initial data.
        /// </summary>
        /// <param name="modelBuilder">The model builder used to configure entity mappings.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Seed People
            modelBuilder.Entity<Person>().HasData(
                new Person { Id = 1, FullName = "Anton" },
                new Person { Id = 2, FullName = "Aura" }
            );

            // Seed Tasks
            modelBuilder.Entity<TaskItem>().HasData(
                new TaskItem { Id = 1, Name = "Programming", Description = "Programming" },
                new TaskItem { Id = 2, Name = "Testing", Description = "Testing" }
            );
        }
    }
}
