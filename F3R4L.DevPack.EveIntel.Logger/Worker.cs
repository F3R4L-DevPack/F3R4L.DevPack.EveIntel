using F3R4L.DevPack.EveIntel.Logger.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F3R4L.DevPack.EveIntel.Logger
{
    public class Worker
    {
        private readonly IFileHandler _fileHandler;
        private readonly ILogFormattedTextHandler _logFormattedTextHandler;
        private readonly ICsvHandler _csvHandler;
        private readonly IDateTimeWrapper _dateTimeWrapper;
        private readonly ILogger<Worker> _logger;

        //  Initialised to the number of lines to skip in the log file before reaching the first log entry. This is based on the standard EVE Online log file format, which includes metadata and headers in the first 13 lines.
        public static int LinesRead = 13;
        public static SystemData[] AllSystems = Array.Empty<SystemData>();
        
        public EventHandler<NewIntelLogEventArgs>? NewLogInfo;

        public async Task ExecuteAsync(string fileName)
        {
            await _logger.LogAsync($"Worker is processing {fileName}.", Enums.LogLevel.Information);

            try
            {
                var fileLines = await _fileHandler.ReadTextFileAsync(fileName);
                await _logger.LogAsync($"Successfully read {fileLines.Count() - LinesRead} lines from {fileName}.", Enums.LogLevel.Debug);

                if (fileLines.Count() > LinesRead)
                {
                    if (AllSystems.Count() == 0)
                    {
                        AllSystems = await _csvHandler.DeserializeAsync<SystemData>(
                            await _fileHandler.ReadEmbeddedResourceTextFileAsync("system-region-lookup.csv")
                        );
                        await _logger.LogAsync($"Successfully read {AllSystems.Count()} systems from embedded data.", Enums.LogLevel.Debug);
                    }
                    var logEntries = await _logFormattedTextHandler.DeserializeNextAsync(fileLines, AllSystems, LinesRead);
                    LinesRead += logEntries.Count();

                    OnExecuteCompleted(logEntries);
                }
            }
            catch(Exception ex)
            {
                await _logger.LogAsync($"An error occurred while processing {fileName}. Error details: {ex.Message}", Enums.LogLevel.Error);
            }

            await _logger.LogAsync($"Worker process is complete.", Enums.LogLevel.Information);
        }

        protected virtual void OnExecuteCompleted(LogLine[] logLines) //protected virtual method
        {
            NewLogInfo?.Invoke(this, 
                new NewIntelLogEventArgs { LogLines = logLines });
        }
    }
}
