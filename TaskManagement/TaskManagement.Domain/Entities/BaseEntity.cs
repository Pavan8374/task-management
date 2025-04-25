namespace TaskManagement.Domain.Entities
{
    /// <summary>
    /// Base entity
    /// </summary>
    public class BaseEntity
    {
        public Guid Id { get; set; }
        public bool IsActive { get; set; }
        public DateTime  CreatedAt{ get; set; }
        public DateTime?  ModifiedAt{ get; set; }
    }
}
