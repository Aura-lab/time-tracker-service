using FluentValidation;
using TimeTrackerService.Dtos;

namespace TimeTrackerService.Validators
{
    /// <summary>
    /// Validator for TimeEntryDto.
    /// Ensures required fields and business rules are met before saving time entries.
    /// </summary>
    public class TimeEntryValidator : AbstractValidator<TimeEntryDto>
    {
        /// <summary>
        /// Initializes validation rules for TimeEntryDto.
        /// </summary>
        public TimeEntryValidator()
        {
            RuleFor(x => x.PersonId)
                .GreaterThan(0)
                .WithMessage("Person is required.");

            RuleFor(x => x.TaskItemId)
                .GreaterThan(0)
                .WithMessage("Task is required.");

            RuleFor(x => x.Date)
                .NotEmpty().WithMessage("Date is required.")
                .LessThanOrEqualTo(DateTime.Today).WithMessage("Date cannot be in the future.");

            RuleFor(x => x.HoursWorked)
                .InclusiveBetween(0.1m, 24m)
                .WithMessage("Hours worked must be between 0.1 and 24.");
        }
    }
}
