using FluentAssertions;
using VerticalSliceBlazorArchitecture.Common.LanguageExtensions.Types.Maybes;
using VerticalSliceBlazorArchitecture.Common.LanguageExtensions.Types.Maybes.Implementation;
using VerticalSliceBlazorArchitecture.DataAccess;
using Xunit;

namespace VerticalSliceBlazorArchitecture.UnitTests.DataAccess
{
    internal record TestModel(int Id, string Name);

    public class QueryableExtensionsUnitTests
    {
        private const string TestName1 = "Test1";
        private const int TestId1 = 1;

        private readonly IQueryable<TestModel> _testQuery = new List<TestModel>
        {
            new(TestId1, TestName1),
            new(2, "Test2"),
            new(3, "Test3")
        }.AsQueryable();

        [Fact]
        public void WhereOptional_MaybeBeingNone_ReturnsQuery()
        {
            // Arrange
            Maybe<string> noneMaybe = None.Value;

            // Act
            var actualQuery = _testQuery.WhereOptional(noneMaybe, str => model => model.Name == str);

            // Assert
            actualQuery.Should().BeSameAs(_testQuery);
        }

        [Fact]
        public void WhereOptional_MaybeBeingSome_FiltersData()
        {
            // Arrange
            Maybe<string> someMaybe = TestName1;

            // Act
            var actualQuery = _testQuery.WhereOptional(someMaybe, str => model => model.Name == str);

            // Assert
            actualQuery.Should().HaveCount(1);
            actualQuery.Single().Id.Should().Be(TestId1);
            actualQuery.Single().Name.Should().Be(TestName1);
        }
    }
}