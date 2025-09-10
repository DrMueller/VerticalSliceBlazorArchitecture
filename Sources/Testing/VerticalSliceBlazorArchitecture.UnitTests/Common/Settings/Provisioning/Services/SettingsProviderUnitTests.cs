using FluentAssertions;
using Microsoft.Extensions.Options;
using Moq;
using VerticalSliceBlazorArchitecture.Common.Settings.Provisioning.Models;
using VerticalSliceBlazorArchitecture.Common.Settings.Provisioning.Services.Implementation;
using Xunit;

namespace VerticalSliceBlazorArchitecture.UnitTests.Common.Settings.Provisioning.Services
{
    public class SettingsProviderUnitTests
    {
        private readonly Mock<IOptions<AppSettings>> _appSettingsMock;
        private readonly SettingsProvider _sut;

        public SettingsProviderUnitTests()
        {
            _appSettingsMock = new Mock<IOptions<AppSettings>>();

            _sut = new SettingsProvider(_appSettingsMock.Object);
        }

        [Fact]
        public void AppSettings_ReturnsAppSettings()
        {
            // Arrange
            var settings = new AppSettings();
            _appSettingsMock.Setup(f => f.Value).Returns(settings);

            // Act
            var actualSettings = _sut.AppSettings;

            // Assert
            actualSettings.Should().Be(settings);
        }
    }
}