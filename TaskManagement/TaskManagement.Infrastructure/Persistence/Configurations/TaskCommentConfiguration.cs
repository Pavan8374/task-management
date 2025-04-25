using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManagement.Domain.Entities;

namespace TaskManagement.Infrastructure.Persistence.Configurations
{
    public class TaskCommentConfiguration : IEntityTypeConfiguration<TaskComment>
    {
        public void Configure(EntityTypeBuilder<TaskComment> builder)
        {
            builder.ToTable("TaskComments");

            builder.HasKey(tc => tc.Id);

            builder.Property(tc => tc.TaskId)
                .IsRequired()
                .HasColumnName("TaskId");

            builder.Property(tc => tc.UserId)
                .IsRequired()
                .HasColumnName("UserId");

            builder.Property(tc => tc.Description)
                .IsRequired()
                .HasColumnName("Description")
                .HasMaxLength(1000);

            builder.Property(tc => tc.IsActive)
                .HasColumnName("IsActive")
                .HasColumnType("bit")
                .HasDefaultValue(true);

            builder.Property(tc => tc.CreatedAt)
                .HasDefaultValueSql("getutcdate()");

            builder.Property(tc => tc.ModifiedAt)
                .HasDefaultValueSql("getutcdate()");

            // Relationships
            builder.HasOne(tc => tc.Task)
                .WithMany()
                .HasForeignKey(tc => tc.TaskId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(tc => tc.User)
                .WithMany()
                .HasForeignKey(tc => tc.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
