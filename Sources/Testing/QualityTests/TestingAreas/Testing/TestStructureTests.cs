using System.Text;
using VerticalSliceBlazorArchitecture.Common.InformationHandling;
using VerticalSliceBlazorArchitecture.QualityTests.TestingInfrastructure.Fixtures.Types;
using Xunit;

namespace VerticalSliceBlazorArchitecture.QualityTests.TestingAreas.Testing
{
    public partial class TestStructureTests
    {
        [Fact]
        public void UnitTestNamespaces_AreCorrect()
        {
            var unitTestTypes = UnitTestTypeFetcher.FetchAllUnitTestTypes();

            // Arrange
            var implementationTypes = GetImplementationTypeNames();
            var infoEntries = InformationEntries.CreateNew();

            // Act
            foreach (var testClass in unitTestTypes)
            {
                var implementationClassName = CreateImplementationClassFullName(testClass, TestSuffix, true);
                var logicTypes = implementationTypes.Where(f => f == implementationClassName).ToList();

                if (!logicTypes.Any())
                {
                    implementationClassName = CreateImplementationClassFullName(testClass, TestSuffix, false);
                    logicTypes = implementationTypes.Where(f => f == implementationClassName).ToList();
                }

                if (!logicTypes.Any())
                {
                    infoEntries = infoEntries.AddError($"Could not find implementation type {implementationClassName}.");
                }

                if (logicTypes.Count > 1)
                {
                    infoEntries = infoEntries.AddError($"Found multiple implementation types {implementationClassName}.");
                }
            }

            if (!infoEntries.HasErrors)
            {
                return;
            }

            // Assert
            var errorMessage = infoEntries.ErrorMessages.Aggregate(new StringBuilder(), (sb, errMsg) => sb.AppendLine(errMsg)).ToString();
            Assert.Fail(errorMessage);
        }
    }
}