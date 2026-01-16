using TaskManager.Api.Common;

namespace TaskManager.Api.Middleware
{
    public class ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An unhandled exception occurred.");
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Response.ContentType = "application/json";
                var response = new
                {
                    Success = false,
                    Message = "An unexpected error occurred. Please try again later."
                };
                await context.Response.WriteAsJsonAsync(ApiResponse<string>.Fail(response.Message));
            }
        }
    }
}
