using FluentAssertions;
using TestArticles = F3R4L.DevPack.EveIntel.Logger;

namespace F3R4L.DevPack.EveIntel.Logger.Tests.FileHandler
{
    public class WriteToFileAsync
    {
        public TestArticles.FileHandler _objectUnderTest;

        private string _filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Files", "WriteToFileAsync.txt");

        [OneTimeSetUp]
        public void Setup()
        {
            _objectUnderTest = new TestArticles.FileHandler();
        }

        [Test]
        public async Task WritesToCorrectFile()
        {
            //  Arrange
            var content = new[] { "Line 1", "Line 2", "Line 3" };

            //  Act
            await _objectUnderTest.WriteTextToFileAsync(_filePath, content);

            //  Assert
            var fileContent = await File.ReadAllLinesAsync(_filePath);
            fileContent.Should().Equal(content);
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            if (File.Exists(_filePath))
            {
                File.Delete(_filePath);
            }
        }
}
