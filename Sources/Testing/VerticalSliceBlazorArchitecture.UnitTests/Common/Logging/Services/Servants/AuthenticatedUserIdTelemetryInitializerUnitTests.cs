using FluentAssertions;
using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.DataContracts;
using Moq;
using VerticalSliceBlazorArchitecture.Common.Logging.Services;
using VerticalSliceBlazorArchitecture.Common.Logging.Services.Models;
using VerticalSliceBlazorArchitecture.Common.Logging.Services.Servants.Implementation;
using Xunit;

namespace VerticalSliceBlazorArchitecture.UnitTests.Common.Logging.Services.Servants
{
    public class AuthenticatedUserIdTelemetryInitializerUnitTests
    {
        private readonly Mock<ILogInfoProvider> _logInfoProviderMock;
        private readonly AuthenticatedUserIdTelemetryInitializer _sut;

        public AuthenticatedUserIdTelemetryInitializerUnitTests()
        {
            _logInfoProviderMock = new Mock<ILogInfoProvider>();
            _sut = new AuthenticatedUserIdTelemetryInitializer(_logInfoProviderMock.Object);
        }

        [Fact]
        public void Initializing_SetsAuthenticatedUserId()
        {
            // Arrange
            const string UserEmail = "test@test.ch";

            _logInfoProviderMock.Setup(f => f.ProvideLogInfo()).Returns(new LogInfo(UserEmail));

            var telemetryMock = new Mock<ITelemetry>();
            var telemetryContext = new TelemetryContext();

            telemetryMock.Setup(f => f.Context)
                .Returns(telemetryContext);

            // Act
            _sut.Initialize(telemetryMock.Object);

            // Assert
            telemetryContext.User.AuthenticatedUserId.Should().Be(UserEmail);
        }
    }
}