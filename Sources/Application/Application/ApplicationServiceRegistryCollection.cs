using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Lamar;
using VerticalSliceBlazorArchitecture.Application.Mediation.Services;
using VerticalSliceBlazorArchitecture.Application.Mediation.Services.Handlers;
using VerticalSliceBlazorArchitecture.Application.Mediation.Services.Implementation;

namespace VerticalSliceBlazorArchitecture.Application
{
    [UsedImplicitly]
    public class ApplicationServiceRegistryCollection : ServiceRegistry
    {
        public ApplicationServiceRegistryCollection()
        {
            For<IMediationService>().Use<MediationService>().Scoped();
            this.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblyContaining<ApplicationServiceRegistryCollection>();
                cfg.AddOpenRequestPreProcessor(typeof(LogPreProcessor<>));
                cfg.Lifetime = ServiceLifetime.Scoped;
            });
        }
    }
}