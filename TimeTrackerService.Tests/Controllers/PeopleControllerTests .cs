using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using System.Net.Http;
using System.Threading.Tasks;
using TimeTrackerService;

namespace TimeTrackerService.Tests.Controllers
{
    public class PeopleControllerTests : IClassFixture<WebApplicationFactory<TimeTrackerService.Program>>
    {
        private readonly HttpClient _client;

        public PeopleControllerTests(WebApplicationFactory<TimeTrackerService.Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetPeople_ReturnsSuccessStatusCode()
        {
            var response = await _client.GetAsync("/api/People");
            response.EnsureSuccessStatusCode();
        }
    }
}
