using Microsoft.EntityFrameworkCore;
using Backend.Data;
using Backend.Models;
using Backend.Enums;

namespace Backend.Services;

public static class SeedingService
{
    public static async Task SeedInitialDataAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        var passwordHasher = scope.ServiceProvider.GetRequiredService<IPasswordHasher>();

        await dbContext.Database.MigrateAsync();

        // Add seeding methods here
        await SeedAdminUserAsync(dbContext, passwordHasher);
        
        await dbContext.SaveChangesAsync();
    }

    private static async Task SeedAdminUserAsync(AppDbContext dbContext, IPasswordHasher passwordHasher)
    {
        if (!await dbContext.Users.AnyAsync(u => u.Role == Role.Admin))
        {
            dbContext.Users.Add(new Teacher
            {
                Id = Guid.NewGuid(),
                FirstName = "Admin",
                LastName = "Admin",
                Email = "admin@gradebook.local",
                PasswordHash = passwordHasher.Hash("@Admin123"),
                Role = Role.Admin,
                CreatedAt = DateTime.UtcNow
            });
        }
    }
}