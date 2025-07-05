using Microsoft.AspNetCore.Mvc;
using TimeTrackerService.Dtos;
using TimeTrackerService.Models;
using TimeTrackerService.Repositories;

namespace TimeTrackerService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class PeopleController : ControllerBase
    {
        private readonly IRepository<Person> _repository;

        public PeopleController(IRepository<Person> repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Get all people.
        /// </summary>
        /// <returns>List of people wrapped in ApiResponse.</returns>
        [HttpGet]
        public async Task<IActionResult> GetPeople()
        {
            var people = await _repository.GetAllAsync();
            var dtoList = people.Select(p => new PersonDto
            {
                Id = p.Id,
                FullName = p.FullName
            });

            return Ok(new ApiResponse<IEnumerable<PersonDto>>(dtoList, "Fetched successfully", 200));
        }
    }
}
