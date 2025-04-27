namespace TaskManagement.Application.DTOs.Auth
{
    /// <summary>
    /// Signin response model
    /// </summary>
    public class SignInResponseModel
    {
        /// <summary>
        /// User identifire.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// First name.
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// Email.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// RoleName
        /// </summary>
        public string RoleName { get; set; }

        /// <summary>
        /// Token
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// Expiration
        /// </summary>
        public DateTime Expiration { get; set; }
    }
}
