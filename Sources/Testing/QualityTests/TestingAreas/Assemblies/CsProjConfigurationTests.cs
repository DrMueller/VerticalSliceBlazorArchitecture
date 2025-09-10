using FluentAssertions;
using VerticalSliceBlazorArchitecture.QualityTests.TestingInfrastructure.SolutionMetadata.Services;
using Xunit;

namespace VerticalSliceBlazorArchitecture.QualityTests.TestingAreas.Assemblies
{
    public class CsProjConfigurationTests
    {
        [Fact]
        public void NoRemoveContextActions_Exist()
        {
            var vsSolution = VsSolutionFactory.Create();

            var failingProjects = vsSolution
                .Projects.Where(f => f.FileItemGroups.Any(g => g.Entries.Any(c => c.BuildContext.ToLower() == "remove")))
                .Select(f => f.AssemblyName)
                .ToList();

            failingProjects.Should().BeEmpty();
        }

        [Fact]
        public void Nullable_IsActivated()
        {
            // Arrange
            var vsSolution = VsSolutionFactory.Create();

            // Act
            var actualProjectsWithoutNullableEnabled = vsSolution
                .Projects
                .Where(f => f.PropertyGroup.NullableEnabled == false)
                .ToList();

            // Assert
            actualProjectsWithoutNullableEnabled.Should().BeEmpty();
        }

        [Fact]
        public void UnsafeCode_IsNotAllowed()
        {
            var vsSolution = VsSolutionFactory.Create();

            var failingProjects = vsSolution
                .Projects.Where(f => f.PropertyGroup.AllowUnsafeBlocks)
                .Select(f => f.AssemblyName)
                .ToList();

            failingProjects.Should().BeEmpty();
        }

        [Fact]
        public void VersionNet9_Is_Used()
        {
            // Arrange
            var vsSolution = VsSolutionFactory.Create();

            // Act
            var not8Assemblies = vsSolution.Projects.Where(f => f.TargetFramework != "net9.0");

            // Assert
            not8Assemblies.Should().BeEmpty();
        }
    }
}