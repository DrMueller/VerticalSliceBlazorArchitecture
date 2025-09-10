using FluentAssertions;
using NetArchTest.Rules;
using VerticalSliceBlazorArchitecture.Data.Base;
using Xunit;

namespace VerticalSliceBlazorArchitecture.QualityTests.TestingAreas.DataAccess
{
    public class EfRecordsTests
    {
        [Fact]
        public void AllRecords_AreInCorrectNamespace()
        {
            var expectedNameSpace = typeof(EfRecordBase).Namespace!;

            var result = Types.InAssembly(typeof(EfRecordBase).Assembly)
                .That().Inherit(typeof(EfRecordBase))
                .Should().ResideInNamespace(expectedNameSpace)
                .GetResult();

            result.FailingTypes.Should().BeNullOrEmpty();
        }
    }
}