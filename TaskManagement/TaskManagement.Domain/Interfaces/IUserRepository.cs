using TaskManagement.Domain.Entities;

namespace TaskManagement.Domain.Interfaces
{
    /// <summary>
    /// User repository interface
    /// </summary>
    public interface IUserRepository : IBaseRepository<User>
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
