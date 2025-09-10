using Moq;
using VerticalSliceBlazorArchitecture.Application.Features.Versioning.GetAppVersion;
using VerticalSliceBlazorArchitecture.Application.Mediation.Services.Handlers;
using VerticalSliceBlazorArchitecture.Common.Logging.Services;
using Xunit;

namespace VerticalSliceBlazorArchitecture.UnitTests.Application.Mediation.Services.Handlers
{
    public class LogPreProcessorUnitTests
    {
        private readonly Mock<ILoggingService> _loggingServiceMock = new();

        [Fact]
        public async Task Processing_TracksUseCaseType()
        {
            // Arrange
            var query = new GetAppVersionQuery();
            var sut = new LogPreProcessor<GetAppVersionQuery>(_loggingServiceMock.Object);

            // Act
            await sut.Process(query, CancellationToken.None);

            // Assert
            var expectedMessage = query.GetType().Name;
            _loggingServiceMock.Verify(f => f.TrackEvent(expectedMessage), Times.Once);
        }
    }
}