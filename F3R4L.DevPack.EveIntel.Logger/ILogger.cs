using F3R4L.DevPack.EveIntel.Logger.Enums;

namespace F3R4L.DevPack.EveIntel.Logger
{
    public interface ILogger<T>
    {
        Task LogAsync(string message, LogLevel logLevel);
    }
}