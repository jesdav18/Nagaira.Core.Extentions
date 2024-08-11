using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace Nagaira.Core.WebApi.Middlewares
{
    public class CurrentUserMiddleware
    {
        private readonly RequestDelegate _next;

        public CurrentUserMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            string token = context.Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
            if (!string.IsNullOrEmpty(token) && token.Split('.').Length == 3)
            {
                JwtSecurityToken jwtToken = new JwtSecurityToken(jwtEncodedString: token);
                string? username = jwtToken.Claims.FirstOrDefault(c => c.Type == "user")?.Value;

                if (!string.IsNullOrEmpty(username))
                {
                    context.Items["user"] = username.ToLower();
                }
            }

            await _next(context);
        }
    }
}
