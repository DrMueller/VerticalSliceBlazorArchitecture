using Xunit;

namespace VerticalSliceBlazorArchitecture.QualityTests.TestingInfrastructure.Fixtures.Quality
{
    [CollectionDefinition(CollectionName)]
    public class QualityTestsCollectionFixture : ICollectionFixture<QualityTestFixture>
    {
        public const string CollectionName = "QualityTests";
    }
}