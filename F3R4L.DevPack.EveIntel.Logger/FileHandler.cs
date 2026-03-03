
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Net.WebSockets;
using System.Reflection;
using System.Reflection.Emit;
using System.Reflection.PortableExecutable;
using System.Text;

namespace F3R4L.DevPack.EveIntel.Logger
{
    [ExcludeFromCodeCoverage]
    public class FileHandler : IFileHandler
    {
        public async Task<string[]> ReadTextFileAsync(string filePath)
        {
            if (File.Exists(filePath))
            {
                return await File.ReadAllLinesAsync(filePath);
            }
            else
            {
                throw new FileNotFoundException($"The file '{filePath}' was not found.");
            }
        }

        public async Task WriteTextToFileAsync(string filePath, params string[] content)
        {
            if(!File.Exists(filePath))
            {
                await File.Create(filePath).DisposeAsync();
            }
            await File.AppendAllLinesAsync(filePath, content, Encoding.UTF8);
        }

        public Task<string[]> GetFileNamesAsync(string folder, string pattern = "")
        {
            if (folder == string.Empty)
            {
                throw new ArgumentException("Folder path cannot be empty.", nameof(folder));
            }
            else if (!Directory.Exists(folder))
            {
                throw new DirectoryNotFoundException($"The directory '{folder}' was not found.");
            }
            else if (string.IsNullOrWhiteSpace(pattern))
            {
                return Task.FromResult(Directory.GetFiles(folder));
            }
            else
            {
                return Task.FromResult(Directory.GetFiles(folder, pattern));
            }
        }

        public Task<string[]> GetEmbeddedResourceFileNamesAsync(Assembly? assembly = null, string subFolder = "Files")
        {
            return Task.Run(() => {
                if (assembly == null)
                {
                    assembly = Assembly.GetAssembly(typeof(FileHandler));
                }
                string folderName = string.Format("{0}.{1}", assembly.GetName().Name, subFolder);
                return assembly
                    .GetManifestResourceNames()
                    .Where(r => r.StartsWith(folderName))
                    .ToArray();
            });
        }

        public Task<string[]> ReadEmbeddedResourceTextFileAsync(string fileName, Assembly? assembly = null, string subFolder = "Files")
        {
            return Task.Run(async () =>
            {
                if (assembly == null)
                {
                    assembly = Assembly.GetAssembly(typeof(FileHandler));
                }
                string embeddedFileName = string.Format("{0}.{1}.{2}", assembly.GetName().Name, subFolder, fileName);
                using var stream = assembly.GetManifestResourceStream(embeddedFileName);
                using var streamReader = new StreamReader(stream);

                var results = new List<string>();
                string line;
                while ((line = await streamReader.ReadLineAsync()) != null)
                {
                    results.Add(line);
                }
                return results.ToArray();
            });
        }
    }
}
