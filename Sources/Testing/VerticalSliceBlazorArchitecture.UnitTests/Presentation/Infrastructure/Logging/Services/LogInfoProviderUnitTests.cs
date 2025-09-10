using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Moq;
using VerticalSliceBlazorArchitecture.Common.Logging.Services.Models;
using VerticalSliceBlazorArchitecture.Presentation.Infrastructure.Logging.Services;
using Xunit;

namespace VerticalSliceBlazorArchitecture.UnitTests.Presentation.Infrastructure.Logging.Services
{
    public class LogInfoProviderUnitTests
    {
        private readonly Mock<IHttpContextAccessor> _httpContextAccessorMock;
        private readonly LogInfoProvider _sut;

        public LogInfoProviderUnitTests()
        {
            _httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            _sut = new LogInfoProvider(_httpContextAccessorMock.Object);
        }

        [Fact]
        public void ProvidingLogInfo_NameClaimNotSet_ReturnsAnonymous()
        {
            // Arrange
            var httpContext = new DefaultHttpContext();

            _httpContextAccessorMock
                .Setup(f => f.HttpContext)
                .Returns(httpContext);

            var principal = new ClaimsPrincipal(new List<ClaimsIdentity>
            {
                new(new List<Claim>
                {
                    new(ClaimTypes.Email, "Name")
                }, "Tra")
            });

            httpContext.User = principal;

            // Act
            var actualLogInfo = _sut.ProvideLogInfo();

            // Assert
            actualLogInfo.BenutzerEmail.Should().Be(LogInfo.AnonymousEmail);
        }

        [Fact]
        public void ProvidingLogInfo_HttpContextNull_ReturnsAnonymous()
        {
            // Arrange
            _httpContextAccessorMock
                .Setup(f => f.HttpContext)
                .Returns((HttpContext?)null);

            // Act
            var actualLogInfo = _sut.ProvideLogInfo();

            // Assert
            actualLogInfo.BenutzerEmail.Should().Be(LogInfo.AnonymousEmail);
        }

        [Fact]
        public void ProvidingLogInfo_UserLoggedIn_ReturnsLogInfo()
        {
            // Arrange
            const string Email = "EMail@gmx.ch";

            var httpContext = new DefaultHttpContext();

            _httpContextAccessorMock
                .Setup(f => f.HttpContext)
                .Returns(httpContext);

            var principal = new ClaimsPrincipal(new List<ClaimsIdentity>
            {
                new(new List<Claim>
                {
                    new(JwtRegisteredClaimNames.Name, Email)
                }, "Tra")
            });

            httpContext.User = principal;

            // Act
            var actualLogInfo = _sut.ProvideLogInfo();

            // Assert
            actualLogInfo.BenutzerEmail.Should().Be(Email);
        }

        [Fact]
        public void ProvidingLogInfo_UserNotLoggedIn_ReturnsAnonymous()
        {
            // Arrange
            var httpContext = new DefaultHttpContext();

            _httpContextAccessorMock
                .Setup(f => f.HttpContext)
                .Returns(httpContext);

            httpContext.User = new ClaimsPrincipal();

            // Act
            var actualLogInfo = _sut.ProvideLogInfo();

            // Assert
            actualLogInfo.BenutzerEmail.Should().Be(LogInfo.AnonymousEmail);
        }
    }
}