using System.Security.Cryptography;
using System.Text;
using TaskManagement.Application.Interfaces;

namespace TaskManagement.Infrastructure.Auth
{
    public class AuthService : IAuthService
    {
        public string GenerateSalt()
        {
            byte[] saltBytes = new byte[16];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(saltBytes);
            return Convert.ToBase64String(saltBytes);
        }

        public string HashPassword(string password, string salt)
        {
            var combined = Encoding.UTF8.GetBytes(password + salt);
            using var sha256 = SHA256.Create();
            byte[] hashBytes = sha256.ComputeHash(combined);
            return Convert.ToBase64String(hashBytes);
        }

        public bool VerifyPassword(string enteredPassword, string storedHash, string salt)
        {
            var hashOfEntered = HashPassword(enteredPassword, salt);
            return hashOfEntered == storedHash;
        }
    }
}
