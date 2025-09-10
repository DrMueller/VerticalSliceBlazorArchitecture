using Xunit;

namespace VerticalSliceBlazorArchitecture.QualityTests.TestingInfrastructure.Fixtures.TestEnvironmentQuality
{
    [CollectionDefinition(CollectionName)]
    public class TestEnvironmentCollectionFixture : ICollectionFixture<TestEnvironmentFixture>
    {
        public const string CollectionName = "QualityTestsInTestEnvironment";
    }
}