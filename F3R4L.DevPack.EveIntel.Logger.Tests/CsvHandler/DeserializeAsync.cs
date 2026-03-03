using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestArticles = F3R4L.DevPack.EveIntel.Logger;

namespace F3R4L.DevPack.EveIntel.Logger.Tests.CsvHandler
{
    public class DeserializeAsync
    {
        private TestArticles.CsvHandler _objectUnderTest;

        [SetUp]
        public void Setup()
        {
            _objectUnderTest = new TestArticles.CsvHandler();
        }

        [Test]
        public async Task Returns_EmptyArray_WhenGivenEmptyContext()
        {
            // Arrange
            string[] context = Array.Empty<string>();

            // Act
            var result = await _objectUnderTest.DeserializeAsync<object>(context);

            // Assert
            result.Count().Should().Be(0);
        }

        [Test]
        public async Task Returns_Properly_When_GivenValidContext()
        {
            // Arrange
            string[] context = new[] { "Column1,Column2", "Value1,Value2", "Value3,Value4" };

            // Act
            var result = await _objectUnderTest.DeserializeAsync<TestObject>(context);

            // Assert
            result.Count().Should().Be(2);
            result.First().Column1.Should().Be("Value1");
            result.First().Column2.Should().Be("Value2");
            result.Last().Column1.Should().Be("Value3");
            result.Last().Column2.Should().Be("Value4");
        }

        private class TestObject
        {
            public string Column1 { get; set; }
            public string Column2 { get; set; }
        }
    }
}
