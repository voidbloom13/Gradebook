using Backend.Enums;

namespace Backend.Models;

public abstract class UserId
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;
    public Role Role { get; set; }
    public DateTime CreatedAt { get; set; }
    public ICollection<Session> Sessions { get; set; } = [];
}