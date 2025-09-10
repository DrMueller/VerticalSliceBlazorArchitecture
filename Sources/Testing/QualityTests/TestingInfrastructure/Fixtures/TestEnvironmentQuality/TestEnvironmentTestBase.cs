using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace VerticalSliceBlazorArchitecture.QualityTests.TestingInfrastructure.Fixtures.TestEnvironmentQuality
{
    [Collection(TestEnvironmentCollectionFixture.CollectionName)]
    public abstract class TestEnvironmentTestBase(TestEnvironmentFixture fixture)
    {
        protected WebApplicationFactory<Program> AppFactory => fixture.AppFactory;
    }
}