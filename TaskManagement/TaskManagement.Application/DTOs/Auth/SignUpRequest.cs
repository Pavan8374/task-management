using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TaskManagement.Application.DTOs.Auth
{
    public class SignUpRequest
    {
        /// <summary>
        /// Full name
        /// </summary>
        [Required(ErrorMessage = "Full name is required.")]
        [StringLength(100, ErrorMessage = "Full name can't be longer than 100 characters.")]
        public string FullName { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        [StringLength(100, ErrorMessage = "Email can't be longer than 100 characters.")]
        public string Email { get; set; }

        /// <summary>
        /// Password
        /// </summary>
        [Required(ErrorMessage = "Password is required.")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters long.")]
        public string Password { get; set; }

        /// <summary>
        /// Is admin flag to create admin account.
        /// </summary>

        [DefaultValue(false)]
        public bool IsAdmin { get; set; } = false;
    }
}
