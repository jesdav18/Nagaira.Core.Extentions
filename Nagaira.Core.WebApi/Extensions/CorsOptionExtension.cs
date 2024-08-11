using Microsoft.AspNetCore.Cors.Infrastructure;

namespace Nagaira.Core.WebApi.Extensions
{
    public static class CorsOptionExtension
    {
        public static void AddPolicyCors(this CorsOptions corsOptions, string name, params string[] origins)
        {
            corsOptions.AddPolicy(name, builder =>
            {
                if (origins.Length == 0)
                {
                    builder.AllowAnyOrigin();
                }
                else
                {
                    builder.WithOrigins(origins);
                }
                builder.AllowAnyHeader()
                       .AllowAnyMethod()
                       .AllowCredentials();
            });
        }
    }
}
