using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;

namespace Nagaira.WebApi.Utilities.Extensions
{
    public class LogEndpointHelper
    {
        private readonly string _logDirectory;


        public LogEndpointHelper(string logDirectory)
        {
            _logDirectory = logDirectory;
        }


        public async Task HandleLogsEndpoint(HttpContext context)
        {
            var logDate = DateTime.Now.ToString("yyyyMMdd");
            var logFilePath = Path.Combine(_logDirectory, "logs", $"{logDate}.json");

            if (!File.Exists(logFilePath))
            {
                context.Response.StatusCode = StatusCodes.Status404NotFound;
                await context.Response.WriteAsync("El archivo de log no existe.");
                return;
            }

            using (var streamReader = new StreamReader(logFilePath))
            {
                var logContent = await streamReader.ReadToEndAsync();

                var lines = logContent.Split('\n', StringSplitOptions.RemoveEmptyEntries);
                var jsonArray = new JArray();


                foreach (var line in lines)
                {
                    jsonArray.Add(JObject.Parse(line));
                }

                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(jsonArray.ToString());
            }
        }
    }

}
