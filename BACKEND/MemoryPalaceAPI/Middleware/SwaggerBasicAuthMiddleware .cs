using System.Net.Http.Headers;
using System.Net;
using System.Text;

namespace MemoryPalaceAPI.Middleware
{
    public class SwaggerBasicAuthMiddleware : IMiddleware
    {
        private string BasicAuthLogin;
        private string BasicAuthPassword;
        public SwaggerBasicAuthMiddleware(Secrets secrets)
        {
            BasicAuthLogin = secrets.BasicAuthLogin;
            BasicAuthPassword = secrets.BasicAuthPassword;
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            if (context.Request.Path.StartsWithSegments("/swagger"))
            {
                string authHeader = context.Request.Headers["Authorization"];
                if (authHeader != null && authHeader.StartsWith("Basic "))
                {
                    // Get the credentials from request header
                    var header = AuthenticationHeaderValue.Parse(authHeader);
                    var inBytes = Convert.FromBase64String(header.Parameter);
                    var credentials = Encoding.UTF8.GetString(inBytes).Split(':');
                    var username = credentials[0];
                    var password = credentials[1];
                    // validate credentials
                    if (username.Equals(BasicAuthLogin)
                      && password.Equals(BasicAuthPassword))
                    {
                        await next.Invoke(context).ConfigureAwait(false);
                        return;
                    }
                }
                context.Response.Headers["WWW-Authenticate"] = "Basic";
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            }
            else
            {
                await next.Invoke(context).ConfigureAwait(false);
            }
        }

    }
}
