namespace TaskManagement.Application.Exceptions
{
    /// <summary>
    /// Not found exception
    /// </summary>
    public class NotFoundException : Exception
    {
        /// <summary>
        /// Not found exception constructor
        /// </summary>
        /// <param name="message">Message</param>
        public NotFoundException(string message) : base(message) { }
    }
}
