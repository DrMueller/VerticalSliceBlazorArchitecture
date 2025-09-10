using FluentAssertions;
using VerticalSliceBlazorArchitecture.Common.Logging.Services;
using VerticalSliceBlazorArchitecture.Presentation.Infrastructure.Testing.Controllers;
using VerticalSliceBlazorArchitecture.QualityTests.TestingInfrastructure.Fixtures.Quality;
using VerticalSliceBlazorArchitecture.Testing.Common.Mocks;
using Xunit;

namespace VerticalSliceBlazorArchitecture.QualityTests.TestingAreas.CrossCutting.ExceptionHandling
{
    public class ApiExceptionHandlingTests(QualityTestFixture fixture) : QualityTestBase(fixture)
    {
        [Fact]
        public async Task ThrowingUncaughtException_LogsException()
        {
            // Arrange
            using var client = AppFactory.CreateClient();

            // Act
            await client.GetAsync(new Uri("api/test/exception", UriKind.Relative));

            // Assert
            var loggingServiceMock = (LoggingServiceMock)AppFactory.Services.GetRequiredService<ILoggingService>();
            loggingServiceMock.LoggedException.Should().NotBeNull();
            loggingServiceMock.LoggedException!.Message.Should().Be(TestController.ExceptionMessage);
        }
    }
}