using FluentAssertions;
using VerticalSliceBlazorArchitecture.QualityTests.TestingInfrastructure.Fixtures.Assemblies;
using VerticalSliceBlazorArchitecture.QualityTests.TestingInfrastructure.SolutionMetadata.Services;
using Xunit;

namespace VerticalSliceBlazorArchitecture.QualityTests.TestingAreas.CodeQuality
{
    public class AnalyzersTests
    {
        private const string DisposableAnalyzerName = "IDisposableAnalyzers";
        private const string ReferenceCopAnalyzer = "ReferenceCopAnalyzer";
        private const string ThreadingAnalyzerName = "Microsoft.VisualStudio.Threading.Analyzers";
        private const string XUnitAnalyzerName = "xunit.analyzers";

        private static readonly IReadOnlyCollection<string> _globalAnalyzers = new List<string>
        {
            DisposableAnalyzerName,
            ThreadingAnalyzerName
        };

        [Fact]
        public void GlobalRoslynAnalyzers_AreInAllProjects()
        {
            var projectsWithMissingPackages = VsSolutionFactory.Create()
                .Projects
                .Where(proj => !proj.ContainNugetsReferences(_globalAnalyzers))
                .Select(f => f.AssemblyName)
                .ToList();

            projectsWithMissingPackages.Should().BeEmpty();
        }

        [Fact]
        public void GlobalRoslynAnalyzers_HaveSameVersion()
        {
            var distinctVersions = VsSolutionFactory.Create()
                .Projects
                .SelectMany(f => f.NugetReferences)
                .Where(f => _globalAnalyzers.Contains(f.PackageName))
                .Distinct();

            distinctVersions.Count().Should().Be(_globalAnalyzers.Count);
        }

        [Fact]
        public void ReferenceCopAnalyzer_IsInApplicationProject()
        {
            var refCopAssemblies = VsSolutionFactory.Create()
                .Projects
                .Where(proj => proj.NugetReferences.Any(g => g.PackageName == ReferenceCopAnalyzer))
                .ToList();

            refCopAssemblies.Should().HaveCount(1);
            refCopAssemblies.Single().AssemblyName.Should().Be(AssemblyNamespaces.Application);
        }

        [Fact]
        public void XUnitAnalyzer_HasSameVersion()
        {
            var distinctVersions = VsSolutionFactory.Create()
                .TestProjects
                .SelectMany(f => f.NugetReferences)
                .Where(f => f.PackageName == XUnitAnalyzerName)
                .Select(f => f.PackageVersion)
                .Distinct()
                .ToList();

            distinctVersions.Should().HaveCount(1);
        }

        [Fact]
        public void XUnitAnalyzer_IsInAllTestProjects()
        {
            var missingPackages = VsSolutionFactory.Create()
                .TestProjects
                .Where(proj => proj.NugetReferences.All(g => g.PackageName != XUnitAnalyzerName))
                .Select(proj => proj.AssemblyName);

            missingPackages.Should().BeEmpty();
        }
    }
}