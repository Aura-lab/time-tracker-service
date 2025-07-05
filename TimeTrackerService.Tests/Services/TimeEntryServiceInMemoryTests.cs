using System;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using TimeTrackerService.Data;
using TimeTrackerService.Dtos;
using TimeTrackerService.Models;
using TimeTrackerService.Repositories;
using TimeTrackerService.Services;
using TimeTrackerService.Validators;
using Xunit;

namespace TimeTrackerService.Tests.Services
{
    public class TimeEntryServiceInMemoryTests
    {
        private AppDbContext CreateDbContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var context = new AppDbContext(options);
            context.Database.EnsureCreated();
            return context;
        }

        private TimeEntryService CreateService(AppDbContext context)
        {
            var repository = new Repository<TimeEntry>(context);
            var validator = new TimeEntryValidator();
            var unitOfWork = new UnitOfWork(context);
            return new TimeEntryService(repository, validator, unitOfWork);
        }

        [Fact]
        public async Task CreateAsync_AddsEntry()
        {
            var context = CreateDbContext();
            var service = CreateService(context);

            var dto = new TimeEntryDto { PersonId = 1, TaskItemId = 1, Date = DateTime.Today, HoursWorked = 3 };

            var result = await service.CreateAsync(dto);

            var dbEntry = await context.TimeEntries.FirstOrDefaultAsync();
            Assert.NotNull(dbEntry);
            Assert.Equal(dto.PersonId, dbEntry.PersonId);
            Assert.Equal(dto.HoursWorked, dbEntry.HoursWorked);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsEntries()
        {
            var context = CreateDbContext();
            context.TimeEntries.Add(new TimeEntry { PersonId = 1, TaskItemId = 1, Date = DateTime.Today, HoursWorked = 5 });
            await context.SaveChangesAsync();

            var service = CreateService(context);

            var entries = await service.GetAllAsync();

            Assert.Single(entries);
            Assert.Equal(5, entries.First().HoursWorked);
        }

        [Fact]
        public async Task UpdateAsync_UpdatesEntry()
        {
            var context = CreateDbContext();
            var entry = new TimeEntry { PersonId = 1, TaskItemId = 1, Date = DateTime.Today, HoursWorked = 2 };
            context.TimeEntries.Add(entry);
            await context.SaveChangesAsync();

            var service = CreateService(context);

            var dto = new TimeEntryDto
            {
                Id = entry.Id,
                PersonId = entry.PersonId,
                TaskItemId = entry.TaskItemId,
                Date = entry.Date,
                HoursWorked = 8
            };

            await service.UpdateAsync(dto);

            var updated = await context.TimeEntries.FindAsync(entry.Id);
            Assert.Equal(8, updated!.HoursWorked);
        }

        [Fact]
        public async Task DeleteAsync_RemovesEntry()
        {
            var context = CreateDbContext();
            var entry = new TimeEntry { PersonId = 1, TaskItemId = 1, Date = DateTime.Today, HoursWorked = 1 };
            context.TimeEntries.Add(entry);
            await context.SaveChangesAsync();

            var service = CreateService(context);

            await service.DeleteAsync(entry.Id);

            var dbEntry = await context.TimeEntries.FindAsync(entry.Id);
            Assert.Null(dbEntry);
        }

        [Fact]
        public async Task CreateAsync_InvalidDto_ThrowsValidationException()
        {
            var context = CreateDbContext();
            var service = CreateService(context);

            var dto = new TimeEntryDto
            {
                PersonId = 0,
                TaskItemId = 0,
                Date = DateTime.Today.AddDays(1), // Future date
                HoursWorked = 0
            };

            await Assert.ThrowsAsync<ValidationException>(() => service.CreateAsync(dto));
        }
    }
}
