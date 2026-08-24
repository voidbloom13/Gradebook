namespace Backend.Endpoints;

public static class AuthenticationEndpoints
{
    public static IEndpointRouteBuilder MapAuthenticationEndpoints(
        this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/auth/session", ValidateSession);
        app.MapPost("/api/auth/login", LoginUser);
        app.MapPost("/api/auth/logout", LogoutUser);

        return app;
    }

    private static IResult ValidateSession()
    {
        return; // Validate cookie/session, return 200(frontend/{role}/dashboard)/401(frontend/login)
    }

    private static IResult LoginUser()
    {
        return; // Validates user credentials, creates a new Session, creates and returns cookie
    }

    private static IResult ValidateSession()
    {
        return; // Adds RevokedAt to Session, force-reload to fail ValidateSession()
    }
}