using JetBrains.Annotations;
using Lamar;
using VerticalSliceBlazorArchitecture.Application.Context.Services;
using VerticalSliceBlazorArchitecture.Common.Logging.Services;
using VerticalSliceBlazorArchitecture.Presentation.Infrastructure.Caching.Controllers;
using VerticalSliceBlazorArchitecture.Presentation.Infrastructure.Caching.Controllers.Implementation;
using VerticalSliceBlazorArchitecture.Presentation.Infrastructure.Context.Services.Implementation;
using VerticalSliceBlazorArchitecture.Presentation.Infrastructure.JavaScript.Services;
using VerticalSliceBlazorArchitecture.Presentation.Infrastructure.JavaScript.Services.Implementation;
using VerticalSliceBlazorArchitecture.Presentation.Infrastructure.Logging.Services;
using VerticalSliceBlazorArchitecture.Presentation.Infrastructure.Navigation.Services;
using VerticalSliceBlazorArchitecture.Presentation.Infrastructure.Navigation.Services.Implementation;
using VerticalSliceBlazorArchitecture.Presentation.Infrastructure.Storage;
using VerticalSliceBlazorArchitecture.Presentation.Infrastructure.Storage.Implementation;

namespace VerticalSliceBlazorArchitecture.Presentation
{
    [UsedImplicitly]
    public class PresentationServiceRegistryCollection : ServiceRegistry
    {
        public PresentationServiceRegistryCollection()
        {
            RegisterInfrastructure();
        }

        private void RegisterInfrastructure()
        {
            For<IBenutzerContextState>().Use<BenutzerContextState>().Scoped();
            For<INavigator>().Use<Navigator>().Scoped();
            For<ILogInfoProvider>().Use<LogInfoProvider>().Scoped();
            For<IJavaScriptLocator>().Use<JavaScriptLocator>().Transient();
            For<ICachingController>().Use<CachingController>().Transient();
            For<IProtectedLocalStorageProxy>().Use<ProtectedLocalStorageProxy>().Scoped();
            For<ILocalStorageProxy>().Use<LocalStorageProxy>().Transient();
        }
    }
}