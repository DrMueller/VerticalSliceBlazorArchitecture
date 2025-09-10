using System.Reflection;

namespace VerticalSliceBlazorArchitecture.QualityTests.TestingInfrastructure.Fixtures.Assemblies
{
    public static class AssemblyFetcher
    {
        private static readonly IReadOnlyCollection<Assembly> _implementationAssemblies = new List<Assembly>
        {
            AssemblyProvider.Implementations.Applicatiom
        };

        private static readonly IReadOnlyCollection<Assembly> _unitTestAssemblies = new List<Assembly>
        {
            AssemblyProvider.Testing.UnitTests,
        };

        public static IReadOnlyCollection<Assembly> FetchAllAssemblies()
        {
            return _implementationAssemblies
                .Union(_unitTestAssemblies)
                .Union([
                    AssemblyProvider.Testing.QualityTests
                ])
                .ToList();
        }

        public static IReadOnlyCollection<Assembly> FetchImplementationAssemblies()
        {
            return _implementationAssemblies;
        }

        public static IReadOnlyCollection<Assembly> FetchUnitTestAssemblies()
        {
            return _unitTestAssemblies;
        }
    }
}