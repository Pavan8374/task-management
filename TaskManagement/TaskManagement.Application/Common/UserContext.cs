using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace TaskManagement.Application.Common
{
    /// <summary>
    /// User context
    /// </summary>
    public class UserContext : IUserContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        /// User context constructor
        /// </summary>
        /// <param name="httpContextAccessor">Http context accessor</param>
        public UserContext(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// User id
        /// </summary>
        public int UserId
        {
            get
            {
                var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                return userIdClaim != null ? int.Parse(userIdClaim) : 0;
            }
        }

        /// <summary>
        /// Email
        /// </summary>
        public string? Email
        {
            get => _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Email)?.Value;
        }

        /// <summary>
        /// Role
        /// </summary>
        public string? Role
        {
            get => _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Role)?.Value;
        }

    }
}
