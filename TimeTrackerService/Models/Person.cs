using System.ComponentModel.DataAnnotations;

namespace TimeTrackerService.Models
{
    /// <summary>
    /// Represents a person who can log time entries.
    /// </summary>
    public class Person
    {
        /// <summary>
        /// Gets or sets the unique identifier for the person.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the full name of the person.
        /// </summary>
        [Required]
        public string FullName { get; set; } = string.Empty;
    }
}