using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Backend.Data;
using Backend.Endpoints;
using Backend.Services;

var builder = WebApplication.CreateBuilder(args);

var connectionString =
    builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException(
        "Connection String 'DefaultConnection' not found."
    );

builder.Services.AddSingleton<IPasswordHasher, PasswordHasherService>();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString)  
);

builder.Services
    .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.Cookie.HttpOnly = true;
        options.Cookie.Name = "GradebookAuth";
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
        options.Events.OnRedirectToLogin = context =>
        {
            context.Response.StatusCode = 401;
            return Task.CompletedTask;
        };
        options.ExpireTimeSpan = TimeSpan.FromHours(4);
        options.SlidingExpiration = true;
    });

builder.Services.AddAuthorization();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AngularPolicy", policy =>
    {
        var frontendUrl = "http://localhost:4200";
        policy
            .WithOrigins(frontendUrl)
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});

var app = builder.Build();

app.UseCors("AngularPolicy");
app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/", () => "API is running...");
app.MapAuthenticationEndpoints();

await app.SeedInitialDataAsync();

app.Run();
