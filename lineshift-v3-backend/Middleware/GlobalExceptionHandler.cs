using lineshift_v3_backend.Exceptions;
using System.Net;
using System.Text.Json;

namespace lineshift_v3_backend.Middleware
{
    public class GlobalExceptionHandler
    {
        private readonly RequestDelegate _next; // represents the next middleware in the app's request pipeline
        private readonly ILogger<GlobalExceptionHandler> _logger;

        public GlobalExceptionHandler(RequestDelegate next, ILogger<GlobalExceptionHandler> logger)
        {
            _next = next;
            _logger = logger;
        }

        #region Methods
        public async Task InvokeAsync(HttpContext context)
        {
            // wraps the call tot he next middleware 
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                // Log the exception Here
                _logger.LogError(ex, "An exception was thrown by the application");
                await HandleExceptionAsync(context, ex);
            }
        }

        public static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            // Exception processing
            // Mapping the status code and message based on the exception given
            var (statusCode, message) = ExceptionDetailsMapper.Map(exception); 


            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;

            var exceptionDetails = new
            {
                StatusCode = context.Response.StatusCode,
                Message = message,
            };

            return context.Response.WriteAsync(JsonSerializer.Serialize(exceptionDetails));
        }
    }
        #endregion
}
