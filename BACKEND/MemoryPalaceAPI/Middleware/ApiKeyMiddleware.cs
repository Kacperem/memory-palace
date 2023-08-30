using MemoryPalaceAPI.Exceptions;

namespace MemoryPalaceAPI.Middleware
{
    public class ApiKeyMiddleware : IMiddleware
    {

        private const string ApiKey = "API-KEY";


        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            if (!context.Request.Headers.TryGetValue(ApiKey, out var apiKeyVal))
            {

                context.Response.StatusCode = 401;
                context.Response.WriteAsync("Api Key not found!");
                return;
            }

            var appSettings = context.RequestServices.GetRequiredService<IConfiguration>();
            var apiKey = appSettings.GetValue<string>(ApiKey);
            if (!apiKey.Equals(apiKeyVal))
            {
                context.Response.StatusCode = 401;
                 context.Response.WriteAsync("Unauthorized client");

            }

             next(context);
        }

    }
}
