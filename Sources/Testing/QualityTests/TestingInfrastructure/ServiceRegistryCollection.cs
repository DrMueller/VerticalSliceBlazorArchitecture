using JetBrains.Annotations;
using Lamar;
using Microsoft.AspNetCore.Components;
using VerticalSliceBlazorArchitecture.Common.Logging.Services;
using VerticalSliceBlazorArchitecture.Testing.Common.Mocks;

namespace VerticalSliceBlazorArchitecture.QualityTests.TestingInfrastructure
{
    [UsedImplicitly]
    public class ServiceRegistryCollection : ServiceRegistry
    {
        public ServiceRegistryCollection()
        {
            For<ILoggingService>().Use<LoggingServiceMock>().Singleton();
            For<NavigationManager>().Use<NavigationManagerMock>().Scoped();
        }
    }
}