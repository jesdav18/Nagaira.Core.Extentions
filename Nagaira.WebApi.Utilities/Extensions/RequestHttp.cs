using RestSharp;

namespace Nagaira.WebApi.Utilities.Extensions
{
    public static class RequestHttp
    {
        private static RestRequest ConfigurarRequest(string ruta, Method method, string bearerToken = "", IDictionary<string, string>? headers = null)
        {
            RestRequest request = new RestRequest(ruta, method);
            request.RequestFormat = DataFormat.Json;
            request.AddHeader("Authorization", $"Bearer {bearerToken}");
            request.AddHeader("Content-Type", "application/json");

            if (headers == null) return request;

            foreach (var header in headers)
            {
                request.AddHeader(header.Key, header.Value);
            }

            return request;
        }

        public static RestRequest Get(string ruta, string bearerToken = "", IDictionary<string, string>? headers = null)
        {
            return ConfigurarRequest(ruta, Method.GET, bearerToken, headers!);
        }

        public static RestRequest Post(string ruta, object body, string bearerToken = "", IDictionary<string, string>? headers = null)
        {
            RestRequest request = ConfigurarRequest(ruta, Method.POST, bearerToken, headers!);
            request.AddJsonBody(body);
            return request;
        }
    }
}
