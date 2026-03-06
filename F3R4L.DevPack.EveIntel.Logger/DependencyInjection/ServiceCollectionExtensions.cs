using Microsoft.Extensions.DependencyInjection;

namespace F3R4L.DevPack.EveIntel.Logger.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IFileHandler, FileHandler>();
            services.AddScoped<IDateTimeWrapper, DateTimeWrapper>();
            services.AddScoped<ICsvHandler, CsvHandler>();
            services.AddScoped<ILogFormattedTextHandler, LogFormattedTextHandler>();
            services.AddScoped<ILogger<Worker>, Logger<Worker>>();
            services.AddScoped<IWorker, Worker>();
        }
    }
}
