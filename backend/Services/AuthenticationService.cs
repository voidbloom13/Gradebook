using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Backend.Data;
using Backend.Dtos;
using Backend.Models;
using System.Security.Claims;

namespace Backend.Services;

public static class AuthenticationService
{
    public static async Task<IResult> ValidateSessionAsync(HttpContext context, AppDbContext db)
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

        return Results.Ok(new
        {
            name = user.FirstName + " " + user.LastName,
            isEmailVerified = user.IsEmailVerified,
            requirePasswordChange = user.RequirePasswordChange
        });
    }

    public static async Task<IResult> LoginAsync(HttpContext context, AppDbContext db)
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

    public static async Task<IResult> SignupStudentAsync(HttpContext context, AppDbContext db, EmailVerificationService emailVerificationService)
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
        await emailVerificationService.GenerateCodeAsync(user);

        return Results.Created();
    }

    public static async Task<IResult> LogoutAsync(HttpContext context)
    {
        await context.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return Results.Ok();
    }

    public static async Task<IResult> GenerateEmailVerificationCodeAsync(HttpContext context, AppDbContext db, EmailVerificationService emailVerificationService)
    {
        var userIdClaim = context.User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (!Guid.TryParse(userIdClaim, out var userId))
        {
            return Results.Unauthorized();
        }
        var user = await db.Users.FirstOrDefaultAsync<User>(u => u.Id == userId);
        if (user == null)
        {
            return Results.Unauthorized();
        }

        await emailVerificationService.GenerateCodeAsync(user);

        return Results.Ok(new
        {
            message = "New Email verification code generated successfully."
        });
    }

    public static async Task<IResult> VerifyEmailVerificationCodeAsync(HttpContext context, AppDbContext db, EmailVerificationService emailVerificationService)
    {
        var userIdClaim = context.User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (!Guid.TryParse(userIdClaim, out var userId))
        {
            return Results.Unauthorized();
        }
        var user = await db.Users.FirstOrDefaultAsync<User>(u => u.Id == userId);
        if (user == null)
        {
            return Results.Unauthorized();
        }

        if (user.IsEmailVerified)
        {
            return Results.Ok(new
            {
                message = "Email is already verified"
            });
        }

        var request = await context.Request.ReadFromJsonAsync<EmailVerificationCodeDto>();
        if (
            request == null
            || string.IsNullOrWhiteSpace(request.code)
            || request.code.Length != 6
            || !request.code.All(char.IsDigit)
        )
        {
            return Results.BadRequest(new
            {
                message = "Code is required and must be 6 digits."
            });
        }
        var code = request.code;

        var emailVerificationSuccessful = await emailVerificationService.VerifyCodeAsync(code, user);
        if (!emailVerificationSuccessful)
        {
            return Results.BadRequest(new
            {
                message = "Unable to verify email."
            });
        }
        return Results.Ok(new
        {
            message = "Email verification successful."
        });
    }   

    public static async Task<IResult> ChangePasswordAsync(HttpContext context)
    {
        return Results.Ok();
    }

    public static async Task<IResult> ForgotPasswordAsync(HttpContext context)
    {
        return Results.Ok();
    }
}