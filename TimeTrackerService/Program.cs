using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using TimeTrackerService.Data;
using TimeTrackerService.Dtos;
using TimeTrackerService.Filters;
using TimeTrackerService.Repositories;
using TimeTrackerService.Services;
using TimeTrackerService.Validators;

var builder = WebApplication.CreateBuilder(args);

// Add DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// Add repositories
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

// Add validators
builder.Services.AddScoped<IValidator<TimeEntryDto>, TimeEntryValidator>();

// Add controllers
builder.Services.AddControllers(options =>
{
    options.Filters.Add<ApiExceptionFilter>();
})
.AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
});


// Add Swagger with XML comments
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Time Tracker API",
        Version = "v1",
        Description = "API for tracking time entries, people, and tasks.",
        Contact = new OpenApiContact
        {
            Name = "Aura Wang",
            Email = "Aura.Wang@master.com"
        }
    });

    // Set the comments path for the Swagger JSON and UI.
    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath))
    {
        options.IncludeXmlComments(xmlPath);
    }
});

// Add CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocal", policy =>
    {
        policy.WithOrigins("http://localhost:4200", "http://localhost:5173")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddScoped<ITimeEntryService, TimeEntryService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

var app = builder.Build();

// Configure middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Time Tracker API V1");
        c.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();

app.UseCors("AllowLocal");

app.UseAuthorization();

app.MapControllers();

app.Run();

namespace TimeTrackerService
{
    public partial class Program { }
}
