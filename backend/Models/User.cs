using Backend.Enums;

namespace Backend.Models;

public abstract class User
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string? PasswordHash { get; set; } = null!;
    public bool IsEmailVerified { get; set; }
    public bool RequirePasswordChange { get; set; }
    public bool IsDisabled { get; set; }
    public Role Role { get; set; }
    public DateTime CreatedAt { get; set; }
}