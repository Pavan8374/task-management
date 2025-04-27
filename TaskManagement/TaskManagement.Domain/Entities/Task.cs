using TaskStatus = TaskManagement.Domain.Enums.TaskStatus;

namespace TaskManagement.Domain.Entities
{
    /// <summary>
    /// Task entity
    /// </summary>
    public class Task : BaseEntity
    {
        /// <summary>
        /// Task title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Task status
        /// </summary>
        public TaskStatus TaskStatus { get; set; }

        /// <summary>
        /// Task description
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Task assigned to userId
        /// </summary>
        public int AssignedToUserId { get; set; }

        /// <summary>
        /// Task assigned by userId
        /// </summary>
        public int AssignedByUserId { get; set; }

        public User AssignedToUser { get; set; }
        public User AssignedByUser { get; set; }

    }
}
