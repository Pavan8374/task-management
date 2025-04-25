namespace TaskManagement.Domain.Entities
{
    public class User : BaseEntity
    {
        public required string FullName {  get; set; }   
        public required string Email {  get; set; }   
        public required string PasswordHash {  get; set; }   
        public required string Salt {  get; set; }   
        public required int RoleId { get; set; }
        public Role Role { get; set; }
    }
}
