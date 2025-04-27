namespace TaskManagement.Application.Common
{
    /// <summary>
    /// User context interface
    /// </summary>
    public interface IUserContext
    {
        /// <summary>
        /// User id
        /// </summary>
        int UserId { get; }

        /// <summary>
        /// User email
        /// </summary>
        string? Email { get; }

        /// <summary>
        /// User role
        /// </summary>
        string? Role { get; }
    }
}
