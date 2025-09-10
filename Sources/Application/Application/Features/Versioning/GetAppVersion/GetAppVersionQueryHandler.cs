using JetBrains.Annotations;
using MediatR;
using VerticalSliceBlazorArchitecture.Common;
using VerticalSliceBlazorArchitecture.Common.Settings.Provisioning.Services;

namespace VerticalSliceBlazorArchitecture.Application.Features.Versioning.GetAppVersion
{
    [UsedImplicitly]
    public class GetAppVersionQueryHandler(ISettingsProvider settingsProvider) : IRequestHandler<GetAppVersionQuery, string>
    {
        private const string LocalVersion = "1.0.0 (local)";

        public Task<string> Handle(GetAppVersionQuery request, CancellationToken cancellationToken)
        {
            var appSettingVersion = settingsProvider.AppSettings.AppVersion;

            var version = appSettingVersion == Constants.SemVerVariable ? LocalVersion : appSettingVersion;

            return Task.FromResult(version);
        }
    }
}