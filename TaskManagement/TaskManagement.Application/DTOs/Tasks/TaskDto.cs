namespace TaskManagement.Application.DTOs.Tasks
{
    /// <summary>
    /// Task dto
    /// </summary>
    public class TaskDto
    {
        /// <summary>
        /// Task id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Task title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Task description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Task status
        /// </summary>
        public TaskStatusDTO TaskStatus { get; set; }

        /// <summary>
        /// Assigned to user dto
        /// </summary>
        public AssignedToUserDto AssignedToUser { get; set; }

        /// <summary>
        /// Assigned by user dto
        /// </summary>
        public AssignedByUserDto AssignedByUser { get; set; }
    }

    /// <summary>
    /// Assigned to user dto
    /// </summary>
    public class AssignedToUserDto
    {
        /// <summary>
        /// User id
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// User full name
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        ///  User email
        /// </summary>
        public string Email { get; set; }
    }

    /// <summary>
    /// Assigned by user dto
    /// </summary>
    public class AssignedByUserDto
    {
        /// <summary>
        /// User id
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// User full name
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        ///  User email
        /// </summary>
        public string Email { get; set; }
    }

    /// <summary>
    /// Task status dto
    /// </summary>
    public class TaskStatusDTO
    {
        /// <summary>
        /// Task status id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Task status
        /// </summary>
        public string Status { get; set; }
    }

}
