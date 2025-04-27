namespace TaskManagement.Application.Common
{
    public interface IUserContext
    {
        int UserId { get; }
        string? Email { get; }
        string? Role { get; }
    }
}
