
using F3R4L.DevPack.EveIntel.Logger.Models;

namespace F3R4L.DevPack.EveIntel.Logger
{
    public interface IWorker
    {
        event EventHandler<NewIntelLogEventArgs>? NewLogInfo;
        Task ExecuteAsync(string fileName);
    }
}