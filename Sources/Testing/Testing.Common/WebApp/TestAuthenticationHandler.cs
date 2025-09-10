using System.Security.Claims;
using System.Text.Encodings.Web;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace VerticalSliceBlazorArchitecture.Testing.Common.WebApp
{
    [PublicAPI]
    public class TestAuthenticationHandler(
        IOptionsMonitor<TestAuthenticationOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder)
        : AuthenticationHandler<TestAuthenticationOptions>(options, logger, encoder)
    {
        public const string TestSchemeName = "Test Scheme";

        public static Guid? IdpGuid { get; set; }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (IdpGuid == null)
            {
                var authResult = AuthenticateResult.Fail("No IDP Guid");

                return Task.FromResult(authResult);
            }

            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, IdpGuid.ToString() ?? string.Empty)
            };

            var identity = new ClaimsIdentity(claims, "TestAuth");
            var principal = new ClaimsPrincipal(identity);

            var authenticationTicket = new AuthenticationTicket(
                principal,
                new AuthenticationProperties(),
                "Test Scheme");

            return Task.FromResult(AuthenticateResult.Success(authenticationTicket));
        }
    }
}