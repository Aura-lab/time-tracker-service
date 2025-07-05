using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using TimeTrackerService.Dtos;

namespace TimeTrackerService.Filters
{
    /// <summary>
    /// Global exception filter to handle FluentValidation and other exceptions,
    /// and return uniform ApiResponse format.
    /// </summary>
    public class ApiExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is ValidationException validationException)
            {
                var errors = validationException.Errors
                    .GroupBy(e => e.PropertyName)
                    .ToDictionary(
                        g => g.Key,
                        g => g.Select(e => e.ErrorMessage).ToArray()
                    );

                var apiResponse = new ApiResponse<string>(
                    null!,
                    "Validation failed",
                    400
                )
                {
                    Errors = errors
                };

                context.Result = new BadRequestObjectResult(apiResponse);
                context.ExceptionHandled = true;
                return;
            }

            var genericResponse = new ApiResponse<string>(
                null!,
                "An unexpected error occurred.",
                500
            );

            context.Result = new ObjectResult(genericResponse)
            {
                StatusCode = 500
            };
            context.ExceptionHandled = true;
        }
    }
}
