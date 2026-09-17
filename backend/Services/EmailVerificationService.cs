using System.Security.Cryptography;
using Backend.Data;
using Backend.Models;
using Microsoft.EntityFrameworkCore;
using Backend.Services.Email;

namespace Backend.Services;

public class EmailVerificationService
{
    private readonly AppDbContext _dbContext;
    private readonly IPasswordHasher _hasher;
    private readonly IEmailService _emailService;
    public EmailVerificationService(AppDbContext dbContext, IPasswordHasher hasher, IEmailService emailService)
    {
        _dbContext = dbContext;
        _hasher = hasher;
        _emailService = emailService;
    }

    public async Task<string> GenerateCodeAsync(User user)
    {
        var code = RandomNumberGenerator
            .GetInt32(0, 1_000_000)
            .ToString("D6");
        
        var verificationCode = new EmailVerificationCode
        {
            UserId = user.Id,
            CodeHash = _hasher.Hash(code),
            CreatedAt = DateTime.UtcNow,
            ExpiresAt = DateTime.UtcNow.AddMinutes(15),
            IsValid = true
        };

        _dbContext.EmailVerificationCodes.Add(verificationCode);
        await _dbContext.SaveChangesAsync();

        await _emailService.SendEmailAsync(
            user.Email,
            "Your Email Verification Code",
            code
        );

        return code;
    }

    public async Task<bool> VerifyCodeAsync(string code, User user)
    {
        var verificationCode = await _dbContext.EmailVerificationCodes
            .Where(c =>
                c.UserId == user.Id
                && c.IsValid
                && c.FailedAttempts < 5
                && c.ExpiresAt > DateTime.UtcNow)
            .OrderByDescending(c => c.CreatedAt)
            .FirstOrDefaultAsync();
        
        if (verificationCode == null)
        {
            return false;
        }

        var isVerified = _hasher.Verify(code, verificationCode.CodeHash);
        user.IsEmailVerified = isVerified;
        if (!isVerified)
        {
            verificationCode.FailedAttempts++;
            if (verificationCode.FailedAttempts >= 5)
            {
                verificationCode.IsValid = false;
                verificationCode.InvalidatedAt = DateTime.UtcNow;
            }
        } else
        {
            verificationCode.IsValid = false;
            verificationCode.VerifiedAt = DateTime.UtcNow;
        }
        _dbContext.Update(user);
        _dbContext.Update(verificationCode);
        await _dbContext.SaveChangesAsync();
        return isVerified;
    }
}