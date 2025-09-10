using FluentAssertions;
using Lamar;
using Lamar.IoC;
using VerticalSliceBlazorArchitecture.DataAccess.Repositories.Base;
using VerticalSliceBlazorArchitecture.DataAccess.UnitOfWorks.Implementation;
using VerticalSliceBlazorArchitecture.Domain.Infrastructure.Data.Writing;
using VerticalSliceBlazorArchitecture.QualityTests.TestingInfrastructure.BlazorMetadata.Services;
using Xunit;

namespace VerticalSliceBlazorArchitecture.QualityTests.TestingAreas.CrossCutting.DependencyInjection
{
    public partial class DependencyInjectionTests
    {
        [Fact]
        public void BlazorComponentInjections_AreValid()
        {
            var serviceContainer = fixture.AppFactory.Services;
            var container = (IContainer)serviceContainer;

            var componentsWithInjections = BlazorComponentFactory
                .CreateAll()
                .Where(f => f.Injections.Any());

            var failingInjections = new List<string>();

            foreach (var comp in componentsWithInjections)
            {
                var injections = comp.Injections;

                foreach (var inj in injections)
                {
                    try
                    {
                        container.GetInstance(inj.PropertyType);
                    }
                    catch (LamarException)
                    {
                        failingInjections.Add($"{comp.Name} -> {inj.PropertyType.Name}");
                    }
                }
            }

            failingInjections.Should().BeEmpty();
        }

        //[Fact]
        //public void LamarConfiguration_IsValid()
        //{
        //    var serviceContainer = fixture.AppFactory.Services;
        //    serviceContainer.Should().BeOfType<Container>();
        //    var container = (IContainer)serviceContainer;
        //    var testService = container.GetInstance<ILoggingService>();

        //    testService.Should().NotBeNull();
        //    container.AssertConfigurationIsValid();
        //}

        [Fact]
        public void Repositories_AreTransient()
        {
            var types = new List<Type>
            {
                typeof(RepositoryBase)
            };

            var wrongRegistrations = fixture.Registrations.Value
                .Where(reg => types.Any(type => reg.ImplementationType.IsAssignableTo(type)))
                .Where(reg => reg.Lifetime != ServiceLifetime.Transient)
                .ToList();

            wrongRegistrations.Should().BeEmpty();
        }

        [Fact]
        public void ScopedTypes_DoNotUseTransientTypes()
        {
            AssertInjectionScoping(
                ServiceLifetime.Scoped,
                ServiceLifetime.Transient);
        }

        [Fact]
        public void SingletonTypes_UseOnlyOtherSingletonTypes()
        {
            AssertInjectionScoping(
                ServiceLifetime.Singleton,
                ServiceLifetime.Scoped,
                ServiceLifetime.Transient);
        }

        // DO NOT EVER CHANGE THIS
        [Fact]
        public void UnitOfWork_IsTransient()
        {
            var types = new List<Type>
            {
                typeof(IUnitOfWork),
                typeof(UnitOfWork)
            };

            var wrongRegistrations = fixture.Registrations.Value
                .Where(reg => types.Any(type => reg.ImplementationType.IsAssignableTo(type)))
                .Where(reg => reg.Lifetime != ServiceLifetime.Transient)
                .ToList();

            wrongRegistrations.Should().BeEmpty();
        }
    }
}