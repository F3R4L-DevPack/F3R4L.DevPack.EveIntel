using FluentAssertions;
using TestArticles = F3R4L.DevPack.EveIntel.Logger;

namespace F3R4L.DevPack.EveIntel.Logger.Tests.FileHandler
{
    public class ReadEmbeddedResourceTextFileAsync
    {
        public TestArticles.FileHandler _objectUnderTest;

        [OneTimeSetUp]
        public void Setup()
        {
            _objectUnderTest = new TestArticles.FileHandler();
        }

        [Test]
        public async Task ReadEmbeddedResourceTextFileAsync_Returns_Correct_Content()
        {
            //  Arrange
            var fileName = "system-region-lookup.csv";

            //  Act
            var result = await _objectUnderTest.ReadEmbeddedResourceTextFileAsync(fileName);

            //  Assert
            result.Count().Should().Be(8438);
            result.First().Should().Be("regionID,regionName,solarSystemID,solarSystemName");
        }
    }
}
