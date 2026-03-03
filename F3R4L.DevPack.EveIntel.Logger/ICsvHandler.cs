
namespace F3R4L.DevPack.EveIntel.Logger
{
    public interface ICsvHandler
    {
        Task<T[]> DeserializeAsync<T>(string[] context);
    }
}