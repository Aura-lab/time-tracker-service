using TimeTrackerService.Dtos;

namespace TimeTrackerService.Services
{
    /// <summary>
    /// Defines the service interface for managing time entries.
    /// </summary>
    public interface ITimeEntryService
    {
        /// <summary>
        /// Retrieves all time entries.
        /// </summary>
        Task<IEnumerable<TimeEntryDto>> GetAllAsync();

        /// <summary>
        /// Retrieves a time entry by its ID.
        /// </summary>
        Task<TimeEntryDto?> GetByIdAsync(int id);

        /// <summary>
        /// Creates a new time entry.
        /// </summary>
        Task<TimeEntryDto> CreateAsync(TimeEntryDto dto);

        /// <summary>
        /// Updates an existing time entry.
        /// </summary>
        Task UpdateAsync(TimeEntryDto dto);

        /// <summary>
        /// Deletes a time entry by ID.
        /// </summary>
        Task DeleteAsync(int id);
    }
}
