using Microsoft.AspNetCore.Http;

namespace Nagaira.WebApi.Utilities.Extensions
{
    public static class RootEndpointHelper
    {
        public static async Task HandleRootEndpoint(HttpContext context, bool isProduction, string assemblyName, string version)
        {
            var ambiente = isProduction ? "Producción" : "Desarrollo";
            var responseContent = $"<b>{assemblyName} | {version} | {ambiente}</b><hr>";

            context.Response.ContentType = "text/html";
            await context.Response.WriteAsync(responseContent);
        }
    }
}
