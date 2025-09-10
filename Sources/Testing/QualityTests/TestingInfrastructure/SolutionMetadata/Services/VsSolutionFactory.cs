using VerticalSliceBlazorArchitecture.QualityTests.TestingInfrastructure.SolutionMetadata.Models;

namespace VerticalSliceBlazorArchitecture.QualityTests.TestingInfrastructure.SolutionMetadata.Services
{
    internal static class VsSolutionFactory
    {
        private static readonly Lazy<VsSolution> _lazySolution = new(CreateInternal);

        internal static VsSolution Create()
        {
            return _lazySolution.Value;
        }

        private static VsSolution CreateInternal()
        {
            var projects = CsProjectFilesLocator
                .GetAllCsProjFiles()
                .Select(CsProjectFactory.Create)
                .ToList();

            return new VsSolution(projects);
        }
    }
}