using FluentAssertions;
using JetBrains.Annotations;
using Lamar;
using Lamar.Diagnostics;
using VerticalSliceBlazorArchitecture.DataAccess.DbContexts.Factories;
using VerticalSliceBlazorArchitecture.Testing.Common.Data;

namespace VerticalSliceBlazorArchitecture.QualityTests.TestingAreas.CrossCutting.DependencyInjection
{
    [UsedImplicitly]
    public sealed class DependencyInjectionTestsFixture : IDisposable, IAsyncDisposable
    {
        internal DependencyInjectionTestsAppFactory AppFactory { get; }

        internal Lazy<IReadOnlyCollection<InstanceRef>> Registrations { get; }

        public DependencyInjectionTestsFixture()
        {
            Registrations = new Lazy<IReadOnlyCollection<InstanceRef>>(LoadRegistrations);
            AppFactory = new DependencyInjectionTestsAppFactory();

            var dbContextFactory = (TestAppDbContextFactory)AppFactory.Services.GetRequiredService<IAppDbContextFactory>();
            dbContextFactory.InitializeName();
        }

        public void Dispose()
        {
            AppFactory.Dispose();
        }

        public async ValueTask DisposeAsync()
        {
            await AppFactory.DisposeAsync();
        }

        private IReadOnlyCollection<InstanceRef> LoadRegistrations()
        {
            var serviceContainer = AppFactory.Services;
            serviceContainer.Should().BeOfType<Container>();
            var container = (IContainer)serviceContainer;

            return container.Model.AllInstances.ToList();
        }
    }
}