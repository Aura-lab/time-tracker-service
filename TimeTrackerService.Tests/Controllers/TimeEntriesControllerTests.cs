using System;
using System.Net.Http.Json;
using System.Threading.Tasks;
using TimeTrackerService.Dtos;
using Xunit;
using Microsoft.AspNetCore.Mvc.Testing;
using TimeTrackerService;
{
    
}

namespace TimeTrackerService.Tests.Controllers
{
    public class TimeEntriesControllerTests : IClassFixture<WebApplicationFactory<TimeTrackerService.Program>>
    {
        private readonly HttpClient _client;

        public TimeEntriesControllerTests(WebApplicationFactory<TimeTrackerService.Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetTimeEntries_ReturnsSuccessStatusCode()
        {
            var response = await _client.GetAsync("/api/TimeEntries");
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task PostTimeEntry_ReturnsCreated()
        {
            var newEntry = new TimeEntryDto
            {
                PersonId = 1,
                TaskItemId = 1,
                Date = DateTime.Today,
                HoursWorked = 2
            };

            var response = await _client.PostAsJsonAsync("/api/TimeEntries", newEntry);
            response.EnsureSuccessStatusCode();
            Assert.Equal(System.Net.HttpStatusCode.Created, response.StatusCode);
        }

        [Fact]
        public async Task PutTimeEntry_ReturnsOk()
        {
            var newEntry = new TimeEntryDto
            {
                PersonId = 1,
                TaskItemId = 1,
                Date = DateTime.Today,
                HoursWorked = 2
            };

            var createResponse = await _client.PostAsJsonAsync("/api/TimeEntries", newEntry);
            createResponse.EnsureSuccessStatusCode();
            var createdApiResponse = await createResponse.Content.ReadFromJsonAsync<ApiResponse<TimeEntryDto>>();
            var createdEntry = createdApiResponse!.Data;

            var updateEntry = new TimeEntryDto
            {
                Id = createdEntry.Id,
                PersonId = createdEntry.PersonId,
                TaskItemId = createdEntry.TaskItemId,
                Date = createdEntry.Date,
                HoursWorked = 4
            };

            var putResponse = await _client.PutAsJsonAsync($"/api/TimeEntries/{createdEntry.Id}", updateEntry);
            Assert.Equal(System.Net.HttpStatusCode.OK, putResponse.StatusCode);
        }

        [Fact]
        public async Task DeleteTimeEntry_ReturnsOk_AndNotFoundAfter()
        {
            var newEntry = new TimeEntryDto
            {
                PersonId = 1,
                TaskItemId = 1,
                Date = DateTime.Today,
                HoursWorked = 1
            };

            var createResponse = await _client.PostAsJsonAsync("/api/TimeEntries", newEntry);
            createResponse.EnsureSuccessStatusCode();
            var createdApiResponse = await createResponse.Content.ReadFromJsonAsync<ApiResponse<TimeEntryDto>>();
            var createdEntry = createdApiResponse!.Data;

            var deleteResponse = await _client.DeleteAsync($"/api/TimeEntries/{createdEntry.Id}");
            Assert.Equal(System.Net.HttpStatusCode.OK, deleteResponse.StatusCode);

            var getResponse = await _client.GetAsync($"/api/TimeEntries/{createdEntry.Id}");
            Assert.Equal(System.Net.HttpStatusCode.NotFound, getResponse.StatusCode);
        }
    }
}
