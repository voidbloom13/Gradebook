using Backend.Services;

namespace Backend.Endpoints;

public static class AuthenticationEndpoints
{
    public static IEndpointRouteBuilder MapAuthenticationEndpoints(
        this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/auth/session", ValidateSession);
        app.MapPost("/api/auth/login", LoginUser);
        app.MapPost("/api/auth/signup", CreateNewStudent); // admin routes will create new Teachers
        app.MapPost("/api/auth/logout", LogoutUser);

        return app;
    }

    private static IResult ValidateSession()
    {
        return AuthenticationService.ValidateSessionService(context);
    }

    private static IResult LoginUser()
    {
        return AuthenticationService.LoginUserService(context);
    }

    private static IResult CreateNewStudent()
    {
        return AuthenticationService.CreateNewStudentService(context);
    }

    private static IResult LogoutUser()
    {
        return AuthenticationServiceLogoutUserService(context);
    }
}