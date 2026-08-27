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
            Console.WriteLine(result);
            return result;
        });

        app.MapPost("/api/auth/login", (HttpContext ctx) =>
        {
            var result = AuthenticationService.LoginUserService(ctx);
            Console.WriteLine(result);
            return result;
        });

        app.MapPost("/api/auth/signup", (HttpContext ctx) =>
        {
            var result = AuthenticationService.CreateNewStudentService(ctx);
            Console.WriteLine(result);
            return result;
        }); // admin routes will create new Teachers
        
        app.MapPost("/api/auth/logout", (HttpContext ctx) =>
        {
            var result = AuthenticationService.LogoutUserService(ctx);
            Console.WriteLine(result);
            return result;
        });

        return app;
    }
}