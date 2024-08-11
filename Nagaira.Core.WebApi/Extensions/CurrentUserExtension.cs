using Microsoft.AspNetCore.Builder;
using Nagaira.Core.WebApi.Middlewares;

namespace Nagaira.Core.WebApi.Extensions
{
    public static class CurrentUserMiddlewareExtensions
    {
        public static IApplicationBuilder CurrentUserMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CurrentUserMiddleware>();
        }
    }
}
