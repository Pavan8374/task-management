using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Task = TaskManagement.Domain.Entities.Task;
using TaskStatus = TaskManagement.Domain.Enums.TaskStatus;

namespace TaskManagement.Infrastructure.Persistence.Configurations
{
    public class TaskConfiguration : IEntityTypeConfiguration<Task>
    {
        public void Configure(EntityTypeBuilder<Task> builder)
        {
            builder.ToTable("Tasks");

            builder.HasKey(t => t.Id);

            builder.Property(t => t.Title)
                .IsRequired()
                .HasColumnName("Title")
                .HasMaxLength(200);

            builder.Property(t => t.Description)
                .HasColumnName("Description")
                .HasMaxLength(1000);

            builder.Property(t => t.TaskStatus)
                .HasColumnName("TaskStatus")
                .IsRequired()
                .HasDefaultValue(TaskStatus.Todo);

            builder.Property(t => t.AssignedToUserId)
                .IsRequired()
                .HasColumnName("AssignedToUserId");

            builder.Property(t => t.AssignedByUserId)
                .IsRequired()
                .HasColumnName("AssignedByUserId");

            builder.Property(t => t.IsActive)
                .HasColumnName("IsActive")
                .HasColumnType("bit")
                .HasDefaultValue(true);

            builder.Property(t => t.CreatedAt)
                .HasDefaultValueSql("getutcdate()");

            builder.Property(t => t.ModifiedAt)
                .HasDefaultValueSql("getutcdate()");

            // Relationships
            builder.HasOne(t => t.AssignedToUser)
                .WithMany()
                .HasForeignKey(t => t.AssignedToUserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(t => t.AssignedByUser)
                .WithMany()
                .HasForeignKey(t => t.AssignedByUserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
