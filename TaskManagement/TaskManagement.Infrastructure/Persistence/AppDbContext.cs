using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace TaskManagement.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(
                Assembly.GetAssembly(typeof(AppContext)));
            base.OnModelCreating(modelBuilder);
        }
    }
}
