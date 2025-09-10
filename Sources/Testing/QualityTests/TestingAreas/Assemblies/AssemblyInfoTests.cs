using FluentAssertions;
using VerticalSliceBlazorArchitecture.QualityTests.TestingInfrastructure.SolutionMetadata.Services;
using Xunit;

namespace VerticalSliceBlazorArchitecture.QualityTests.TestingAreas.Assemblies
{
    /// <summary>
    ///     These tests are needed, as GitTools GitVersion has a problem with assemblyInfos
    ///     We therefore need to manually add them and not generate it afterwards
    /// </summary>
    public class AssemblyInfoTests
    {
        [Fact]
        public void AssembliesWithAssemblyInfo_DoNotGenerateAssemblyInfo()
        {
            var vsSolution = VsSolutionFactory.Create();

            foreach (var proj in vsSolution.Projects)
            {
                var assemblyInfoPath = $"{proj.AssemblyPath}/AssemblyInfo.cs";
                var assemblyInfoExists = File.Exists(assemblyInfoPath);

                if (assemblyInfoExists)
                {
                    proj.PropertyGroup.GenerateAssemblyInfo.Should().BeFalse(proj.AssemblyName);
                }
            }
        }

        [Fact]
        public void AssembliesWithoutAssemblyInfo_DoGenerateAssemblyInfo()
        {
            var vsSolution = VsSolutionFactory.Create();

            foreach (var proj in vsSolution.Projects)
            {
                var assemblyInfoPath = $"{proj.AssemblyPath}/AssemblyInfo.cs";
                var assemblyInfoExists = File.Exists(assemblyInfoPath);

                if (!assemblyInfoExists)
                {
                    proj.PropertyGroup.GenerateAssemblyInfo.Should().BeTrue(proj.AssemblyName);
                }
            }
        }
    }
}