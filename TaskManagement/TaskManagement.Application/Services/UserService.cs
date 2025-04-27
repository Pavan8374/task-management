using TaskManagement.Application.Interfaces;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Interfaces;

namespace TaskManagement.Application.Services
{
    public class UserService : BaseService<User>, IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository) : base(userRepository)
        {
            _userRepository = userRepository;   
        }

        /// <summary>
        /// Get user details by email async 
        /// </summary>
        /// <param name="email">Email address</param>
        /// <returns>User</returns>
        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _userRepository.GetUserByEmailAsync(email);    
        }

        /// <summary>
        /// Check if user exist in the system
        /// </summary>
        /// <param name="userId">User identity</param>
        /// <returns>Boolean: true/false</returns>
        public async Task<bool> IsUserExist(int userId)
        {
            return await _userRepository.IsUserExist(userId);
        }
    }
}
