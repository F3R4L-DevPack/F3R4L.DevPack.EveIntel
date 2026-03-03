using F3R4L.DevPack.EveIntel.Logger.Models;
using System.Linq;

namespace F3R4L.DevPack.EveIntel.Logger
{
    public class LogFormattedTextHandler : ILogFormattedTextHandler
    {
        public Task<DateTime> GetFileCreationTimeAsync(string[] logFileLines)
        {
            return Task.Run(async () =>
            {
                var separator = new string[] { ": " };
                var matchedLine = await SearchLinesAsync(logFileLines, "Session started:");

                if (string.IsNullOrEmpty(matchedLine))
                {
                    throw new Exception("Could not find session start time in log file.");
                }

                return Convert.ToDateTime(matchedLine.Split(separator, StringSplitOptions.RemoveEmptyEntries)[1]);
            });
        }

        public Task<string> GetLoggerNameAsync(string[] logFileLines)
        {
            return Task.Run(async () =>
            {
                var separator = new string[] { ": " };
                var matchedLine = await SearchLinesAsync(logFileLines, "Listener:");

                if (string.IsNullOrEmpty(matchedLine))
                {
                    throw new Exception("Could not find listener name in log file.");
                }

                return matchedLine.Split(separator, StringSplitOptions.RemoveEmptyEntries).First();
            });
        }

        public Task<LogLine[]> DeserializeNextAsync(string[] context, SystemData[] allSystems, int skipCount = 13)
        {
            if (context == null || context.Count() == 0)
            {
                return Task.FromResult(Array.Empty<LogLine>());
            }
            return Task.Run(() =>
            {
                var useLines = context.Skip(skipCount);
                return useLines.Select(line => new LogLine(line, allSystems)).ToArray();
            });
        }

        internal virtual Task<string?> SearchLinesAsync(string[] logFileLines, string pattern)
        {
            return Task.Run(() =>
            {
                return logFileLines.FirstOrDefault(line => line.Contains(pattern));
            });
        }
    }
}
