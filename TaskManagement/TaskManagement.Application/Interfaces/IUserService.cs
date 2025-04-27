using TaskManagement.Domain.Entities;

namespace TaskManagement.Application.Interfaces
{
    public interface IUserService : IBaseService<User>
    {
        /// <summary>
        /// Get user details by email async 
        /// </summary>
        /// <param name="email">Email address</param>
        /// <returns>User</returns>
        public Task<User> GetUserByEmailAsync(string email);

        /// <summary>
        /// Check if user exist in the system
        /// </summary>
        /// <param name="userId">User identity</param>
        /// <returns>Boolean: true/false</returns>
        public Task<bool> IsUserExist(int userId);
    }
}
