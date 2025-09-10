using Microsoft.AspNetCore.Mvc.Testing;
using VerticalSliceBlazorArchitecture.DataAccess.DbContexts.Factories;
using VerticalSliceBlazorArchitecture.Testing.Common.Data;
using Xunit;

namespace VerticalSliceBlazorArchitecture.QualityTests.TestingInfrastructure.Fixtures.Quality
{
    [Collection(QualityTestsCollectionFixture.CollectionName)]
    public abstract class QualityTestBase : IAsyncLifetime
    {
        private readonly QualityTestFixture _fixture;

        protected WebApplicationFactory<Program> AppFactory => _fixture.AppFactory;

        protected QualityTestBase(QualityTestFixture fixture)
        {
            _fixture = fixture;
            var dbContextFactory = (TestAppDbContextFactory)fixture.AppFactory.Services.GetRequiredService<IAppDbContextFactory>();
            dbContextFactory.InitializeName();
        }

        public Task DisposeAsync()
        {
            return Task.CompletedTask;
        }

        public Task InitializeAsync()
        {
            return Task.CompletedTask;
        }
    }
}