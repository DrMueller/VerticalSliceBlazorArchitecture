using System.IdentityModel.Tokens.Jwt;
using JetBrains.Annotations;
using VerticalSliceBlazorArchitecture.Common.Logging.Services;
using VerticalSliceBlazorArchitecture.Common.Logging.Services.Models;

namespace VerticalSliceBlazorArchitecture.Presentation.Infrastructure.Logging.Services
{
    [UsedImplicitly]
    internal class LogInfoProvider(IHttpContextAccessor httpContextAccessor) : ILogInfoProvider
    {
        private bool IsIdentityAuthenticated
        {
            get
            {
                var identityExists = httpContextAccessor.HttpContext?.User.Identity;

                return identityExists is { IsAuthenticated: true };
            }
        }

        public LogInfo ProvideLogInfo()
        {
            if (!IsIdentityAuthenticated)
            {
                return LogInfo.CreateAnonymous();
            }

            var identityEmail = httpContextAccessor.HttpContext?.User.Claims.SingleOrDefault(f => f.Type == JwtRegisteredClaimNames.Name)?.Value;

            if (string.IsNullOrEmpty(identityEmail))
            {
                return LogInfo.CreateAnonymous();
            }

            var logInfo = new LogInfo(identityEmail);

            return logInfo;
        }
    }
}