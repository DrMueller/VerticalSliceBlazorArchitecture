using VerticalSliceBlazorArchitecture.QualityTests.TestingInfrastructure.Fixtures.Assemblies;

namespace VerticalSliceBlazorArchitecture.QualityTests.TestingAreas.Testing
{
    public partial class TestStructureTests
    {
        private const string TestSuffix = "UnitTests";

        private static string CreateImplementationClassFullName(Type testClass, string testSuffix, bool appendImplementation)
        {
            var logicTypeName = testClass.Name.Replace(testSuffix, string.Empty, StringComparison.OrdinalIgnoreCase);

            var logicTypeNamespacePart = testClass.Namespace!.Replace($".{TestSuffix}", "", StringComparison.Ordinal);

            if (appendImplementation)
            {
                logicTypeNamespacePart += ".Implementation";
            }

            var logicTypeFullName = $"{logicTypeNamespacePart}.{logicTypeName}";

            return logicTypeFullName;
        }

        private static string CutGenericMarker(string typeName)
        {
            const char GenericMarker = '`';

            if (!typeName.Contains(GenericMarker))
            {
                return typeName;
            }

            return typeName[..typeName.IndexOf(GenericMarker)];
        }

        private static IReadOnlyCollection<string> GetImplementationTypeNames()
        {
            return AssemblyFetcher
                .FetchImplementationAssemblies()
                .SelectMany(f => f.GetTypes())
                .Where(f => !string.IsNullOrEmpty(f.FullName))
                .Select(f => f.FullName)
                .Select(CutGenericMarker!)
                .Distinct() // Generic equatables (like RefId) add another type, which we want to have filtered as long as the namespaces are the same
                .ToList();
        }
    }
}