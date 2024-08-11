using Newtonsoft.Json;
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

        private static RestRequest ConfigurarRequestXml(string ruta, Method method, IDictionary<string, string>? headers = null)
        {
            RestRequest request = new RestRequest(ruta, method);
            request.RequestFormat = DataFormat.Xml;


            request.AddHeader("Content-Type", "text/xml");
            request.AddHeader("Accept", "text/xml");

            if (headers == null) return request;

            foreach (var header in headers)
            {
                request.AddHeader(header.Key, header.Value);
            }

            return request;
        }

        public static RestRequest GetXml(string ruta, IDictionary<string, string>? headers = null)
        {
            return ConfigurarRequestXml(ruta, Method.Get, headers!);
        }

        public static RestRequest PostXml(string ruta, object body, IDictionary<string, string>? headers = null)
        {
            RestRequest request = ConfigurarRequestXml(ruta, Method.Post, headers!);
            request.AddParameter("text/xml", body, ParameterType.RequestBody);
            return request;
        }

        public static RestRequest Get(string ruta, string bearerToken = "", IDictionary<string, string>? headers = null)
        {
            return ConfigurarRequest(ruta, Method.Get, bearerToken, headers!);
        }

        public static RestRequest GetWithBody(string ruta, object body, string bearerToken = "", IDictionary<string, string>? headers = null)
        {
            string serializedBody = JsonConvert.SerializeObject(body);
            RestRequest request = ConfigurarRequest(ruta, Method.Post, bearerToken, headers!);
            request.AddParameter("application/json; charset=utf-8", serializedBody, ParameterType.RequestBody);

            return request;
        }

        public static RestRequest Post(string ruta, object body, string bearerToken = "", IDictionary<string, string>? headers = null)
        {
            RestRequest request = ConfigurarRequest(ruta, Method.Post, bearerToken, headers!);
            request.AddJsonBody(body);
            return request;
        }

        public static RestRequest Put(string ruta, object body, string bearerToken = "", IDictionary<string, string>? headers = null)
        {
            RestRequest request = ConfigurarRequest(ruta, Method.Put, bearerToken, headers!);
            request.AddJsonBody(body);
            return request;
        }
    }
}
