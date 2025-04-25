using TaskManagement.Domain.Enums;
using TaskStatus = TaskManagement.Domain.Enums.TaskStatus;

namespace TaskManagement.Domain.Entities
{
    public class Task : BaseEntity
    {
        public string Title { get; set; }
        public TaskStatus TaskStatus { get; set; }
        public string? Description { get; set; }
        public int AssignedToUserId { get; set; }
        public int AssignedByUserId { get; set; }

        public User AssignedToUser { get; set; }
        public User AssignedByUser { get; set; }

    }
}
