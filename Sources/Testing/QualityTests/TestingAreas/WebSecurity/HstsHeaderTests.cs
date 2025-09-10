using FluentAssertions;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Options;
using VerticalSliceBlazorArchitecture.QualityTests.TestingInfrastructure.Fixtures.TestEnvironmentQuality;
using Xunit;

namespace VerticalSliceBlazorArchitecture.QualityTests.TestingAreas.WebSecurity
{
    // Hsts ignores localhost, so we test the configuration, not the actual response header
    public class HstsHeaderTests(TestEnvironmentFixture fixture) : TestEnvironmentTestBase(fixture)
    {
        [Fact]
        public void HstsOptions_AreConfigured()
        {
            var actualHstsOptions = AppFactory.Services.GetRequiredService<IOptions<HstsOptions>>().Value;
            actualHstsOptions.IncludeSubDomains.Should().BeTrue();
            actualHstsOptions.MaxAge.Should().Be(TimeSpan.FromDays(730));
        }
    }
}