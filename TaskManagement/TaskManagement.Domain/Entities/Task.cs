namespace TaskManagement.Domain.Entities
{
    public class Task : BaseEntity
    {
        public required string Title { get; set; }
        public TaskStatus TaskStatus { get; set; }
        public string? Description { get; set; }
    }
}
