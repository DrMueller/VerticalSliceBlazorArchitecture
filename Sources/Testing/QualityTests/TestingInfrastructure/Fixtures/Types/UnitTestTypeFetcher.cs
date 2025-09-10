using VerticalSliceBlazorArchitecture.QualityTests.TestingInfrastructure.Fixtures.Assemblies;

namespace VerticalSliceBlazorArchitecture.QualityTests.TestingInfrastructure.Fixtures.Types
{
    public static class UnitTestTypeFetcher
    {
        private static readonly Lazy<IReadOnlyCollection<Type>> _lazyUnitTestTypes = new(FetchInternal);

        public static IReadOnlyCollection<Type> FetchAllUnitTestTypes()
        {
            return _lazyUnitTestTypes.Value;
        }

        private static IReadOnlyCollection<Type> FetchInternal()
        {
            return AssemblyFetcher.FetchUnitTestAssemblies().SelectMany(f => f.GetTypes().Where(t => t.Name.EndsWith("UnitTests"))).ToList();
        }
    }
}