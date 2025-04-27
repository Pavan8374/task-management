namespace TaskManagement.API.Models
{
    /// <summary>
    /// Api response 
    /// </summary>
    /// <typeparam name="T">Generic model</typeparam>
    public class ApiResponse<T>
    {
        /// <summary>
        /// Success
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// Model object
        /// </summary>
        public T? Data { get; set; }

        /// <summary>
        /// Message
        /// </summary>
        public string? Message { get; set; }

        /// <summary>
        /// Success response
        /// </summary>
        /// <param name="data">generic model</param>
        /// <param name="message">Message</param>
        /// <returns>ApiResponse with generic model</returns>
        public static ApiResponse<T> SuccessResponse(T data, string? message = null)
        {
            return new ApiResponse<T> { Success = true, Data = data, Message = message };
        }

        /// <summary>
        /// Failure response
        /// </summary>
        /// <param name="message">Message</param>
        /// <returns>ApiResponse with generic model</returns>
        public static ApiResponse<T> FailureResponse(string message)
        {
            return new ApiResponse<T> { Success = false, Data = default, Message = message };
        }
    }
}
