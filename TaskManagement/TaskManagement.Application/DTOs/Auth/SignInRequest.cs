using System.ComponentModel.DataAnnotations;

namespace TaskManagement.Application.DTOs.Auth
{
    /// <summary>
    /// Sign in request model
    /// </summary>
    public class SignInRequest
    {
        /// <summary>
        /// Email address
        /// </summary>
        [Required(ErrorMessage = "Email is required!")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }

        /// <summary>
        /// Password
        /// </summary>

        [Required(ErrorMessage = "Password is required!")]
        public string Password { get; set; }
    }
}
