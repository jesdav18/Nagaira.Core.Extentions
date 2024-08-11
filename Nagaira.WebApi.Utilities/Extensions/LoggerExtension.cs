using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Json;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace Nagaira.WebApi.Utilities.Extensions
{
    public class LoggerHelper : IDisposable
    {
        private static readonly ILoggerFactory LoggerFactory = new LoggerFactory().AddSerilog();

        public LoggerHelper(string directoryBase)
        {
            string logPath = Path.Combine(directoryBase, "logs");
            string logFile = Path.Combine(logPath, $".json");

            Log.Logger = new LoggerConfiguration()
                .WriteTo.File(new JsonFormatter(), logFile, rollingInterval: RollingInterval.Day, restrictedToMinimumLevel: LogEventLevel.Debug, fileSizeLimitBytes: null, flushToDiskInterval: TimeSpan.FromSeconds(1))
                .CreateLogger();
        }

        public ILogger<T> CreateLogger<T>()
        {
            return LoggerFactory.CreateLogger<T>();
        }

        public void LogInformation(ILogger logger, string message, params object[] args)
        {
            string formattedMessage = FormatLogMessage("INFO", message, args);
            logger.LogInformation(formattedMessage);
        }

        public void LogWarning(ILogger logger, string message, params object[] args)
        {
            string formattedMessage = FormatLogMessage("WARNING", message, args);
            logger.LogWarning(formattedMessage);
        }

        public void LogError(ILogger logger, Exception exception, string message, params object[] args)
        {
            string formattedMessage = FormatLogMessage("ERROR", message, args);
            logger.LogError(exception, formattedMessage);
        }

        private string FormatLogMessage(string logLevel, string message, params object[] args)
        {

            string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            string formattedMessage = $"{timestamp} | {logLevel}  | {string.Format(message, args)}";

            return formattedMessage;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            Log.CloseAndFlush();
        }
    }

}