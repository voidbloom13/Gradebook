using System.Security.Cryptography;
using Backend.Data;
using Backend.Models;
using Microsoft.EntityFrameworkCore;

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

    public async Task<bool> VerifyCode(string code, User user)
    {
        var verificationCode = await _dbContext.EmailVerificationCodes
            .Where(c =>
                c.UserId == user.Id
                && !c.IsUsed
                && c.Attempts < 3
                && c.ExpiresAt > DateTime.UtcNow)
            .OrderByDescending(c => c.CreatedAt)
            .FirstOrDefaultAsync();
        
        if (verificationCode == null)
        {
            return false;
        }
        
        var isVerified = _hasher.Verify(code, verificationCode.CodeHash);
        if (!isVerified)
        {
            verificationCode.Attempts++;
            _dbContext.Update(verificationCode);
        }
        return isVerified;
    }
}