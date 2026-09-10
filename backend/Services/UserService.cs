using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Backend.Data;
using Backend.Dtos;
using Backend.Models;

namespace Backend.Services;

public static class UserService
{
    public static async Task<IResult> GetEmailAsync(HttpContext context, AppDbContext db)
    {
        var userIdClaim = context.User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (
            context.User.Identity?.IsAuthenticated != true
            || !Guid.TryParse(userIdClaim, out var userId))
        {
            return Results.Unauthorized();
        }
        var user = await db.Users.FirstOrDefaultAsync<User>(u => u.Id == userId);
        if (user == null)
        {
            return Results.Unauthorized();
        }

        return Results.Ok(new {
            emailAddress = user.Email
        });
    }

    public static async Task<IResult> UpdateEmailAsync(HttpContext context, AppDbContext db)
    {
        return Results.Ok();
    }

    public static async Task<IResult> ResetPasswordAsync(HttpContext context, AppDbContext db)
    {
        return Results.Ok();
    }

    public static async Task<IResult> ForgotPasswordAsync(HttpContext context, AppDbContext db)
    {
        return Results.Ok();
    }
}