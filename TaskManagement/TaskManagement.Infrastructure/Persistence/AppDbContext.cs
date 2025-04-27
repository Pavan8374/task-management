using Microsoft.EntityFrameworkCore;
using System.Reflection;
using TaskManagement.Domain.Entities;

namespace TaskManagement.Infrastructure.Persistence
{
    /// <summary>
    /// Application db context
    /// </summary>
    public class AppDbContext : DbContext
    {
        /// <summary>
        /// Application db context constructor
        /// </summary>
        /// <param name="options">options</param>
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        /// <summary>
        /// Users
        /// </summary>
        public DbSet<User> Users { get; set; }

        /// <summary>
        /// Roles
        /// </summary>
        public DbSet<Role> Roles { get; set; }

        /// <summary>
        /// Tasks
        /// </summary>
        public DbSet<Domain.Entities.Task> Tasks { get; set; }

        /// <summary>
        /// Task comments
        /// </summary>
        public DbSet<TaskComment> TaskComments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(
                Assembly.GetAssembly(typeof(AppDbContext)));
            base.OnModelCreating(modelBuilder);
        }
    }
}
