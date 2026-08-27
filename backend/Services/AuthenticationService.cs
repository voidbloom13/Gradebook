namespace Backend.Services;

public static class AuthenticationService
{
    public static IResult ValidateSessionService(HttpContext context)
    {
        if (context.User.Identity?.IsAuthenticated ?? false)
            return Results.Unauthorized();
        return Results.Ok(new
        {
            Name = context.User.Identity?.Name
        });
    }

    public static IResult LoginUserService(HttpContext context)
    {
        // Validate user inputs
        // Create Claims, ClaimsIdentity, and ClaimsPrincipal from User object
        // SignInAsync()
        // Create Cookie
        return Results.Ok();
    }

    public static IResult CreateNewStudentService(HttpContext context)
    {
        return Results.Ok();
    }

    public static IResult LogoutUserService(HttpContext context)
    {
        return Results.Ok();
    }
}