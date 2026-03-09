using F3R4L.DevPack.EveIntel.Logger.Models;
using FluentAssertions;

namespace F3R4L.DevPack.EveIntel.Logger.Tests.LogFormattedTextHandler
{
    public class DeserializeNextAsync : BaseClass
    {
        [Test]
        public async Task Returns_Correct_LogLines()
        {
            //  Arrange
            var context = new string[] {
                "Header Line 1",
                "Header Line 2",
                "Header Line 3",
                "Header Line 4",
                "Header Line 5",
                "Header Line 6",
                "Header Line 7",
                "Header Line 8",
                "Header Line 9",
                "Header Line 10",
                "Header Line 11",
                "Header Line 12",
                "Header Line 13",
                "﻿[ 2026.03.02 11:51:59 ] NORTHERN HUNT3R > Nari Kashada  3USX-F",
                "﻿[ 2026.03.02 11:52:15 ] NORTHERN HUNT3R > Pacifier",
                "﻿[ 2026.03.02 11:52:31 ] Momonogi Kana > 4-HWWF  Aniki EX-S  Aniki EX-Z"
            };
            var allSystems = new SystemData[] {
                new SystemData { SolarSystemName = "3USX-F" },
                new SystemData { SolarSystemName = "4-HWWF" }
            };

            //  Act
            var result = await _objectUnderTest.DeserializeNextAsync(context, allSystems);

            //  Assert
            result.Should().HaveCount(3);
            result[0].LogDateTime.ToString("yyyy-MM-ddTHH:mm:ss").Should().Be("2026-03-02T11:51:59");
            result[0].UserName.Should().Be("NORTHERN HUNT3R");
            result[0].SystemName.Should().Be("3USX-F");
            result[0].Message.Should().Be("Nari Kashada  3USX-F");
            result[1].LogDateTime.ToString("yyyy-MM-ddTHH:mm:ss").Should().Be("2026-03-02T11:52:15");
            result[1].UserName.Should().Be("NORTHERN HUNT3R");
            result[1].SystemName.Should().Be("UNKNOWN SYSTEM");
            result[1].Message.Should().Be("Pacifier");
            result[2].LogDateTime.ToString("yyyy-MM-ddTHH:mm:ss").Should().Be("2026-03-02T11:52:31");
            result[2].UserName.Should().Be("Momonogi Kana");
            result[2].SystemName.Should().Be("4-HWWF");
            result[2].Message.Should().Be("4-HWWF  Aniki EX-S  Aniki EX-Z");
        }
    }
}
