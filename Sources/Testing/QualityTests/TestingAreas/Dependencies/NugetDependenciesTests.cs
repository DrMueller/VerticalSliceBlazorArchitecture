using FluentAssertions;
using VerticalSliceBlazorArchitecture.QualityTests.TestingInfrastructure.SolutionMetadata.Services;
using Xunit;

namespace VerticalSliceBlazorArchitecture.QualityTests.TestingAreas.Dependencies
{
    public class NugetDependenciesTests
    {
        // FluentAssertion is chargeable after V.8.0.0, see https://github.com/fluentassertions/fluentassertions/pull/2943
        // Therefore, do not upgrade
        [Fact]
        public void FluentAssertionVersion_IsFixed()
        {
            // Arrange
            const string ExpectedVersion = "[7.2.0]";
            var vsSolution = VsSolutionFactory.Create();

            // Act
            var actualFluentAssertionNugetVersions = vsSolution
                .FindNugets("FluentAssertions")
                .Select(f => f.PackageVersion)
                .Distinct()
                .ToList();

            // Assert
            actualFluentAssertionNugetVersions.Should().HaveCount(1);
            actualFluentAssertionNugetVersions.Single().Should().Be(ExpectedVersion);
        }

        [Fact]
        public void NugetDependencies_AreUnique()
        {
            // Arrange
            var vsSolution = VsSolutionFactory.Create();

            // Act
            var actualDuplicates = vsSolution.CalculateDuplicatedNugets();

            // Assert
            actualDuplicates.Should().BeEmpty();
        }
    }
}