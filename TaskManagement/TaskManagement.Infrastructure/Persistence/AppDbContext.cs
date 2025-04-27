using Microsoft.EntityFrameworkCore;
using System.Reflection;
using TaskManagement.Domain.Entities;

namespace TaskManagement.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Domain.Entities.Task> Tasks { get; set; }
        public DbSet<TaskComment> TaskComments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(
                Assembly.GetAssembly(typeof(AppDbContext)));
            base.OnModelCreating(modelBuilder);
        }
    }
}
