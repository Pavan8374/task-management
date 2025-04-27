namespace TaskManagement.Application.Exceptions
{
    /// <summary>
    /// Validation exception
    /// </summary>
    public class ValidationException : Exception
    {
        /// <summary>
        /// Validation exception constructor
        /// </summary>
        /// <param name="message">Message</param>
        public ValidationException(string message) : base(message) { }
    }

}
