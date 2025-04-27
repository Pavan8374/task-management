namespace TaskManagement.Application.Exceptions
{
    /// <summary>
    /// Business exception
    /// </summary>
    public class BusinessException : Exception
    {
        /// <summary>
        /// Business exception constructor
        /// </summary>
        /// <param name="message">Message</param>
        public BusinessException(string message) : base(message) { }
    }

}
