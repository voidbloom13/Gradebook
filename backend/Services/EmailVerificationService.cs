using System.Security.Cryptography;
using Backend.Data;
using Backend.Models;

namespace Backend.Services;

public class EmailVerificationService
{
    private readonly AppDbContext _dbContext;
    private readonly IPasswordHasher _hasher;
    public EmailVerificationService(AppDbContext dbContext, IPasswordHasher hasher)
    {
        _dbContext = dbContext;
        _hasher = hasher;
    }

    public async Task<string> GenerateCode(User user)
    {
        // pass dbContext and hasher service here
        var code = RandomNumberGenerator
            .GetInt32(0, 1_000_000)
            .ToString("D6");
        
        var verificationCode = new EmailVerificationCode
        {
            UserId = user.Id,
            CodeHash = _hasher.Hash(code),
            CreatedAt = DateTime.UtcNow,
            ExpiresAt = DateTime.UtcNow.AddMinutes(15),
            IsUsed = false
        };

        _dbContext.EmailVerificationCodes.Add(verificationCode);
        await _dbContext.SaveChangesAsync();

        return code;
    }
}