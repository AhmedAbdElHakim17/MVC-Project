using Microsoft.AspNetCore.Mvc;

namespace MVC_PROJECT.Middlewares
{
    public class GlobalExceptionHandlingMiddleware : Controller
    {
        public readonly RequestDelegate next;
        public readonly ILogger<GlobalExceptionHandlingMiddleware> logger;
        public GlobalExceptionHandlingMiddleware(RequestDelegate _next, ILogger<GlobalExceptionHandlingMiddleware> _logger)
        {
            next = _next;
            logger = _logger;
        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex) 
            {
                logger.LogError(ex, $"[Exception] An unhandled exception occured {ex.Message} {context.Request.Path}");
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = ex switch
                {
                    KeyNotFoundException => StatusCodes.Status404NotFound,
                    ArgumentException => StatusCodes.Status400BadRequest,
                    _ => StatusCodes.Status500InternalServerError
                };
                var errorResponse = new
                {
                    statusCode = context.Response.StatusCode,
                    message = "There is an error, Please Try Again",
                };
                await context.Response.WriteAsJsonAsync(errorResponse);
            }
        }
    }
}
