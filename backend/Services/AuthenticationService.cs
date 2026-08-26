namespace Backend.Services;

public static class AuthenticationService
{
    public static IResult ValidateSessionService(HttpContext context)
    {
        return Results.Ok();
    }

    public static IResult LoginUserService(HttpContext context)
    {
        return Results.Ok();
    }

    public static IResult LogoutUserService(HttpContext context)
    {
        return Results.Ok();
    }
}