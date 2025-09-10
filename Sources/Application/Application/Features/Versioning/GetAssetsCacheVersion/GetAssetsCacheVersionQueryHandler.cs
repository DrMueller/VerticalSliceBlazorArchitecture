using MediatR;
using VerticalSliceBlazorArchitecture.Common;
using VerticalSliceBlazorArchitecture.Common.Settings.Provisioning.Services;

namespace VerticalSliceBlazorArchitecture.Application.Features.Versioning.GetAssetsCacheVersion
{
    public class GetAssetsCacheVersionQueryHandler(
        ISettingsProvider settingsProvider)
        : IRequestHandler<GetAssetsCacheVersionQuery, string>
    {
        public static Lazy<string> LocalTicks { get; } = new(DateTime.Now.Ticks.ToString());

        public Task<string> Handle(GetAssetsCacheVersionQuery request, CancellationToken cancellationToken)
        {
            var appSettingVersion = settingsProvider.AppSettings.AppVersion;

            var version = appSettingVersion == Constants.SemVerVariable ? LocalTicks.Value : appSettingVersion;

            return Task.FromResult(version);
        }
    }
}