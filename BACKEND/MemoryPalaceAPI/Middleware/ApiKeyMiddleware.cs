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
                throw new UnauthorizedException("Api Key not found");
            }

            var appSettings = context.RequestServices.GetRequiredService<IConfiguration>();

            if (!ApiKeyValue.Equals(apiKeyFromUser))
            {
                throw new UnauthorizedException("Unauthorized client");
            }

            await next(context);
        }

    }
}
