using System.ComponentModel.DataAnnotations;

namespace TimeTrackerService.Models
{
    /// <summary>
    /// Represents a task that can be assigned to time entries.
    /// </summary>
    public class TaskItem
    {
        /// <summary>
        /// Gets or sets the unique identifier for the task.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the task.
        /// </summary>
        [Required]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the detailed description of the task.
        /// </summary>
        public string Description { get; set; } = string.Empty;
    }
}
