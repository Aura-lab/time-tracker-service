using System.ComponentModel.DataAnnotations;

namespace TimeTrackerService.Models
{
    /// <summary>
    /// Represents a single time tracking entry.
    /// </summary>
    public class TimeEntry
    {
        /// <summary>
        /// Gets or sets the unique identifier for the time entry.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the ID of the person who logged the time.
        /// </summary>
        [Required]
        public int PersonId { get; set; }

        /// <summary>
        /// Gets or sets the related person entity.
        /// </summary>
        public Person? Person { get; set; }

        /// <summary>
        /// Gets or sets the ID of the task item being tracked.
        /// </summary>
        [Required]
        public int TaskItemId { get; set; }

        /// <summary>
        /// Gets or sets the related task item entity.
        /// </summary>
        public TaskItem? TaskItem { get; set; }

        /// <summary>
        /// Gets or sets the date when the work was performed.
        /// </summary>
        [Required]
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets the number of hours worked on the task.
        /// </summary>
        [Required]
        [Range(0, 24, ErrorMessage = "Hours worked must be between 0 and 24.")]
        public decimal HoursWorked { get; set; }
    }
}
