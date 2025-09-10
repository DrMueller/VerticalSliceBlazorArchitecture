using Microsoft.AspNetCore.Mvc.Testing;
using VerticalSliceBlazorArchitecture.Testing.Common.WebApp;

namespace VerticalSliceBlazorArchitecture.QualityTests.TestingInfrastructure.Fixtures.Quality
{
    public class QualityTestAppFactory : WebApplicationFactory<Program>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.AddTestAuthentication();
        }
    }
}