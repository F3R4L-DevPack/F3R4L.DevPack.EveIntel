using F3R4L.DevPack.EveIntel.Logger.Models;

namespace F3R4L.DevPack.EveIntel.Logger
{
    public interface ILogFormattedTextHandler
    {
        Task<LogLine[]> DeserializeNextAsync(string[] context, SystemData[] allSystems, int skipCount = 13);
        Task<DateTime> GetFileCreationTimeAsync(string[] logFileLines);
        Task<string> GetLoggerNameAsync(string[] logFileLines);
    }
}