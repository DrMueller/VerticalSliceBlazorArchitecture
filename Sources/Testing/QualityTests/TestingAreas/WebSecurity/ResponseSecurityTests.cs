using System.Net.Http.Headers;
using FluentAssertions;
using VerticalSliceBlazorArchitecture.Presentation.Infrastructure.ResponseHeader.Models;
using VerticalSliceBlazorArchitecture.QualityTests.TestingInfrastructure.Fixtures.TestEnvironmentQuality;
using Xunit;

namespace VerticalSliceBlazorArchitecture.QualityTests.TestingAreas.WebSecurity
{
    public class ResponseSecurityTests(TestEnvironmentFixture fixture) : TestEnvironmentTestBase(fixture)
    {
        [Fact]
        public async Task CallingApi_WithTestEnvironment_ReturnsSecurityHeaders()
        {
            // Arrange
            using var client = AppFactory.CreateClient();

            // Act
            using var actualResponse = await client.GetAsync("api/Test/test");

            // Assert
            AssertHeader(actualResponse.Headers, SecurityResponseHeader.ContentSecurityPolicy);
            AssertHeader(actualResponse.Headers, SecurityResponseHeader.ContentTypeOptions);
        }

        private static void AssertHeader(HttpHeaders actualHeaders, SecurityResponseHeader expectedHeader)
        {
            var actualValues = actualHeaders.GetValues(expectedHeader.Name).ToList();
            actualValues.Should().ContainSingle();
            actualValues.Single().Should().Be(expectedHeader.Value);
        }
    }
}