using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Nagaira.Core.WebApi.Services.Interfaces;
using System;

namespace Nagaira.Core.WebApi.Services
{
    public class CurrentService : ICurrentService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CurrentService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetCurrentUser()
        {
            var user = _httpContextAccessor.HttpContext?.Items["user"];
            return user?.ToString() ?? string.Empty;
        }

        public string GetCurrentToken()
        {
            var context = _httpContextAccessor.HttpContext;
            if (context != null && context.Request.Headers.TryGetValue("Authorization", out StringValues token))
            {
                return token.ToString().Replace("Bearer ", "");
            }

            throw new Exception("No se encontró token en la petición.");
        }
    }
}
