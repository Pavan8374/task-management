using Microsoft.EntityFrameworkCore;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Interfaces;

namespace TaskManagement.Infrastructure.Persistence.Repositories
{
    /// <summary>
    /// User repository
    /// </summary>
    public class UserRepository : BaseRepository<User, AppDbContext>, IUserRepository
    {
        private readonly AppDbContext _context;

        /// <summary>
        /// User repository constructor
        /// </summary>
        /// <param name="context">ApplicationDb context</param>
        public UserRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        /// <summary>
        /// Get user details by email async 
        /// </summary>
        /// <param name="email">Email address</param>
        /// <returns>User</returns>
        public async Task<User> GetUserByEmailAsync(string email)
        {
            var emailLower = email.ToLower();
            return await _context.Users.AsNoTracking().FirstOrDefaultAsync(x => x.IsActive && x.Email.ToLower() == emailLower);
        }

        /// <summary>
        /// Check if user exist in the system
        /// </summary>
        /// <param name="userId">User identity</param>
        /// <returns>Boolean: true/false</returns>
        public async Task<bool> IsUserExist(int userId)
        {
            return await _context.Users.AsNoTracking().AnyAsync(x => x.Id == userId);
        }
    }
}
