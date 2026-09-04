using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Backend.Data;
using Backend.Dtos;
using Backend.Models;

namespace Backend.Services;

public static class AuthenticationService
{
    public static IResult ValidateSessionService(HttpContext context)
    {
        if (!context.User.Identity?.IsAuthenticated ?? true)
            return Results.Unauthorized();
        return Results.Ok(new
        {
            Name = context.User.Identity?.Name
        });
    }

    public static async Task<IResult> LoginUserService(HttpContext context, AppDbContext db)
    {
        // Create loginRequest object and verify credentials exist
        var loginRequest = await context.Request.ReadFromJsonAsync<LoginRequestDto>();
        if (
            loginRequest == null 
            || string.IsNullOrWhiteSpace(loginRequest.Email) 
            || string.IsNullOrWhiteSpace(loginRequest.Password)
        )
        {
            return Results.BadRequest();
        }

        // Retrieve user from DB and validate password
        var user = await db.Users
            .SingleOrDefaultAsync<User>(u => u.Email == loginRequest.Email);

        if (user == null)
        {
            return Results.Unauthorized();
        }
        if (user.IsDisabled)
        {
            return Results.Forbid();
        }

        var passwordHasher = new PasswordHasherService();
        var verificationResult = passwordHasher.Verify(loginRequest.Password, user.PasswordHash!);
        if (!verificationResult)
        {
            return Results.Unauthorized();
        }
        
        // Create Claims, ClaimsIdentity, and ClaimsPrincipal from User object
        var claims = new List<Claim>
        {
            new Claim(
                ClaimTypes.Name,
                $"{user.LastName}, {user.FirstName}"
            ),
            new Claim(
                ClaimTypes.Email,
                user.Email
            ),
            new Claim(
                ClaimTypes.Role,
                user.Role.ToString()
            )
        };
        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

        // SignInAsync() and return Ok
        await context.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);
        return Results.Ok();
    }

    public static IResult CreateNewStudentService(HttpContext context)
    {
        return Results.Ok();
    }

    public static async Task LogoutUserService(HttpContext context)
    {
        await context.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return;
    }
}