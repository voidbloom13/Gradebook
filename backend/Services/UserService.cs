namespace Backend.Services;

public static class AuthenticationService
{
    public static async Task<IResult> GetEmailAsync(HttpContext context, AppDbContext db)
    {
        
        return Results.Ok(new
        {
            email: ""
        })
    }

    public static async Task<IResult> ChangeEmailAsync(HttpContext context, AppDbContext db)
    {
        return Results.Ok();
    }

    public static async Task<IResult> ChangePasswordAsync(HttpContext context, AppDbContext db)
    {
        return Results.Ok();
    }

    public static async Task<IResult> ForgotPasswordAsync(HttpContext context, AppDbContext db)
    {
        return Results.Ok();
    }
}