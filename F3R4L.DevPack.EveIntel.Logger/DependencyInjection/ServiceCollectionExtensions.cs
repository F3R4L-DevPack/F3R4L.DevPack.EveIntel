using Microsoft.Extensions.DependencyInjection;

namespace F3R4L.DevPack.EveIntel.Logger.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IFileHandler, FileHandler>();
            services.AddSingleton<IDateTimeWrapper, DateTimeWrapper>();
            services.AddSingleton<ICsvHandler, CsvHandler>();
            services.AddSingleton<ILogFormattedTextHandler, LogFormattedTextHandler>();
        }
    }
}
