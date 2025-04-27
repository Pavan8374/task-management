using System.Net;
using TaskManagement.API.Models;
using TaskManagement.Application.Exceptions;

namespace TaskManagement.API.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context); 
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");

                await HandleExceptionAsync(context, ex);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            var response = new ApiResponse<string>();

            switch (exception)
            {
                case ValidationException:
                case BusinessException:
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    response = ApiResponse<string>.FailureResponse(exception.Message);
                    break;
                case NotFoundException:
                    context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                    response = ApiResponse<string>.FailureResponse(exception.Message);
                    break;
                default:
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    response = ApiResponse<string>.FailureResponse("An unexpected error occurred.");
                    break;
            }

            await context.Response.WriteAsJsonAsync(response);
        }
    }
}
