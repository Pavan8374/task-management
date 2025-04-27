namespace TaskManagement.Domain.Entities
{
    /// <summary>
    /// Role
    /// </summary>
    public class Role
    {
        /// <summary>
        /// Role identity
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Role name
        /// </summary>
        public required string RoleName { get; set; }
    }
}
