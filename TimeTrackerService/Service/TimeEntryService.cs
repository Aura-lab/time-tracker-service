using FluentValidation;
using Microsoft.EntityFrameworkCore;
using TimeTrackerService.Dtos;
using TimeTrackerService.Models;
using TimeTrackerService.Repositories;

namespace TimeTrackerService.Services
{
    public class TimeEntryService : ITimeEntryService
    {
        private readonly IRepository<TimeEntry> _repo;
        private readonly IValidator<TimeEntryDto> _validator;
        private readonly IUnitOfWork _unitOfWork;

        public TimeEntryService(IRepository<TimeEntry> repo, IValidator<TimeEntryDto> validator, IUnitOfWork unitOfWork)
        {
            _repo = repo;
            _validator = validator;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<TimeEntryDto>> GetAllAsync()
        {
            var entries = await _repo.GetAllAsync(q => q
                .Include(e => e.Person)
                .Include(e => e.TaskItem));

            return entries.Select(MapToDto);
        }

        public async Task<TimeEntryDto?> GetByIdAsync(int id)
        {
            var entry = await _repo.GetByIdAsync(id, q => q
                .Include(e => e.Person)
                .Include(e => e.TaskItem));

            return entry == null ? null : MapToDto(entry);
        }

        public async Task<TimeEntryDto> CreateAsync(TimeEntryDto dto)
        {
            await _validator.ValidateAndThrowAsync(dto);

            var entry = new TimeEntry
            {
                PersonId = dto.PersonId,
                TaskItemId = dto.TaskItemId,
                Date = dto.Date,
                HoursWorked = dto.HoursWorked
            };

            await _repo.AddAsync(entry);
            await _unitOfWork.CommitAsync();

            return MapToDto(entry);
        }

        public async Task UpdateAsync(TimeEntryDto dto)
        {
            await _validator.ValidateAndThrowAsync(dto);

            var entry = await _repo.GetByIdAsync(dto.Id);
            if (entry == null)
            {
                throw new KeyNotFoundException("Entry not found.");
            }

            entry.PersonId = dto.PersonId;
            entry.TaskItemId = dto.TaskItemId;
            entry.Date = dto.Date;
            entry.HoursWorked = dto.HoursWorked;

            await _repo.UpdateAsync(entry);
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entry = await _repo.GetByIdAsync(id);
            if (entry != null)
            {
                await _repo.DeleteAsync(entry);
                await _unitOfWork.CommitAsync();
            }
        }

        private TimeEntryDto MapToDto(TimeEntry entry)
        {
            return new TimeEntryDto
            {
                Id = entry.Id,
                PersonId = entry.PersonId,
                PersonName = entry.Person?.FullName,
                TaskItemId = entry.TaskItemId,
                TaskName = entry.TaskItem?.Name,
                Date = entry.Date,
                HoursWorked = entry.HoursWorked
            };
        }
    }
}
