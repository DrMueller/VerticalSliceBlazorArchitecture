using JetBrains.Annotations;
using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.Extensibility;
using VerticalSliceBlazorArchitecture.Common.Settings.Provisioning.Services;

namespace VerticalSliceBlazorArchitecture.Common.Logging.Services.Servants.Implementation
{
    [UsedImplicitly]
    internal class TelemetryInitializer(ISettingsProvider settingsProvider) : ITelemetryInitializer
    {
        public const string RoleInstanceName = "VerticalSliceBlazorArchitecture";

        public void Initialize(ITelemetry telemetry)
        {
            telemetry.Context.Cloud.RoleInstance = $"{RoleInstanceName}_{settingsProvider.AppSettings.EnvironmentName}";
        }
    }
}