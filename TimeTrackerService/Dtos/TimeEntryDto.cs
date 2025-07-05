namespace TimeTrackerService.Dtos
{
    /// <summary>
    /// Represents a time entry data transfer object.
    /// </summary>
    public class TimeEntryDto
    {
        /// <summary>
        /// ID of the time entry.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// ID of the person.
        /// </summary>
        public int PersonId { get; set; }

        /// <summary>
        /// Full name of the person (optional, for display).
        /// </summary>
        public string? PersonName { get; set; }

        /// <summary>
        /// ID of the task item.
        /// </summary>
        public int TaskItemId { get; set; }

        /// <summary>
        /// Name of the task (optional, for display).
        /// </summary>
        public string? TaskName { get; set; }

        /// <summary>
        /// Date of the time entry.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Hours worked.
        /// </summary>
        public decimal HoursWorked { get; set; }
    }
}
