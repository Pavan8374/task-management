using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManagement.Domain.Entities;

namespace TaskManagement.Infrastructure.Persistence.DataConfigurations
{
    public class RoleDataConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasData(
                 new Role() { Id = 1, RoleName = "Admin"},
                 new Role() { Id = 2, RoleName = "User"}
             );
        }
    }
}
