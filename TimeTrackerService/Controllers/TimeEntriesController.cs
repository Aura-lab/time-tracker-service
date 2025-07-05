using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TimeTrackerService.Data;
using TimeTrackerService.Dtos;
using TimeTrackerService.Services;

namespace TimeTrackerService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class TimeEntriesController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ITimeEntryService _service;

        public TimeEntriesController(ITimeEntryService service, AppDbContext context)
        {
            _service = service;
            _context = context;
        }

        /// <summary>
        /// Get all time entries.
        /// </summary>
        /// <returns>List of time entries wrapped in ApiResponse.</returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var entries = await _service.GetAllAsync();
            return Ok(new ApiResponse<IEnumerable<TimeEntryDto>>(entries, "Fetched successfully", 200));
        }

        /// <summary>
        /// Get a single time entry by ID.
        /// </summary>
        /// <param name="id">The ID of the time entry.</param>
        /// <returns>The time entry if found; otherwise, 404 response.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var entry = await _service.GetByIdAsync(id);
            if (entry == null)
                return NotFound(new ApiResponse<string>(null!, "Entry not found", 404));

            return Ok(new ApiResponse<TimeEntryDto>(entry, "Fetched successfully", 200));
        }

        /// <summary>
        /// Create a new time entry.
        /// </summary>
        /// <param name="dto">The time entry data transfer object.</param>
        /// <returns>The created time entry wrapped in ApiResponse.</returns>
        [HttpPost]
        public async Task<IActionResult> Post(TimeEntryDto dto)
        {
            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(Get), new { id = created.Id }, new ApiResponse<TimeEntryDto>(created, "Created successfully", 201));
        }

        /// <summary>
        /// Update an existing time entry.
        /// </summary>
        /// <param name="id">The ID of the time entry to update.</param>
        /// <param name="dto">The updated time entry data transfer object.</param>
        /// <returns>ApiResponse indicating success or failure.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, TimeEntryDto dto)
        {
            if (id != dto.Id)
                return BadRequest(new ApiResponse<string>(null!, "ID mismatch", 400));

            await _service.UpdateAsync(dto);
            return Ok(new ApiResponse<string>("", "Updated successfully", 200));
        }

        /// <summary>
        /// Delete a time entry by ID.
        /// </summary>
        /// <param name="id">The ID of the time entry to delete.</param>
        /// <returns>ApiResponse indicating deletion result.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return Ok(new ApiResponse<string>("", "Deleted successfully", 200));
        }
    }
}
