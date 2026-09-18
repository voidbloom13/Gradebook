using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Backend.Data;
using Backend.Dtos;
using Backend.Models;
using Backend.Services.Helpers;

namespace Backend.Services.Endpoints;

public static class UserEndpointService
{
    public static async Task<IResult> InitDashboard(HttpContext context, AppDbContext db)
    {
        var user = await UserService.GetUserAsync(context, db);
        if (user == null)
        {
            return Results.Unauthorized();
        }

        return Results.Ok(new
        {
            isEmailVerified = user.IsEmailVerified,
            requirePasswordChange = user.RequirePasswordChange
        });
    }

    public static async Task<IResult> InitVerifyEmailAsync(HttpContext context, AppDbContext db, EmailVerificationService emailVerificationService)
    {
        var user = await UserService.GetUserAsync(context, db);
        if (user == null)
        {
            return Results.Unauthorized();
        }

        // Check if current code exists. If not, then generate one
        var code = await emailVerificationService.GetValidCodeAsync(user);
        if (code == null)
        {
            await emailVerificationService.GenerateCodeAsync(user);
        }

        return Results.Ok(new {
            emailAddress = user.Email,
            resendAvailableAt = DateTime.UtcNow
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