using F3R4L.DevPack.EveIntel.Logger.Enums;
using F3R4L.DevPack.EveIntel.Logger.Models;
using FluentAssertions;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F3R4L.DevPack.EveIntel.Logger.Tests.LoggerOfT
{
    public class LogAsync
    {
        private Logger<LogAsync> _objectUnderTest;
        private Mock<IFileHandler> _fileHandler;
        private Mock<IDateTimeWrapper> _dateTimeWrapper;

        private Mock<IOptions<LoggerConfiguration>> _configuration;

        [SetUp]
        public void Setup()
        {
            _fileHandler = new Mock<IFileHandler>();
            _dateTimeWrapper = new Mock<IDateTimeWrapper>();
            _configuration = new Mock<IOptions<LoggerConfiguration>>();

            _dateTimeWrapper.SetupGet(d => d.UtcNow).Returns(new DateTime(2024, 1, 1, 12, 0, 0));

            _objectUnderTest = new Logger<LogAsync>(_fileHandler.Object, 
                _dateTimeWrapper.Object, _configuration.Object);
        }

        [TestCase(LogLevel.Error, LogLevel.Information, "Test1", false, false)]
        [TestCase(LogLevel.Information, LogLevel.Information, "Test1", true, false)]
        [TestCase(LogLevel.Information, LogLevel.Error, "Test1", true, false)]
        [TestCase(LogLevel.Error, LogLevel.Error, "Test1", true, false)]
        [TestCase(LogLevel.Information, LogLevel.Information, "Test1", true, true)]
        [TestCase(LogLevel.Information, LogLevel.Error, "Test1", true, true)]
        [TestCase(LogLevel.Error, LogLevel.Error, "Test1", true, true)]
        public async Task LogsCorrectly(LogLevel configLogLevel, LogLevel logAtLevel, string logMessage, 
            bool expectedMockCalls, bool expectEvent)
        {
            //  Arrange
            _configuration.SetupGet(c => c.Value).Returns(new LoggerConfiguration
            {
                LogLevel = configLogLevel,
                LogFilePath = "testlogs.txt"
            });
            LogEventArgs? eventArgs = default;
            ;
            if (expectEvent)
            {
                _objectUnderTest.LogEntryAdded += (sender, args) =>
                {
                    eventArgs = args;
                };
            }

            //  Act
            await _objectUnderTest.LogAsync(logMessage, logAtLevel);

            //  Assert
            var expectedCalls = expectedMockCalls ? 1 : 0;
            _dateTimeWrapper.VerifyGet(d => d.UtcNow, Times.Exactly(expectedCalls));
            _fileHandler.Verify(v => v.WriteTextToFileAsync("testlogs.txt", $"2024-01-01 12:00:00 [{logAtLevel}] {typeof(LogAsync).Name}: {logMessage}"), Times.Exactly(expectedCalls));

            if(expectEvent)
            {
                eventArgs.Should().NotBeNull();
            }
            else
            {
                eventArgs.Should().BeNull();
            }
        }
    }
}
