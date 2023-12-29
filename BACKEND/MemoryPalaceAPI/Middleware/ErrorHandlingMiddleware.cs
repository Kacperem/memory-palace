using MemoryPalaceAPI.Exceptions;
using Newtonsoft.Json;

namespace MemoryPalaceAPI.Middleware
{
    public class ErrorHandlingMiddleware : IMiddleware
    {
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(ILogger<ErrorHandlingMiddleware> logger)
        {
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next.Invoke(context);
            }
            catch (ForbidException forbidException)
            {
                context.Response.StatusCode = 403;
                await WriteJsonResponse(context, new ErrorResponse { Message = "Forbidden" });
            }
            catch (BadRequestException badRequestException)
            {
                context.Response.StatusCode = 400;
                await WriteJsonResponse(context, new ErrorResponse { Message = badRequestException.Message });
            }
            catch (NotFoundException notFoundException)
            {
                context.Response.StatusCode = 404;
                await WriteJsonResponse(context, new ErrorResponse { Message = notFoundException.Message });
            }
            catch (UnauthorizedException unauthorizedException)
            {
                context.Response.StatusCode = 401;
                await WriteJsonResponse(context, new ErrorResponse { Message = unauthorizedException.Message });
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);

                context.Response.StatusCode = 500;
                await WriteJsonResponse(context, new ErrorResponse { Message = "Something went wrong" });
            }
        }

        private async Task WriteJsonResponse(HttpContext context, ErrorResponse errorResponse)
        {
            var jsonResponse = JsonConvert.SerializeObject(errorResponse);
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(jsonResponse);
        }
    }

    public class ErrorResponse
    {
        public string Message { get; set; }
        // other fields
    }
}

