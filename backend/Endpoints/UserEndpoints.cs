using Backend.Data;
using Backend.Services;
using Backend.Services.Endpoints;

namespace Backend.Endpoints;

public static class UserEndpoints
{
    public static IEndpointRouteBuilder MapUserEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("/api/user/init-dashboard", async (HttpContext ctx, AppDbContext db) =>
        {
            var result = await UserEndpointService.InitDashboard(ctx, db);
            return result;
        });

        app.MapPost("/api/user/init-verify-email", async (HttpContext ctx, AppDbContext db, EmailVerificationService emailVerificationService) =>
        {
            var result = await UserEndpointService.InitVerifyEmailAsync(ctx, db, emailVerificationService);
            return result;
        });

        app.MapPost("/api/user/update-email", async (HttpContext ctx, AppDbContext db) =>
        {
            var result = await UserEndpointService.UpdateEmailAsync(ctx, db);
            return result;
        });

        app.MapPost("/api/user/reset-password", async (HttpContext ctx, AppDbContext db) =>
        {
            var result = await UserEndpointService.ResetPasswordAsync(ctx, db);
            return result;
        });

        app.MapPost("/api/user/forgot-password", async (HttpContext ctx, AppDbContext db) =>
        {
            var result = await UserEndpointService.ForgotPasswordAsync(ctx, db);
            return result;
        });

        return app;
    }
}