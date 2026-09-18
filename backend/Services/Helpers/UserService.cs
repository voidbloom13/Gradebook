using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Backend.Data;
using Backend.Models;

namespace Backend.Services.Helpers;

public class UserService
{
    public static async Task<User?> GetUserAsync(HttpContext context, AppDbContext db)
    {
        var userIdClaim = context.User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (
            context.User.Identity?.IsAuthenticated != true
            || !Guid.TryParse(userIdClaim, out var userId))
        {
            return null;
        }
        var user = await db.Users.FirstOrDefaultAsync<User>(u => u.Id == userId);
        return user;
    }
}