using MemoryPalaceAPI.Exceptions;

namespace MemoryPalaceAPI.Middleware
{
    public class ApiKeyMiddleware : IMiddleware
    {
        private const string ApiKeyHeader = "API-KEY";
        private string ApiKeyValue;
        public ApiKeyMiddleware(Secrets secrets)
        {
            ApiKeyValue = secrets.ApiKey;
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            if (!context.Request.Headers.TryGetValue(ApiKeyHeader, out var apiKeyFromUser))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Api Key not found!");
                return;
            }

            var appSettings = context.RequestServices.GetRequiredService<IConfiguration>();

            if (!ApiKeyValue.Equals(apiKeyFromUser))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Unauthorized client");
            }

            await next(context);
        }

    }
}
