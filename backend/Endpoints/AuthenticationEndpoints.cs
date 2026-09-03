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
            return AuthenticationService.ValidateSessionService(ctx);
        });

        app.MapPost("/api/auth/login", (HttpContext ctx, AppDbContext db) =>
        {
            return AuthenticationService.LoginUserService(ctx, db);
        });

        app.MapPost("/api/auth/signup", (HttpContext ctx) =>
        {
            return AuthenticationService.CreateNewStudentService(ctx);
        }); // admin routes will create new Teachers
        
        app.MapPost("/api/auth/logout", (HttpContext ctx) =>
        {
            return AuthenticationService.LogoutUserService(ctx);
        });

        return app;
    }
}