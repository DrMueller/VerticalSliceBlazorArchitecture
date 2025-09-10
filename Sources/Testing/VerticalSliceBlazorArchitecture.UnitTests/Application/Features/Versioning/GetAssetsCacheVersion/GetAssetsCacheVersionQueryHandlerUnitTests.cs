using FluentAssertions;
using Moq;
using VerticalSliceBlazorArchitecture.Application.Features.Versioning.GetAssetsCacheVersion;
using VerticalSliceBlazorArchitecture.Common;
using VerticalSliceBlazorArchitecture.Common.Settings.Provisioning.Models;
using VerticalSliceBlazorArchitecture.Common.Settings.Provisioning.Services;
using Xunit;

namespace VerticalSliceBlazorArchitecture.UnitTests.Application.Features.Versioning.GetAssetsCacheVersion
{
    public class GetAssetsCacheVersionQueryHandlerUnitTests
    {
        private readonly Mock<ISettingsProvider> _settingsProviderMock;
        private readonly GetAssetsCacheVersionQueryHandler _sut;

        public GetAssetsCacheVersionQueryHandlerUnitTests()
        {
            _settingsProviderMock = new Mock<ISettingsProvider>();
            _sut = new GetAssetsCacheVersionQueryHandler(_settingsProviderMock.Object);
        }

        [Fact]
        public async Task GettingCacheVersion_AppSettingContainingSemverVariable_ReturnsDateTimeTicks()
        {
            // Arrange
            _settingsProviderMock
                .Setup(f => f.AppSettings)
                .Returns(new AppSettings
                {
                    AppVersion = Constants.SemVerVariable
                });

            // Act
            var actualVersion = await _sut.Handle(new GetAssetsCacheVersionQuery(), CancellationToken.None);

            // Assert
            actualVersion.Should().Be(GetAssetsCacheVersionQueryHandler.LocalTicks.Value);
        }

        [Fact]
        public async Task GettingCacheVersion_AppSettingContainingVersion_ReturnsVersion()
        {
            // Arrange
            const string Version = "11.22.33";

            _settingsProviderMock
                .Setup(f => f.AppSettings)
                .Returns(new AppSettings
                {
                    AppVersion = Version
                });

            // Act
            var actualVersion = await _sut.Handle(new GetAssetsCacheVersionQuery(), CancellationToken.None);

            // Assert
            actualVersion.Should().Be(Version);
        }
    }
}