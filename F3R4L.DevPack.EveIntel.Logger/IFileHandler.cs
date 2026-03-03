using System.Reflection;

namespace F3R4L.DevPack.EveIntel.Logger
{
    public interface IFileHandler
    {
        Task<string[]> ReadTextFileAsync(string filePath);

        Task WriteTextToFileAsync(string filePath, params string[] content);
        Task<string[]> GetFileNamesAsync(string folder, string pattern = "");

        Task<string[]> GetEmbeddedResourceFileNamesAsync(Assembly? assembly = null, string subFolder = "Files");
        Task<string[]> ReadEmbeddedResourceTextFileAsync(string fileName, Assembly? assembly = null, string subFolder = "Files");
    }
}