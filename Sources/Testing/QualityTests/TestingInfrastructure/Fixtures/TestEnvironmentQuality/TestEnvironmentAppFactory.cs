using Microsoft.AspNetCore.Mvc.Testing;
using VerticalSliceBlazorArchitecture.Testing.Common.WebApp;

namespace VerticalSliceBlazorArchitecture.QualityTests.TestingInfrastructure.Fixtures.TestEnvironmentQuality
{
    public sealed class TestEnvironmentAppFactory : WebApplicationFactory<Program>
    {
        private const string EnvironmentName = "Test";

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", EnvironmentName);
            builder.UseEnvironment(EnvironmentName);
            builder.AddTestAuthentication();
            base.ConfigureWebHost(builder);
        }
    }
}