namespace VerticalSliceBlazorArchitecture.QualityTests.TestingInfrastructure.SolutionMetadata.Services
{
    public static class SourcesDirectoryLocator
    {
        public static DirectoryInfo GetSourcesDirectory()
        {
            var currentDir = new DirectoryInfo(Directory.GetCurrentDirectory());

            while (!currentDir!.FullName.EndsWith("Sources"))
            {
                currentDir = currentDir.Parent;
            }

            return currentDir;
        }
    }
}