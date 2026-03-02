using F3R4L.DevPack.EveIntel.Logger.Enums;

namespace F3R4L.DevPack.EveIntel.Logger.Models
{
    public class LoggerConfiguration
    {
        public LogLevel LogLevel { get; set; }
        public string LogFilePath { get; set; }

        public LoggerConfiguration(LogLevel logLevel)
        {
            LogLevel = logLevel;
            LogFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs.txt");
        }

        public LoggerConfiguration(string logFilePath)
        {
            LogLevel = LogLevel.Error;
            LogFilePath = logFilePath;
        }

        public LoggerConfiguration(string logFilePath, LogLevel logLevel)
        {
            LogFilePath = logFilePath;
            LogLevel = logLevel;
        }

        public LoggerConfiguration()
        {
            LogLevel = LogLevel.Error;
            LogFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs.txt");
        }
    }
}
