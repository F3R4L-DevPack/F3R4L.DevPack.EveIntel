using F3R4L.DevPack.EveIntel.Logger.Enums;
using F3R4L.DevPack.EveIntel.Logger.Models;
using Microsoft.Extensions.Options;

namespace F3R4L.DevPack.EveIntel.Logger
{
    public class Logger<T> : ILogger
    {
        private readonly IFileHandler _fileHandler;
        private readonly IDateTimeWrapper _dateTimeWrapper;
        private readonly IOptions<LoggerConfiguration> _config;

        public event EventHandler<LogEventArgs>? LogEntryAdded;
        

        public Logger(IFileHandler fileHandler, IDateTimeWrapper dateTimeWrapper, IOptions<LoggerConfiguration> config)
        {
            _fileHandler = fileHandler;
            _dateTimeWrapper = dateTimeWrapper;
            _config = config;
        }

        public async Task LogAsync(string message, LogLevel logLevel)
        {
            if (logLevel < _config.Value.LogLevel)
                return;

            var logEntryArgs = new LogEventArgs(_dateTimeWrapper.UtcNow, $"[{logLevel}] {typeof(T).Name}: {message}");
            await _fileHandler.WriteTextToFileAsync(_config.Value.LogFilePath, logEntryArgs.ToString());

            OnLogEntryAdded(logEntryArgs);
        }

        protected virtual void OnLogEntryAdded(LogEventArgs logEntry)
        {
            LogEntryAdded?.Invoke(this, logEntry);
        }
    }
}
