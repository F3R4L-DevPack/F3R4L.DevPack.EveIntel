using FluentAssertions;
using TestArticles = F3R4L.DevPack.EveIntel.Logger;

namespace F3R4L.DevPack.EveIntel.Logger.Tests.FileHandler
{
    public class GetEmbeddedResourceFileNamesAsync
    {
        public TestArticles.FileHandler _objectUnderTest;

        [OneTimeSetUp]
        public void Setup()
        {
            _objectUnderTest = new TestArticles.FileHandler();
        }

        [Test]
        public async Task GetEmbeddedResourceFileNamesAsync_Returns_Correct_Files()
        {
            //  Act
            var files = await _objectUnderTest.GetEmbeddedResourceFileNamesAsync();

            //  Assert
            files.Count().Should().Be(1);
        }
    }
}
