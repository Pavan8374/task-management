using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManagement.Domain.Entities;

namespace TaskManagement.Infrastructure.Persistence.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(u => u.Id);

            builder.Property(u => u.FullName)
                .IsRequired()
                .HasColumnName("FullName")
                .HasMaxLength(100);

            builder.Property(u => u.Email)
                .IsRequired()
                .HasColumnName("Email")
                .HasMaxLength(100);

            builder.Property(u => u.PasswordHash)
                .IsRequired()
                .HasColumnName("PasswordHash")
                .HasColumnType("varchar(512)");

            builder.Property(u => u.Salt)
                .IsRequired()
                .HasColumnName("Salt")
                .HasColumnType("varchar(256)");

            builder.Property(u => u.RoleId)
                .HasColumnName("RoleId")
                .IsRequired();

            builder.Property(x => x.IsActive)
                .HasColumnName("IsActive")
                .HasColumnType("bit")
                .HasDefaultValue(true);

            builder.Property(x => x.CreatedAt)
                .HasDefaultValueSql("getutcdate()");

            builder.Property(x => x.ModifiedAt)
                .HasDefaultValueSql("getutcdate()");

            // Relationships
            builder.HasOne(u => u.Role)
                .WithMany()
                .HasForeignKey(u => u.RoleId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
