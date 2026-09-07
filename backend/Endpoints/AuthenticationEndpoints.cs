using Backend.Data;
using Backend.Services;

namespace Backend.Endpoints;

public static class AuthenticationEndpoints
{
    public static IEndpointRouteBuilder MapAuthenticationEndpoints(
        this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/auth/session", async (HttpContext ctx, AppDbContext db) =>
        {
            var result = await AuthenticationService.ValidateSession(ctx, db);
            return result;
        });

        app.MapPost("/api/auth/login", async (HttpContext ctx, AppDbContext db) =>
        {
            var result = await AuthenticationService.Login(ctx, db);
            return result;
        });

        app.MapPost("/api/auth/signup", async (HttpContext ctx, AppDbContext db, EmailVerificationService emailVerificationService) =>
        {
            var result = await AuthenticationService.SignupStudent(ctx, db, emailVerificationService);
            return result;
        }); // admin routes will create new Teachers
        
        app.MapPost("/api/auth/logout", async (HttpContext ctx) =>
        {
            var result = AuthenticationService.Logout(ctx);
            return result;
        });

        // TODO: Change password if User knows current password
        app.MapPost("/api/auth/change-password", async (HttpContext ctx) =>
        {
            return;
        });

        // TODO: Change password if User forgot current password,
        // Authenticates with User.FirstName, User.LastName, and User.Email
        // prior to setting new password
        app.MapPost("/api/auth/forgot-password", async (HttpContext ctx) =>
        {
            return;
        });

        // TODO: Verify password to ensure the User can reset password
        // Send a 4-8 digit code to User.Email and ask to confirm code
        app.MapPost("/api/auth/verify-email", async (HttpContext ctx, AppDbContext db, EmailVerificationService emailVerificationService) =>
        {
            var result = await AuthenticationService.VerifyEmail(ctx, db, emailVerificationService);
            return result;
        })
        .RequireAuthorization();

        app.MapGet("/api/auth/resend-email-verification-code", async (HttpContext ctx, AppDbContext db, EmailVerificationService emailVerificationService) =>
        {
            var result = await AuthenticationService.ResendEmailVerificationCode(ctx, db, emailVerificationService);
            return result;
        });

        return app;
    }
}