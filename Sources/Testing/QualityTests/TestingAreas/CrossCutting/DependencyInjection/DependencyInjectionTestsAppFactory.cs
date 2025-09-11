using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc.Testing;
using VerticalSliceBlazorArchitecture.Testing.Common.WebApp;

namespace VerticalSliceBlazorArchitecture.QualityTests.TestingAreas.CrossCutting.DependencyInjection
{
    public class DependencyInjectionTestsAppFactory : WebApplicationFactory<Program>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            // Hack, we only want our mock navigationmanager as the real ones are not initialized in the test host
            builder.ConfigureServices(service =>
            {
                var navServices = service.Where(d => d.ServiceType == typeof(NavigationManager)).ToList();
                navServices.ForEach(s => service.Remove(s));
            });

            builder.AddTestAuthentication();
        }
    }
}