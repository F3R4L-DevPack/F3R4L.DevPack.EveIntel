using FluentAssertions;

namespace F3R4L.DevPack.EveIntel.Logger.Tests.LogFormattedTextHandler
{
    public class GetLoggerNameAsync : BaseClass
    {
        [Test]
        public async Task Returns_Correct_Listener_Name()
        {
            //  Arrange
            var logFileLines = new string[] {
                "Session started: 2024.01.01 12:00:00",
                "Listener: TestListener"
            };

            //  Act
            var result = await _objectUnderTest.GetLoggerNameAsync(logFileLines);

            //  Assert
            result.Should().Be("Listener");
        }

        [Test]
        public async Task MultiplePossibles_Returns_Correct_Listener_Name()
        {
            //  Arrange
            var logFileLines = new string[] {
                "Session started: 2024.01.01 12:00:00",
                "Listener: TestListener",
                "Listener: TestListener1"
            };

            //  Act
            var result = await _objectUnderTest.GetLoggerNameAsync(logFileLines);

            //  Assert
            result.Should().Be("Listener");
        }

        [Test]
        public async Task NoMatch_Throws_Exception()
        {
            //  Arrange
            var logFileLines = new string[] {
                "Session started: 2024.01.01 12:00:00"
            };

            //  Act
            Func<Task> act = async () => await _objectUnderTest.GetLoggerNameAsync(logFileLines);

            //  Assert
            await act.Should().ThrowAsync<Exception>().WithMessage("Could not find listener name in log file.");
        }
    }
}
