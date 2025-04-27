namespace TaskManagement.Domain.Entities
{
    /// <summary>
    /// Task Comment entity
    /// </summary>
    public class TaskComment : BaseEntity
    {
        /// <summary>
        /// Task identity
        /// </summary>
        public int TaskId { get; set; }

        /// <summary>
        /// User identity
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Description
        /// </summary>
        public required string Description { get; set; }
        public Task Task { get; set; }
        public User User { get; set; }
    }
}
