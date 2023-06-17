using Microsoft.AspNetCore.Cors.Infrastructure;

namespace Nagaira.WebApi.Utilities.Extensions
{
    public static class CorsOptionExtension
    {
        public static void AddPolicyDiunsa(this CorsOptions corsOptions, string name, params string[] orings)
        {
            corsOptions.AddPolicy(name, builder =>
            {
                if (orings.Length == 0)
                {
                    builder.SetIsOriginAllowed(_ => true);
                }
                else
                {
                    builder.WithOrigins(orings);
                }
                builder.AllowAnyHeader()
                       .AllowAnyMethod()
                       .AllowCredentials();
            });
        }
    }
}
