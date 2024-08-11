using System;
using System.Security.Claims;

namespace Nagaira.Core.WebApi.Extensions.Dtos
{
    public class TokenOption
    {
        public string? Issuer { get; set; }
        public string? Audience { get; set; }
        public string? Secret { get; set; }
        public DateTime? Expires { get; set; }
        public ClaimsIdentity? Claims { get; set; }
    }
}
