namespace Backend.Services;

public sealed class PasswordHasherService : IPasswordHasher
{
    private readonly PasswordHasher<User> _hasher = new();

    public static string Hash(string password)
    {
        return _hasher.HashPassword(null!, password);
    }

    public static bool Verify(string password, string hash)
    {
        return _hasher.VerifyHashedPassword(
            null!,
            hash,
            password) != PasswordVerificationResult.Failed;
        )
    }
}