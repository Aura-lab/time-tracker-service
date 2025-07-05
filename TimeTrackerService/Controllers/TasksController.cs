using Microsoft.AspNetCore.Mvc;
using TimeTrackerService.Dtos;
using TimeTrackerService.Models;
using TimeTrackerService.Repositories;

namespace TimeTrackerService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class TasksController : ControllerBase
    {
        private readonly IRepository<TaskItem> _repository;

        public TasksController(IRepository<TaskItem> repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Get all task items.
        /// </summary>
        /// <returns>List of tasks wrapped in ApiResponse.</returns>
        [HttpGet]
        public async Task<IActionResult> GetTasks()
        {
            var tasks = await _repository.GetAllAsync();
            var dtoList = tasks.Select(t => new TaskItemDto
            {
                Id = t.Id,
                Name = t.Name,
                Description = t.Description
            });

            return Ok(new ApiResponse<IEnumerable<TaskItemDto>>(dtoList, "Fetched successfully", 200));
        }
    }
}
