using Microsoft.EntityFrameworkCore;
using TaskManager.Api.Middleware;
using TaskManager.Application.Interfaces;
using TaskManager.Application.Tasks.Commands;
using TaskManager.Infrastructure.Persistence;
using TaskManager.Infrastructure.Persistence.Mappings;
using TaskManager.Infrastructure.Repositories;
using Serilog;

public partial class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddOpenApi();

        builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateTaskCommand).Assembly));

        builder.Services.AddAutoMapper(cfg => cfg.AddProfile<TaskMappingProfile>());

        builder.Services.AddDbContext<TaskManagerDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("TaskManagerConnectionString")));

        builder.Services.AddScoped<ITaskRepository, TaskRepository>();

        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .CreateLogger();

        builder.Host.UseSerilog();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.UseMiddleware<ExceptionMiddleware>();

        app.Run();
    }
}