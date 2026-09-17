namespace Backend.Models;

public class EmailVerificationCode
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string CodeHash { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public DateTime ExpiresAt { get; set; }
    public DateTime ResendAvailableAt { get; set; }
    public int FailedAttempts { get; set; }
    public bool IsValid { get; set; }
    public DateTime? InvalidatedAt { get; set; }
    public DateTime? VerifiedAt { get; set; }
    public User User { get; set; } = null!;
}