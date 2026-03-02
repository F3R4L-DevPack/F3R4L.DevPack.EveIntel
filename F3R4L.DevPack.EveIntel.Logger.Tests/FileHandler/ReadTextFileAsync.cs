using FluentAssertions;
using TestArticles = F3R4L.DevPack.EveIntel.Logger;

namespace F3R4L.DevPack.EveIntel.Logger.Tests.FileHandler
{
    public class ReadTextFileAsync
    {
        public TestArticles.FileHandler _objectUnderTest;

        [SetUp]
        public void Setup()
        {
            _objectUnderTest = new TestArticles.FileHandler();
        }

        [Test]
        public async Task Returns_Correct_Content()
        {
            //  Arrange
            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Files", "ReadTextFileAsync.txt");
            var expectedContent = new[] { "Line 1", "Line 2", "Line 3" };
            await File.WriteAllLinesAsync(filePath, expectedContent);

            //  Act
            var content = await _objectUnderTest.ReadTextFileAsync(filePath);

            //  Assert
            content.Should().Equal(expectedContent);
            File.Delete(filePath);
        }

        [Test]
        public async Task Throws_FileNotFoundException_For_NonExistentFile()
        {
            //  Arrange
            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Files", "NonExistentFile.txt");

            //  Act
            Func<Task> act = async () => await _objectUnderTest.ReadTextFileAsync(filePath);

            //  Assert
            await act.Should().ThrowAsync<FileNotFoundException>();
        }
    }
}
