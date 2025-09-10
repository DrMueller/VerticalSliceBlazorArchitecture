using JetBrains.Annotations;
using VerticalSliceBlazorArchitecture.Common.LanguageExtensions.Invariance;

namespace VerticalSliceBlazorArchitecture.QualityTests.TestingInfrastructure.SolutionMetadata.Models
{
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class FileItemGroupEntry
    {
        public string BuildAction { get; }
        public string BuildContext { get; }
        public string FilePath { get; }

        public FileItemGroupEntry(
            string buildAction,
            string buildContext,
            string filePath)
        {
            Guard.StringNotNullOrEmpty(() => buildAction);
            Guard.StringNotNullOrEmpty(() => buildContext);
            Guard.StringNotNullOrEmpty(() => filePath);

            BuildAction = buildAction;
            BuildContext = buildContext;
            FilePath = filePath;
        }
    }
}