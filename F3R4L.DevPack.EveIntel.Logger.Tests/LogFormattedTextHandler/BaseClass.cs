using TestArticles = F3R4L.DevPack.EveIntel.Logger;

namespace F3R4L.DevPack.EveIntel.Logger.Tests.LogFormattedTextHandler
{
    public abstract class BaseClass
    {
        public TestArticles.LogFormattedTextHandler _objectUnderTest;

        [SetUp]
        public void Setup()
        {
            _objectUnderTest = new TestArticles.LogFormattedTextHandler();
        }
    }
}