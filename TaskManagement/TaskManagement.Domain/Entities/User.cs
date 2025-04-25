namespace TaskManagement.Domain.Entities
{
    public class User : BaseEntity
    {
        public string FullName {  get; set; }   
        public string Email {  get; set; }   
        public string PasswordHash {  get; set; }   
        public string Salt {  get; set; }   
        public int RoleId { get; set; }
        public Role Role { get; set; }
    }
}
