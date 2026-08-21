namespace Backend.Models;

public class Session
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime ExpiresAt { get; set; }
    public string IpAddress { get; set; } = null!;

    // Research combining Student and Teacher into User
    // ICollection based on Role?
    // ICollection<Session> Sessions in User.cs
}