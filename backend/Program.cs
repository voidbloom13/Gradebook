using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Backend.Data;
using Backend.Endpoints;
using Backend.Enums;
using Backend.Services;

var builder = WebApplication.CreateBuilder(args);

var connectionString =
    builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException(
        "Connection String 'DefaultConnection' not found."
    );

builder.Services.AddSingleton<IPasswordHasher, PasswordHasherService>();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options
        .UseNpgsql(connectionString)
        .UseSeeding((context, _) =>
        {
            if (!context.Users.Any(u => u.Role == Role.Admin))
            {
                var passwordHasher = context.GetService<IPasswordHasher>();

                context.Users.Add(new User
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Admin",
                    LastName = "Admin",
                    Email = "admin@gradebook.local",
                    PasswordHash = passwordHasher.Hash("@Admin123"),
                    Role = Role.Admin,
                    CreatedAt = DateTime.UtcNow
                });
                
                context.SaveChanges();
            }
        }))
});

builder.Services
    .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.Cookie.Name = "Gradebook.Authentication";
        options.Events.OnRedirectToLogin = context =>
        {
            context.Response.StatusCode = 401;
            return Task.CompletedTask;
        };
        options.ExpireTimeSpan = TimeSpan.FromHours(4);
        options.SlidingExpiration = true;
    });

builder.Services.AddAuthorization();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/", () => "API is running...");
app.MapAuthenticationEndpoints();

using var scope = app.Services.CreateScope();
var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
dbContext.Database.Migrate();

app.Run();
