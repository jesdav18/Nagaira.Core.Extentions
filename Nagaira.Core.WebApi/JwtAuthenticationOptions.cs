namespace Nagaira.Core.WebApi
{
    public class JwtAuthenticationOptions
    {
        public string? SchemaName { get; set; }
        public string? SecretKey { get; set; }
        public string? ValidIssuer { get; set; }
        public string? Audience { get; set; }
    }
}
