using JetBrains.Annotations;

namespace VerticalSliceBlazorArchitecture.QualityTests.TestingInfrastructure.SolutionMetadata.Services
{
    [PublicAPI]
    internal static class CsProjectFilesLocator
    {
        internal static IReadOnlyCollection<string> GetAllCsProjFiles()
        {
            var sourcesDir = SourcesDirectoryLocator.GetSourcesDirectory();

            return Directory
                .GetFiles(sourcesDir.FullName, "*.csproj", SearchOption.AllDirectories)
                .Where(f => !f.Contains("Tools"))
                .ToList();
        }

        internal static string GetForProject(string projectName)
        {
            var sourcesDir = SourcesDirectoryLocator.GetSourcesDirectory();

            return Directory
                .GetFiles(sourcesDir.FullName, $"{projectName}.csproj", SearchOption.AllDirectories)
                .Single();
        }
    }
}