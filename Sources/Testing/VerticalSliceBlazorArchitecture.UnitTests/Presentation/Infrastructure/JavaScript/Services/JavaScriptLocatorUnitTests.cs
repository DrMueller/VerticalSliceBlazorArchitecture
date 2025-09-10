using FluentAssertions;
using Moq;
using VerticalSliceBlazorArchitecture.Presentation.Areas.Home;
using VerticalSliceBlazorArchitecture.Presentation.Infrastructure.Caching.Controllers;
using VerticalSliceBlazorArchitecture.Presentation.Infrastructure.JavaScript.Services.Implementation;
using VerticalSliceBlazorArchitecture.Presentation.Infrastructure.Testing.Components;
using Xunit;

namespace VerticalSliceBlazorArchitecture.UnitTests.Presentation.Infrastructure.JavaScript.Services
{
    public class JavaScriptLocatorUnitTests
    {
        private readonly Mock<ICachingController> _cachingControllerMock;
        private readonly JavaScriptLocator _sut;

        public JavaScriptLocatorUnitTests()
        {
            _cachingControllerMock = new Mock<ICachingController>();
            _sut = new JavaScriptLocator(_cachingControllerMock.Object);
        }

        [Fact]
        public async Task Locating_AppendsCacheVersionSuffix()
        {
            // Arrange
            const string CacheSuffix = "?V=123";

            _cachingControllerMock
                .Setup(f => f.LoadCachingSuffixAsync())
                .ReturnsAsync(CacheSuffix);

            // Act
            var actualPath = await _sut.LocateJsFilePathAsync(new TestExceptionPage());

            // Assert
            actualPath.Should().EndWith(CacheSuffix);
        }

        [Fact]
        public async Task LocatingAbsolutePath_AppendsCacheVersionSuffix()
        {
            // Arrange
            const string CacheSuffix = "?V=123";
            const string Path = "Test234";

            _cachingControllerMock
                .Setup(f => f.LoadCachingSuffixAsync())
                .ReturnsAsync(CacheSuffix);

            // Act
            var actualPath = await _sut.LocateAbsoluteJsFilePathAsync(Path);

            // Assert
            actualPath.Should().EndWith($"{Path}{CacheSuffix}");
        }
    }
}