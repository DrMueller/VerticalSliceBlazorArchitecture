using FluentAssertions;
using VerticalSliceBlazorArchitecture.QualityTests.TestingInfrastructure.Fixtures.Assemblies;
using Xunit;

namespace VerticalSliceBlazorArchitecture.QualityTests.TestingAreas.Consistency
{
    public class ModelNamingConsistencyTests
    {
        [Fact]
        public void ModelNamespaces_DoNotExist()
        {
            const string ModelPart = "Model";

            var failingTypes = AssemblyFetcher
                .FetchAllAssemblies()
                .SelectMany(f => f.GetTypes().Where(type => type.Namespace != null
                                                            && (type.Namespace.Contains($".{ModelPart}.") || type.Namespace.EndsWith(ModelPart))));

            failingTypes.Should().BeEmpty();
        }
    }
}