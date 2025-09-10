using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NetArchTest.Rules;
using VerticalSliceBlazorArchitecture.Data.Base;
using VerticalSliceBlazorArchitecture.DataAccess.DbContexts.Contexts.Implementation;
using VerticalSliceBlazorArchitecture.QualityTests.TestingInfrastructure.Fixtures.Quality;
using Xunit;

namespace VerticalSliceBlazorArchitecture.QualityTests.TestingAreas.DataAccess
{
    public class EfRecordsConfigurationTests(QualityTestFixture fixture) : QualityTestBase(fixture)
    {
        [Fact]
        public void AllEntityConfigurations_AreInCorrectNaemspace()
        {
            var expectedNameSpace = typeof(EfRecordBase).Namespace!.Replace(".Base", string.Empty);

            var result = Types.InAssembly(typeof(AppDbContext).Assembly)
                .That().ImplementInterface(typeof(IEntityTypeConfiguration<>))
                .And().AreNotAbstract()
                .Should().ResideInNamespace(expectedNameSpace)
                .GetResult();

            result.FailingTypes.Should().BeNullOrEmpty();
        }
    }
}