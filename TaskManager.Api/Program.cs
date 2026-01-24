using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Text;
using TaskManager.Api.Middleware;
using TaskManager.Application.Features.Tasks.Commands;
using TaskManager.Application.Interfaces;
using TaskManager.Infrastructure.Identity;
using TaskManager.Infrastructure.Persistence;
using TaskManager.Infrastructure.Persistence.DbEntities;
using TaskManager.Infrastructure.Persistence.Mappings;
using TaskManager.Infrastructure.Repositories;

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

        //builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
        //    .AddEntityFrameworkStores<TaskManagerDbContext>()
        //    .AddDefaultTokenProviders();

        builder.Services.AddIdentityCore<ApplicationUser>(options =>
        {
            options.User.RequireUniqueEmail = true;
            options.Password.RequireDigit = true;
            options.Password.RequiredLength = 6;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = false;
        })
            .AddRoles<AspNetRole>()
            .AddEntityFrameworkStores<TaskManagerDbContext>()
            .AddDefaultTokenProviders();

        builder.Services.AddAuthentication("Bearer").AddJwtBearer("Bearer", options =>
        {
            options.TokenValidationParameters = new()
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = builder.Configuration["Jwt:Issuer"],
                ValidAudience = builder.Configuration["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!)
                )
            };
        });

        builder.Services.AddAuthorization();

        builder.Services.AddScoped<ITaskRepository, TaskRepository>();
        builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();

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

        app.UseAuthentication();

        app.UseAuthorization();

        app.MapControllers();

        app.UseMiddleware<ExceptionMiddleware>();

        app.Run();
    }
}