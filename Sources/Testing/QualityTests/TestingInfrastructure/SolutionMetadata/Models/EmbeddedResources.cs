using JetBrains.Annotations;

namespace VerticalSliceBlazorArchitecture.QualityTests.TestingInfrastructure.SolutionMetadata.Models
{
    [PublicAPI]
    public class EmbeddedResources(IReadOnlyCollection<EmbeddedResource> entries)
    {
        public IReadOnlyCollection<EmbeddedResource> FrenchResources
        {
            get { return entries.Where(f => f.UpdatePath.EndsWith(".fr.resx", StringComparison.OrdinalIgnoreCase)).ToList(); }
        }

        public IReadOnlyCollection<EmbeddedResource> GermanResources
        {
            get { return entries.Where(f => f.UpdatePath.EndsWith(".resx", StringComparison.OrdinalIgnoreCase)).Except(FrenchResources).ToList(); }
        }
    }
}