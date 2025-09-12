using FluentValidation;
using JetBrains.Annotations;
using Lamar;
using MediatR;
using VerticalSliceBlazorArchitecture.Application.Mediation.Services;
using VerticalSliceBlazorArchitecture.Application.Mediation.Services.Behaviors;
using VerticalSliceBlazorArchitecture.Application.Mediation.Services.Handlers;
using VerticalSliceBlazorArchitecture.Application.Mediation.Services.Implementation;
using VerticalSliceBlazorArchitecture.Common.LanguageExtensions.Types.Eithers;

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

            this.AddValidatorsFromAssembly(typeof(ApplicationServiceRegistryCollection).Assembly);
            AddValidationBehavior();
        }

        private void AddValidationBehavior()
        {
            var thisAssembly = typeof(ApplicationServiceRegistryCollection).Assembly;

            var eitherOpen = typeof(Either<,>);
            var behaviorOpen = typeof(ValidationBehavior<,>);
            var pipelineOpen = typeof(IPipelineBehavior<,>);
            var irequestOpen = typeof(IRequest<>);

            // Avoid duplicate registrations
            var registered = new HashSet<(Type request, Type response)>();

            foreach (var type in thisAssembly.DefinedTypes)
            {
                if (type.IsAbstract || type.IsInterface)
                {
                    continue;
                }

                // Find IRequest<TResponse> implemented by this type
                var irequest = type.GetInterfaces()
                    .FirstOrDefault(i => i.IsGenericType && i.GetGenericTypeDefinition() == irequestOpen);

                if (irequest is null)
                {
                    continue;
                }

                var responseType = irequest.GetGenericArguments()[0];

                // Match Either<TLeft, TValue>
                if (!responseType.IsGenericType || responseType.GetGenericTypeDefinition() != eitherOpen)
                {
                    continue;
                }

                var right = responseType.GetGenericArguments()[1];

                var requestType = type.AsType();

                // Build closed generic types:
                var closedPipeline = pipelineOpen.MakeGenericType(requestType, responseType);
                var closedBehavior = behaviorOpen.MakeGenericType(requestType, right);

                // Ensure we only add once
                if (!registered.Add((requestType, responseType)))
                {
                    continue;
                }

                For(closedPipeline).Use(closedBehavior).Scoped();
            }
        }
    }
}