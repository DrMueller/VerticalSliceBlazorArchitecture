using VerticalSliceBlazorArchitecture.Common.LanguageExtensions.Invariance;

namespace VerticalSliceBlazorArchitecture.QualityTests.TestingInfrastructure.SolutionMetadata.Models
{
    public class FileItemGroup
    {
        public IReadOnlyCollection<FileItemGroupEntry> Entries { get; }

        public FileItemGroup(IReadOnlyCollection<FileItemGroupEntry> entries)
        {
            Guard.ObjectNotNull(() => entries);

            Entries = entries;
        }
    }
}