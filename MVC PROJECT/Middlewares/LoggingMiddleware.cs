using Microsoft.AspNetCore.Mvc;

namespace MVC_PROJECT.Middlewares
{
    public class LoggingMiddleware : Controller
    {
        public readonly RequestDelegate next;
        public readonly ILogger<LoggingMiddleware> logger;
        public LoggingMiddleware(RequestDelegate n, ILogger<LoggingMiddleware> il)
        {
            next = n;
            logger = il;
        }
        public async Task Invoke(HttpContext context)
        {

            logger.LogInformation("[REQUEST] {Method} {Path} at {Time}]",
                context.Request.Method, context.Request.Path, DateTime.Now);
            await next(context);
            logger.LogInformation("[RESPONSE] {StatusCode} for {Path}]",
                context.Response.StatusCode, context.Request.Path);
        }
    }
}
