using F3R4L.DevPack.EveIntel.Logger.DependencyInjection;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F3R4L.DevPack.EveIntel.Logger.Tests.DI
{
    public class AddServices
    {
        private IServiceProvider _objectUnderTest;

        [OneTimeSetUp]
        public void Setup()
        {
            var builder = Host.CreateApplicationBuilder();
            builder.Services.AddServices();

            _objectUnderTest = builder.Build().Services;
        }

        [Test]
        public void IFileHandler_Is_Registered()
        {
            //  Arrange & Act
            var fileHandler = _objectUnderTest.GetService<IFileHandler>();

            //  Assert
            fileHandler.Should().NotBeNull();
        }

        [Test]
        public void IDateTimeWrapper_Is_Registered()
        {
            //  Arrange & Act
            var dateTimeWrapper = _objectUnderTest.GetService<IDateTimeWrapper>();

            //  Assert
            dateTimeWrapper.Should().NotBeNull();
        }
    }
}
