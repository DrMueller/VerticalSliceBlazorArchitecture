using Xunit;

namespace VerticalSliceBlazorArchitecture.QualityTests.TestingAreas.CrossCutting.DependencyInjection
{
    [CollectionDefinition(CollectionName)]
    public class DependencyInjectionTestsCollectionFixture : ICollectionFixture<DependencyInjectionTestsFixture>
    {
        public const string CollectionName = "DependencyInjectionTests";
    }
}