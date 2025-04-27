using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace TaskManagement.Application.Common
{
    /// <summary>
    /// Shared utilities
    /// </summary>
    public static class SharedUtils
    {
        /// <summary>
        /// Get jwt token 
        /// </summary>
        /// <param name="authClaims">Auth claims</param>
        /// <param name="JwtSecret">JWT Secret</param>
        /// <param name="JwtValidIssuer">Jwt valid issuer</param>
        /// <param name="JwtValidAudience">JWT Valid audience</param>
        /// <param name="JwtExpiryDays">JWT expiry days</param>
        /// <returns>JWTSecurityToken class</returns>
        public static JwtSecurityToken GetJWTToken(List<Claim> authClaims, string JwtSecret, string JwtValidIssuer, string JwtValidAudience, string JwtExpiryDays)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtSecret));

            var token = new JwtSecurityToken(
                issuer: JwtValidIssuer,
                audience: JwtValidAudience,
                expires: DateTime.Now.AddDays(String.IsNullOrWhiteSpace(JwtExpiryDays) ? 7 : Convert.ToInt64(JwtExpiryDays)),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256));
            return token;
        }

        /// <summary>
        /// Get Token Claims
        /// </summary>
        /// <param name="email">Email</param>
        /// <param name="userId">User identity</param>
        /// <param name="name">Name</param>
        /// <param name="role">Role</param>
        /// <returns>Claims</returns>
        public static List<Claim> GetTokenClaims(string email, string userId, string name, string role)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, email ?? string.Empty),
                new Claim(ClaimTypes.NameIdentifier, userId),
                new Claim(ClaimTypes.GivenName, name ?? string.Empty),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Role, role)
            };
            return claims;
        }

        /// <summary>
        /// Generate salt to hash password
        /// </summary>
        /// <returns>salt as sttring</returns>
        public static string GenerateSalt()
        {
            byte[] saltBytes = new byte[16];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(saltBytes);
            return Convert.ToBase64String(saltBytes);
        }

        /// <summary>
        /// Generate hashed password using salt and text password
        /// </summary>
        /// <param name="password">Password</param>
        /// <param name="salt">Salt</param>
        /// <returns>Hashed password</returns>
        public static string HashPassword(string password, string salt)
        {
            var combined = Encoding.UTF8.GetBytes(password + salt);
            using var sha256 = SHA256.Create();
            byte[] hashBytes = sha256.ComputeHash(combined);
            return Convert.ToBase64String(hashBytes);
        }

        /// <summary>
        /// Verify the input password whether it is valid or invalid
        /// </summary>
        /// <param name="enteredPassword">Entered password</param>
        /// <param name="storedHash">Stored hash</param>
        /// <param name="salt">Salt</param>
        /// <returns>trur/false</returns>
        public static bool VerifyPassword(string enteredPassword, string storedHash, string salt)
        {
            var hashOfEntered = HashPassword(enteredPassword, salt);
            return hashOfEntered == storedHash;
        }
    }
}
