namespace TaskManagement.Domain.Entities
{
    public class TaskComment : BaseEntity
    {
        public int TaskId { get; set; }
        public int UserId { get; set; }
        public required string Description { get; set; }
        public Task Task { get; set; }
        public User User { get; set; }
    }
}
