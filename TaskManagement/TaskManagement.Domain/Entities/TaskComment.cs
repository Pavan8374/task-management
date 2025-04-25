namespace TaskManagement.Domain.Entities
{
    public class TaskComment : BaseEntity
    {
        public Guid TaskId { get; set; }
        public Guid UserId { get; set; }
        public required string Description { get; set; }

        public Task Task { get; set; }
        public User User { get; set; }
    }
}
