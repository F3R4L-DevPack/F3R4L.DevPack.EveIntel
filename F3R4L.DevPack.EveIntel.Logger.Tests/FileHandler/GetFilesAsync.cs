using FluentAssertions;
using TestArticles = F3R4L.DevPack.EveIntel.Logger;

namespace F3R4L.DevPack.EveIntel.Logger.Tests.FileHandler
{
    public class GetFilesAsync
    {
        public TestArticles.FileHandler _objectUnderTest;

        [OneTimeSetUp]
        public void Setup()
        {
            _objectUnderTest = new TestArticles.FileHandler();
        }

        [Test]
        public async Task GetFileNamesAsync_NoFilter_Returns_Correct_Files()
        {
            //  Arrange
            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Files");

            //  Act
            var files = await _objectUnderTest.GetFileNamesAsync(filePath);

            //  Assert
            files.Count().Should().Be(4);
        }

        [Test]
        public async Task GetFileNamesAsync_TxtFilter_Returns_Correct_Files()
        {
            //  Arrange
            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Files");

            //  Act
            var files = await _objectUnderTest.GetFileNamesAsync(filePath, "*.txt");

            //  Assert
            files.Count().Should().Be(3);
        }
    }
}
