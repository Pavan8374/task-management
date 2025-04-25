namespace TaskManagement.Application.DTOs.Tasks
{
    public class TaskDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public TaskStatusDTO TaskStatus { get; set; }
        public AssignedToUserDto AssignedToUser { get; set; }
        public AssignedByUserDto AssignedByUser { get; set; }
    }

    public class AssignedToUserDto
    {
        public int UserId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
    }

    public class AssignedByUserDto
    {
        public int UserId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
    }

    public class TaskStatusDTO
    {
        public int Id { get; set; }
        public string Status { get; set; }
    }

}
