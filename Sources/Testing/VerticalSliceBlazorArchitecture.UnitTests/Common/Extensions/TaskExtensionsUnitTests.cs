using FluentAssertions;
using VerticalSliceBlazorArchitecture.Common.Extensions;
using Xunit;

namespace VerticalSliceBlazorArchitecture.UnitTests.Common.Extensions
{
    public class TaskExtensionsUnitTests
    {
        [Fact]
        public async Task SelectAsync_SelectsAsync()
        {
            // Arrange
            var list = new List<string>
            {
                "Test1",
                "Test2",
                "Test23"
            };

            var selectTask = Task.Run<IReadOnlyCollection<string>>(() => list);

            // Act
            var result = await selectTask.SelectListAsync(f => f);

            // Assert
            result.Should().BeEquivalentTo(list);
        }
    }
}