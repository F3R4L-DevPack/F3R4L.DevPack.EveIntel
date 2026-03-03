using FluentAssertions;

namespace F3R4L.DevPack.EveIntel.Logger.Tests.LogFormattedTextHandler
{
    public class GetFileCreationTimeAsync : BaseClass
    {
        [Test]
        public async Task Returns_Correct_DateTime()
        {
            //  Arrange
            var logFileLines = new string[] {
                "Session started: 2024.01.01 12:00:00",
                "Listener: TestListener"
            };

            //  Act
            var result = await _objectUnderTest.GetFileCreationTimeAsync(logFileLines);

            //  Assert
            result.Should().Be(new DateTime(2024, 1, 1, 12, 0, 0));
        }

        [Test]
        public async Task MultiplePossibles_Returns_Correct_DateTime()
        {
            //  Arrange
            var logFileLines = new string[] {
                "Session started: 2024.01.01 12:00:00",
                "Listener: TestListener",
                "Session started: 2024.01.02 12:00:00"
            };

            //  Act
            var result = await _objectUnderTest.GetFileCreationTimeAsync(logFileLines);

            //  Assert
            result.Should().Be(new DateTime(2024, 1, 1, 12, 0, 0));
        }

        [Test]
        public async Task NoMatch_Throws_Exception()
        {
            //  Arrange
            var logFileLines = new string[] {
                "Listener: TestListener"
            };

            //  Act
            Func<Task> act = async () => await _objectUnderTest.GetFileCreationTimeAsync(logFileLines);

            //  Assert
            await act.Should().ThrowAsync<Exception>().WithMessage("Could not find session start time in log file.");
        }
    }
}
