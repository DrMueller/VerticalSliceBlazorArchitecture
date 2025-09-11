using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.Identity.Web;
using Moq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text.Json;
using VerticalSliceBlazorArchitecture.Application.Context.Models;
using VerticalSliceBlazorArchitecture.Presentation.Infrastructure.Context.Services.Implementation;
using VerticalSliceBlazorArchitecture.Presentation.Infrastructure.Storage;
using Xunit;

namespace VerticalSliceBlazorArchitecture.UnitTests.Presentation.Infrastructure.Context.Services
{
    public class BenutzerContextStateUnitTests
    {
        private readonly Mock<HttpContext> _httpContextMock;
        private readonly Mock<IProtectedLocalStorageProxy> _storeMock;
        private readonly BenutzerContextState _sut;

        public BenutzerContextStateUnitTests()
        {
            _httpContextMock = new Mock<HttpContext>();
            Mock<IHttpContextAccessor> httpContextAccessorMock = new();
            httpContextAccessorMock
                .Setup(f => f.HttpContext)
                .Returns(_httpContextMock.Object);

            _storeMock = new Mock<IProtectedLocalStorageProxy>();
            _sut = new BenutzerContextState(httpContextAccessorMock.Object, _storeMock.Object);
        }

        [Fact]
        public async Task CheckingBenutzerExists_DeserializationFailing_ReturnsFalse()
        {
            // Arrange
            var principal = CreateMockPrincipal(Guid.NewGuid().ToString());

            _httpContextMock
                .Setup(f => f.User)
                .Returns(principal);

            _storeMock
                .Setup(f => f.GetAsync<BenutzerContext>(BenutzerContextState.Key))
                .Throws<JsonException>();

            // Act
            var actualResult = await _sut.CheckBenutzerExistsAsync();

            // Assert
            actualResult.Should().BeFalse();
        }

        [Fact]
        public async Task CheckingBenutzerExists_ExistingAndIdpGuidMatching_ReturnsTrue()
        {
            // Arrange
            var idpGuid = Guid.NewGuid().ToString();

            var benutzerContext = new BenutzerContext
            {
                IdpId = idpGuid
            };

            _storeMock
                .Setup(f => f.GetAsync<BenutzerContext>(BenutzerContextState.Key))
                .ReturnsAsync(benutzerContext);

            var principal = CreateMockPrincipal(idpGuid);

            _httpContextMock
                .Setup(f => f.User)
                .Returns(principal);

            // Act
            var actualResult = await _sut.CheckBenutzerExistsAsync();

            // Arrange
            actualResult.Should().BeTrue();
        }

        [Fact]
        public async Task CheckingBenutzerExists_GuidsNotMatching_ReturnsFalse()
        {
            // Arrange
            var benutzerContext = new BenutzerContext
            {
                IdpId = Guid.NewGuid().ToString()
            };

            _storeMock
                .Setup(f => f.GetAsync<BenutzerContext>(BenutzerContextState.Key))
                .ReturnsAsync(benutzerContext);

            var principal = CreateMockPrincipal(Guid.NewGuid().ToString());

            _httpContextMock
                .Setup(f => f.User)
                .Returns(principal);

            // Act
            var actualResult = await _sut.CheckBenutzerExistsAsync();

            // Assert
            actualResult.Should().BeFalse();
        }

        private static ClaimsPrincipal CreateMockPrincipal(string idpGuid)
        {
            var identity = new GenericIdentity("Tra", "");
            identity.AddClaim(new Claim(ClaimConstants.ObjectId, idpGuid));

            var identityMock = new Mock<IIdentity>();
            identityMock.Setup(f => f.IsAuthenticated).Returns(true);

            return new ClaimsPrincipal(identity);
        }
    }
}