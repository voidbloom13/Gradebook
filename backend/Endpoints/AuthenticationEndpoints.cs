using Backend.Data;
using Backend.Services;

namespace Backend.Endpoints;

public static class AuthenticationEndpoints
{
    public static IEndpointRouteBuilder MapAuthenticationEndpoints(
        this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/auth/session", (HttpContext ctx) =>
        {
            var result = AuthenticationService.ValidateSessionService(ctx);
            return result;
        });

        app.MapPost("/api/auth/login", async (HttpContext ctx, AppDbContext db) =>
        {
            var result = await AuthenticationService.LoginUserService(ctx, db);
            return result;
        });

        app.MapPost("/api/auth/signup", async (HttpContext ctx, AppDbContext db, EmailVerificationService emailVerificationService) =>
        {
            var result = await AuthenticationService.CreateNewStudentService(ctx, db, emailVerificationService);
            return result;
        }); // admin routes will create new Teachers
        
        app.MapPost("/api/auth/logout", (HttpContext ctx) =>
        {
            var result = AuthenticationService.LogoutUserService(ctx);
            return result;
        });

        // TODO: Change password if User knows current password
        app.MapPost("/api/auth/change-password", (HttpContext ctx) =>
        {
          return;
        });

        // TODO: Change password if User forgot current password,
        // Authenticates with User.FirstName, User.LastName, and User.Email
        // prior to setting new password
        app.MapPost("/api/auth/forgot-password", (HttpContext ctx) =>
        {
          return;
        });

        // TODO: Verify password to ensure the User can reset password
        // Send a 4-8 digit code to User.Email and ask to confirm code
        app.MapPost("/api/auth/verify-email", (HttpContext ctx) =>
        {
          return;
        });

        return app;
    }
}