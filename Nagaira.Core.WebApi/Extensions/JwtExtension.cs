using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nagaira.Core.WebApi.Extensions
{
    public static class JwtExtensions
    {
        public static void AddJwtAuthentication(this IServiceCollection services, IList<JwtAuthenticationOptions> authenticationOptions)
        {
            if (authenticationOptions.Any())
            {
                foreach (var authOption in authenticationOptions)
                {
                    var secretKeyEnconde = Encoding.ASCII.GetBytes(authOption.SecretKey!);

                    services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                        .AddJwtBearer(authOption.SchemaName!, x =>
                        {
                            x.RequireHttpsMetadata = false;
                            x.SaveToken = true;
                            x.TokenValidationParameters = new TokenValidationParameters
                            {
                                ValidateActor = true,
                                ValidateAudience = true,
                                ValidateLifetime = true,
                                ValidateIssuerSigningKey = true,
                                ValidIssuer = authOption.ValidIssuer,
                                ValidAudience = authOption.Audience,
                                IssuerSigningKey = new SymmetricSecurityKey(secretKeyEnconde)
                            };
                        });
                }

                var schemes = authenticationOptions.Select(authOption => authOption.SchemaName!).ToArray();

                services.AddAuthorization(options =>
                {
                    options.DefaultPolicy = new AuthorizationPolicyBuilder()
                        .AddAuthenticationSchemes(schemes)
                        .RequireAuthenticatedUser()
                        .Build();
                });
            }
        }

    }
}
