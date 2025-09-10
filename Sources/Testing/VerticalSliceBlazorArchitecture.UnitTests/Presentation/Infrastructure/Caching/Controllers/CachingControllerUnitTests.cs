using FluentAssertions;
using Moq;
using VerticalSliceBlazorArchitecture.Application.Features.Versioning.GetAssetsCacheVersion;
using VerticalSliceBlazorArchitecture.Application.Mediation.Services;
using VerticalSliceBlazorArchitecture.Presentation.Infrastructure.Caching.Controllers.Implementation;
using Xunit;

namespace VerticalSliceBlazorArchitecture.UnitTests.Presentation.Infrastructure.Caching.Controllers
{
    public class CachingControllerUnitTests
    {
        private readonly Mock<IMediationService> _mediatorMock;
        private readonly CachingController _sut;

        public CachingControllerUnitTests()
        {
            _mediatorMock = new Mock<IMediationService>();
            _sut = new CachingController(_mediatorMock.Object);
        }

        [Fact]
        public async Task LoadingCachingSuffix_LoadsCachingSuffix()
        {
            // Arrange
            const string Version = "123.45";

            _mediatorMock
                .Setup(f => f.SendAsync(It.IsAny<GetAssetsCacheVersionQuery>()))
                .ReturnsAsync(Version);

            // Act
            var actualSuffix = await _sut.LoadCachingSuffixAsync();

            // Assert
            var expectedSuffix = string.Format(CachingController.SuffixTemplate, Version);
            actualSuffix.Should().Be(expectedSuffix);
        }
    }
}