using FluentAssertions;
using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.DataContracts;
using Moq;
using VerticalSliceBlazorArchitecture.Common.Logging.Services.Servants.Implementation;
using VerticalSliceBlazorArchitecture.Common.Settings.Provisioning.Models;
using VerticalSliceBlazorArchitecture.Common.Settings.Provisioning.Services;
using Xunit;

namespace VerticalSliceBlazorArchitecture.UnitTests.Common.Logging.Services.Servants
{
    public class TelemetryInitializerUnitTests
    {
        private readonly Mock<ISettingsProvider> _settingsProviderMock;
        private readonly TelemetryInitializer _sut;

        public TelemetryInitializerUnitTests()
        {
            _settingsProviderMock = new Mock<ISettingsProvider>();
            _sut = new TelemetryInitializer(_settingsProviderMock.Object);
        }

        [Fact]
        public void Initializing_SetsRoleInstance()
        {
            // Arrange
            const string EnvName = "Hello123";
            var appSettings = new AppSettings
            {
                EnvironmentName = EnvName
            };

            _settingsProviderMock
                .SetupGet(f => f.AppSettings)
                .Returns(appSettings);

            var telemetryMock = new Mock<ITelemetry>();
            var telemetryContext = new TelemetryContext();

            telemetryMock.Setup(f => f.Context)
                .Returns(telemetryContext);

            // Act
            _sut.Initialize(telemetryMock.Object);

            // Assert
            telemetryContext.Cloud.RoleInstance.Should().Be($"{TelemetryInitializer.RoleInstanceName}_{EnvName}");
        }
    }
}