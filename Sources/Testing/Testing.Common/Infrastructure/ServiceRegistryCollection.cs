using JetBrains.Annotations;
using Lamar;
using VerticalSliceBlazorArchitecture.Common.Settings.Provisioning.Services;
using VerticalSliceBlazorArchitecture.DataAccess.DbContexts.Factories;
using VerticalSliceBlazorArchitecture.Testing.Common.Data;
using VerticalSliceBlazorArchitecture.Testing.Common.Mocks;

namespace VerticalSliceBlazorArchitecture.Testing.Common.Infrastructure
{
    [UsedImplicitly]
    public class ServiceRegistryCollection : ServiceRegistry
    {
        public ServiceRegistryCollection()
        {
            For<IAppDbContextFactory>().Use<TestAppDbContextFactory>().Singleton();
            For<ISettingsProvider>().Use<SettingsProviderMock>().Singleton();
        }
    }
}