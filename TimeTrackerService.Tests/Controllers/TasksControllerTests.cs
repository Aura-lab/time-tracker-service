using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using System.Net.Http;
using System.Threading.Tasks;
using TimeTrackerService;

namespace TimeTrackerService.Tests.Controllers
{
    public class TasksControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public TasksControllerTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetTasks_ReturnsSuccessStatusCode()
        {
            var response = await _client.GetAsync("/api/Tasks");
            response.EnsureSuccessStatusCode();
        }
    }
}
