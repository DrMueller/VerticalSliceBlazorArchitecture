using FluentAssertions;
using VerticalSliceBlazorArchitecture.QualityTests.TestingInfrastructure.SolutionMetadata.Services;
using Xunit;

namespace VerticalSliceBlazorArchitecture.QualityTests.TestingAreas.Assemblies
{
    public class AssembliesTests
    {
        [Fact]
        public void NoBackupFiles_Exist()
        {
            var sourcesDir = SourcesDirectoryLocator.GetSourcesDirectory();
            var allCsProjFiles = Directory
                .GetFiles(sourcesDir.FullName, "*.csproj", SearchOption.AllDirectories);

            var backupFiles = allCsProjFiles.Where(f => f.Contains("Backup", StringComparison.OrdinalIgnoreCase)).ToList();

            backupFiles.Should().BeEmpty();
        }
    }
}