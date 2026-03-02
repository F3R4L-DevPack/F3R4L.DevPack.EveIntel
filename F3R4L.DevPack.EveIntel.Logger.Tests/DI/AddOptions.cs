using F3R4L.DevPack.EveIntel.Logger.DependencyInjection;
using F3R4L.DevPack.EveIntel.Logger.Enums;
using F3R4L.DevPack.EveIntel.Logger.Models;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace F3R4L.DevPack.EveIntel.Logger.Tests.DI
{
    public class AddOptions
    {
        [Test]
        public void LoggerConfiguration_Correctly_LoadsFromFile()
        {
            //  Arrange
            var builder = Host.CreateApplicationBuilder();
            builder.Configuration.Sources.Clear();
            builder.Configuration.AddJsonFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Files\LoggerOptionsConfig.json"));

            //  Act
            builder.AddOptions();

            using IHost host = builder.Build();

            var services = host.Services;
            var options = services.GetService<IOptions<LoggerConfiguration>>()?.Value;

            //  Assert
            options.Should().NotBeNull();
            options?.LogLevel.Should().Be(LogLevel.Information);
            options?.LogFilePath.Should().Be("C:\\Logs\\EveIntel.log");
        }

        [Test]
        public void LoggerConfiguration_Correctly_Defaults()
        {
            //  Arrange
            var builder = Host.CreateApplicationBuilder();
            builder.Configuration.Sources.Clear();

            //  Act
            builder.AddOptions();

            using IHost host = builder.Build();

            var services = host.Services;
            var options = services.GetService<IOptions<LoggerConfiguration>>()?.Value;

            //  Assert
            options.Should().NotBeNull();
            options?.LogLevel.Should().Be(LogLevel.Error);
            options?.LogFilePath.Should().Be(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs.txt"));
        }
    }
}