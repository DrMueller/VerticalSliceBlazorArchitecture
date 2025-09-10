using JetBrains.Annotations;
using Lamar;
using Microsoft.ApplicationInsights.Extensibility;
using VerticalSliceBlazorArchitecture.Common.Logging.Services;
using VerticalSliceBlazorArchitecture.Common.Logging.Services.Implementation;
using VerticalSliceBlazorArchitecture.Common.Logging.Services.Servants.Implementation;
using VerticalSliceBlazorArchitecture.Common.Settings.Provisioning.Services;
using VerticalSliceBlazorArchitecture.Common.Settings.Provisioning.Services.Implementation;

namespace VerticalSliceBlazorArchitecture.Common
{
    [UsedImplicitly]
    public class CommonServiceRegistryCollection : ServiceRegistry
    {
        public CommonServiceRegistryCollection()
        {
            For<ISettingsProvider>().Use<SettingsProvider>().Singleton();

            // Logging
            For<ILoggingService>().Use<LoggingService>().Scoped();
            For<ITelemetryInitializer>().Use<TelemetryInitializer>().Singleton();
            For<ITelemetryInitializer>().Use<AuthenticatedUserIdTelemetryInitializer>().Scoped();
        }
    }
}