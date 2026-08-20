using Backend.Enums;

namespace Backend.Models;

public class Teacher
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;
    public Role Role { get; set; }
    public DateTime CreatedAt { get; set; }
    public ICollection<Course> Courses { get; set; } = [];
}