using JetBrains.Annotations;

namespace VerticalSliceBlazorArchitecture.QualityTests.TestingInfrastructure.Fixtures.Quality
{
    [UsedImplicitly]
    public sealed class QualityTestFixture : IDisposable, IAsyncDisposable
    {
        internal QualityTestAppFactory AppFactory { get; } = new();

        public void Dispose()
        {
            AppFactory.Dispose();
        }

        public async ValueTask DisposeAsync()
        {
            await AppFactory.DisposeAsync();
        }
    }
}