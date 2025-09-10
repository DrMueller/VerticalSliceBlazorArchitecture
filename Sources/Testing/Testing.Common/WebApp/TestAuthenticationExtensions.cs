using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace VerticalSliceBlazorArchitecture.Testing.Common.WebApp
{
    public static class TestAuthenticationExtensions
    {
        public static void AddTestAuthentication(
            this IWebHostBuilder builder)
        {
            builder.ConfigureServices(s =>
            {
                s.AddAuthentication(options =>
                    {
                        options.DefaultAuthenticateScheme = TestAuthenticationHandler.TestSchemeName;
                        options.DefaultChallengeScheme = TestAuthenticationHandler.TestSchemeName;
                        options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                        options.DefaultSignOutScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    })
                    .AddScheme<TestAuthenticationOptions, TestAuthenticationHandler>(TestAuthenticationHandler.TestSchemeName, "Test Auth", null);
            });
        }
    }
}