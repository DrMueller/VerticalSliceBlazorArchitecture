using System.Reflection;
using FluentAssertions;
using VerticalSliceBlazorArchitecture.QualityTests.TestingInfrastructure.Fixtures.Types;
using Xunit;

namespace VerticalSliceBlazorArchitecture.QualityTests.TestingAreas.Testing
{
    public class UnitTestTests
    {
        [Fact]
        public void UnitTestClasses_HaveCorrectClassNames()
        {
            var allUnitTestTypes = UnitTestTypeFetcher.FetchAllUnitTestTypes();

            foreach (var type in allUnitTestTypes)
            {
                var sutField = type.GetField("_sut", BindingFlags.NonPublic | BindingFlags.Instance);

                if (sutField == null)
                {
                    continue;
                }

                var expectedClassName = $"{sutField.FieldType.Name}UnitTests";
                type.Name.Should().Be(expectedClassName);
            }
        }
    }
}