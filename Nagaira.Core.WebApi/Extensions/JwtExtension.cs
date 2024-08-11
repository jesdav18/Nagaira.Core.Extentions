using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Nagaira.Core.WebApi.Extensions.Dtos;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
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

        public static string GenerarJwtToken(TokenOption tokenOption)
        {
            var key = Encoding.ASCII.GetBytes(tokenOption?.Secret!);
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = tokenOption.Claims,
                Expires = tokenOption.Expires,
                Audience = tokenOption!.Audience,
                Issuer = tokenOption!.Issuer,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public static string GetClaimFromToken(string token, string claimName, out string message)
        {
            try
            {
                token = token.ToString().Replace("Bearer ", "");

                JwtSecurityToken tokenJwt = new JwtSecurityToken(jwtEncodedString: token);
                string claimResponse = tokenJwt.Claims.First(c => c.Type == claimName).Value;
                if (claimResponse == null) return message = "Token no válido";

                message = "OK";
                return claimResponse;
            }
            catch (Exception exc)
            {
                return message = exc.ToString();
            }
        }

    }
}
