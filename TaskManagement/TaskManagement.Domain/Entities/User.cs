namespace TaskManagement.Domain.Entities
{
    /// <summary>
    /// User entity
    /// </summary>
    public class User : BaseEntity
    {
        /// <summary>
        /// User full name
        /// </summary>
        public string FullName {  get; set; }   

        /// <summary>
        /// User email
        /// </summary>
        public string Email {  get; set; }   

        /// <summary>
        /// Hashed password
        /// </summary>
        public string PasswordHash {  get; set; }   

        /// <summary>
        /// Salt
        /// </summary>
        public string Salt {  get; set; }  
        
        /// <summary>
        /// RoleId
        /// </summary>
        public int RoleId { get; set; }

        /// <summary>
        /// Role identity
        /// </summary>
        public Role Role { get; set; }
    }
}
