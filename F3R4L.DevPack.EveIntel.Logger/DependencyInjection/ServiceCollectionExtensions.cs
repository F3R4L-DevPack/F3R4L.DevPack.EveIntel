using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F3R4L.DevPack.EveIntel.Logger.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IFileHandler, FileHandler>();
            services.AddSingleton<IDateTimeWrapper, DateTimeWrapper>();
            //services.AddSingleton<ILogProcessor, LogProcessor>();
            //services.AddSingleton<ILogStorage, LogStorage>();
        }
    }
}
