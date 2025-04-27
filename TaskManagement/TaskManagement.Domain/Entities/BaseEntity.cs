namespace TaskManagement.Domain.Entities
{
    /// <summary>
    /// Base entity
    /// </summary>
    public class BaseEntity
    {
        /// <summary>
        /// Identity
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Is active
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Created at
        /// </summary>
        public DateTime  CreatedAt{ get; set; }

        /// <summary>
        /// Modified at.
        /// </summary>
        public DateTime?  ModifiedAt{ get; set; }
    }
}
