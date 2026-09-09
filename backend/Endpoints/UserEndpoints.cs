using Backend.Data;
using Backend.Services;

namespace Backend.Endpoints;

public static class UserEndpoints
{
    public static IEndpointRouteBuilder MapUserEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/user/get-email", async (HttpContext ctx, AppDbContext db) =>
        {
            var result = await UserService.GetEmailAsync(ctx, db);
            return result;
        });

        app.MapPost("/api/user/change-email", async (HttpContext ctx, AppDbContext db) =>
        {
            var result = await UserService.ChangeEmailAsync(ctx, db);
            return result;
        });

        app.MapPost("/api/user/change-password", async (HttpContext ctx, AppDbContext db) =>
        {
            var result = await UserService.ChangePasswordAsync(ctx, db);
            return result;
        });

        app.MapPost("/api/user/forgot-password", async (HttpContext ctx, AppDbContext db) =>
        {
            var result = await UserService.ForgotPasswordAsync(ctx, db);
            return result;
        });
    }
}