using JetBrains.Annotations;

namespace VerticalSliceBlazorArchitecture.QualityTests.TestingInfrastructure.Fixtures.TestEnvironmentQuality
{
    [UsedImplicitly]
    public sealed class TestEnvironmentFixture : IDisposable, IAsyncDisposable
    {
        internal TestEnvironmentAppFactory AppFactory { get; } = new();

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