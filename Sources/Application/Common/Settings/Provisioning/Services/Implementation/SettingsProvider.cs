using JetBrains.Annotations;
using Microsoft.Extensions.Options;
using VerticalSliceBlazorArchitecture.Common.Settings.Provisioning.Models;

namespace VerticalSliceBlazorArchitecture.Common.Settings.Provisioning.Services.Implementation
{
    [UsedImplicitly]
    public class SettingsProvider(
        IOptions<AppSettings> appSettings
    )
        : ISettingsProvider
    {
        public AppSettings AppSettings => appSettings.Value;
    }
}