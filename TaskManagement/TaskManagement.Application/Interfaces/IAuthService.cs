namespace TaskManagement.Application.Interfaces;

public interface IAuthService
{
    string GenerateSalt();
    string HashPassword(string password, string salt);
    bool VerifyPassword(string enteredPassword, string storedHash, string salt);
}
