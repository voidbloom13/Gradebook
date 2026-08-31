using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Backend.Data;
using Backend.Endpoints;
// using Backend.Enums;
// using Backend.Models;
using Backend.Services;

var builder = WebApplication.CreateBuilder(args);

var connectionString =
    builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException(
        "Connection String 'DefaultConnection' not found."
    );

builder.Services.AddSingleton<IPasswordHasher, PasswordHasherService>();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString)  
);

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

await app.SeedInitialDataAsync();
// using (var scope = app.Services.CreateScope())
// {
//     var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
//     var passwordHasher = scope.ServiceProvider.GetRequiredService<IPasswordHasher>();
//     await dbContext.Database.MigrateAsync();
//     if (!await dbContext.Users.AnyAsync(u => u.Role == Role.Admin))
//     {
//         dbContext.Users.Add(new Teacher
//         {
//             Id = Guid.NewGuid(),
//             FirstName = "Admin",
//             LastName = "Admin",
//             Email = "admin@gradebook.local",
//             PasswordHash = passwordHasher.Hash("@Admin123"),
//             Role = Role.Admin,
//             CreatedAt = DateTime.UtcNow
//         });
        
//         await dbContext.SaveChangesAsync();
//     }
// };

app.Run();
