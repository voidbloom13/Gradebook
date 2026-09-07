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
        return Results.Ok();
    }

    public static async Task<IResult> LoginUserService(HttpContext context, AppDbContext db)
    {
        // Creates and Validates loginRequest
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
        await CreateClaims.CreateUserClaims(context, user);
        return Results.Ok();
    }

    public static async Task<IResult> CreateNewStudentService(HttpContext context, AppDbContext db, EmailVerificationService emailVerificationService)
    {
        // Creates and Validates signupRequest
        var signupRequest = await context.Request.ReadFromJsonAsync<SignupRequestDto>();
        if (
            signupRequest == null
            || string.IsNullOrWhiteSpace(signupRequest.FirstName)
            || string.IsNullOrWhiteSpace(signupRequest.LastName)
            || string.IsNullOrWhiteSpace(signupRequest.Email)
            || string.IsNullOrWhiteSpace(signupRequest.Password)
        )
        {
            return Results.BadRequest();
        }

        // Check if email exists
        var emailExists = await db.Users.AnyAsync<User>(u => u.Email == signupRequest.Email);

        if (emailExists)
        {
            return Results.Conflict();
        }

        // Creates the user object and adds entry to DB
        var user = new Student
        {
            FirstName = signupRequest.FirstName,
            LastName = signupRequest.LastName,
            Email = signupRequest.Email,
            IsEmailVerified = false,
            RequirePasswordChange = false,
            IsDisabled = false,
            Role = Enums.Role.Student,
            CreatedAt = DateTime.UtcNow
        };
        var passwordHasher = new PasswordHasherService();
        user.PasswordHash = passwordHasher.Hash(signupRequest.Password);
        db.Users.Add(user);
        await db.SaveChangesAsync();

        // Create Claims, ClaimsIdentity, and ClaimsPrincipal from User object, Email verification code.
        await CreateClaims.CreateUserClaims(context, user);
        var verificationCode = await emailVerificationService.GenerateCode(user);


        return Results.Created("/api/auth/signup",new
        {
            user = user.Id,
            verificationCode
        });
    }

    public static async Task LogoutUserService(HttpContext context)
    {
        await context.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return;
    }
}