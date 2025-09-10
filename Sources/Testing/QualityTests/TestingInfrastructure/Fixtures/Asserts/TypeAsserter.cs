using FluentAssertions;
using VerticalSliceBlazorArchitecture.Testing.Common.Reflections;

namespace VerticalSliceBlazorArchitecture.QualityTests.TestingInfrastructure.Fixtures.Asserts
{
    internal static class TypeAsserter
    {
        internal static void AssertAreImmutable(IEnumerable<Type> types)
        {
            List<Type> failingTypes = [];

            foreach (var type in types)
            {
                var add = type.GetProperties().Any(x => x.CanWrite && !x.IsInitOnly());

                if (add)
                {
                    failingTypes.Add(type);
                }
            }

            failingTypes.Should().BeNullOrEmpty();
        }
    }
}