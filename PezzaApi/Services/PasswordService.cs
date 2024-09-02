using Microsoft.AspNetCore.Identity;

public class PasswordService
{
    private readonly PasswordHasher<object> passwordHasher;

    public PasswordService()
    {
        // Initialize the password hasher
        passwordHasher = new PasswordHasher<object>();
    }

    public string HashPassword(string password)
    {
        // Use PasswordHasher to hash the plain-text password
        return passwordHasher.HashPassword(null, password);
    }

    public bool VerifyPassword(string hashedPassword, string providedPassword)
    {
        // Verify the provided password against the stored hash
        var result = passwordHasher.VerifyHashedPassword(null, hashedPassword, providedPassword);

        // Return true if the verification was successful, otherwise false
        return result == PasswordVerificationResult.Success;
    }
}