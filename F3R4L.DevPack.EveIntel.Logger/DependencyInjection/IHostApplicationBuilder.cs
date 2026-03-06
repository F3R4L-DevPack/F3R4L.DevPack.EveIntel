using F3R4L.DevPack.EveIntel.Logger.Models;
using Microsoft.Extensions.DependencyInjection;

namespace F3R4L.DevPack.EveIntel.Logger.DependencyInjection
{
    public static class IHostApplicationBuilder
    {
        public static void AddOptions(this Microsoft.Extensions.Hosting.IHostApplicationBuilder builder)
        {
            builder.Services.Configure<LoggerConfiguration>(builder.Configuration.GetSection(nameof(LoggerConfiguration)));
        }
    }
}
