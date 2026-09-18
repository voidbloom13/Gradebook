using Backend.Data;
using Backend.Services;
using Backend.Services.Endpoints;
using Backend.Services.Email;

namespace Backend.Endpoints;

public static class AuthenticationEndpoints
{
    public static IEndpointRouteBuilder MapAuthenticationEndpoints(
        this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/auth/session", async (HttpContext ctx, AppDbContext db) =>
        {
            var result = await AuthenticationEndpointService.ValidateSessionAsync(ctx, db);
            return result;
        });

        app.MapPost("/api/auth/login", async (HttpContext ctx, AppDbContext db) =>
        {
            var result = await AuthenticationEndpointService.LoginAsync(ctx, db);
            return result;
        });

        app.MapPost("/api/auth/signup", async (HttpContext ctx, AppDbContext db, EmailVerificationService emailVerificationService) =>
        {
            var result = await AuthenticationEndpointService.SignupStudentAsync(ctx, db, emailVerificationService);
            return result;
        }); // admin routes will create new Teachers
        
        app.MapPost("/api/auth/logout", async (HttpContext ctx) =>
        {
            var result = AuthenticationEndpointService.LogoutAsync(ctx);
            return result;
        })
        .RequireAuthorization();

        app.MapPost("/api/auth/generate-email-verification-code", async (HttpContext ctx, AppDbContext db, EmailVerificationService emailVerificationService) =>
        {
            var result = await AuthenticationEndpointService.GenerateEmailVerificationCodeAsync(ctx, db, emailVerificationService);
            return result;
        })
        .RequireAuthorization();

        // TODO: Verify password to ensure the User can reset password
        // Send a 4-8 digit code to User.Email and ask to confirm code
        app.MapPost("/api/auth/verify-email", async (HttpContext ctx, AppDbContext db, EmailVerificationService emailVerificationService) =>
        {
            var result = await AuthenticationEndpointService.VerifyEmailVerificationCodeAsync(ctx, db, emailVerificationService);
            return result;
        })
        .RequireAuthorization();

        return app;
    }
}