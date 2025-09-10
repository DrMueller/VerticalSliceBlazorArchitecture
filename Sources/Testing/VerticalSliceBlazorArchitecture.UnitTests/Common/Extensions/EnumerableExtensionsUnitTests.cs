using FluentAssertions;
using VerticalSliceBlazorArchitecture.Common.Extensions;
using Xunit;

namespace VerticalSliceBlazorArchitecture.UnitTests.Common.Extensions
{
    public class EnumerableExtensionsUnitTests
    {
        [Fact]
        public void ContainsAny_OtherListBeingNull_ReturnsFalse()
        {
            // Arrange
            var list1 = new List<long>
            {
                1,
                3,
                5
            };

            // Act
            var actualContainsAny = list1.ContainsAny(null!);

            // Assert
            actualContainsAny.Should().BeFalse();
        }

        [Fact]
        public void ContainsAny_OtherListContainsEntry_ReturnsTrue()
        {
            // Arrange
            var list1 = new List<long>
            {
                1,
                3,
                5
            };

            var list2 = new List<long>
            {
                2,
                4,
                5
            };

            // Act
            var actualContainsAny = list1.ContainsAny(list2);

            // Assert
            actualContainsAny.Should().BeTrue();
        }

        [Fact]
        public void ContainsAny_OtherListDoesNotContainEntry_ReturnsFalse()
        {
            // Arrange
            var list1 = new List<long>
            {
                1,
                3,
                5
            };

            var list2 = new List<long>
            {
                2,
                4,
                6
            };

            // Act
            var actualContainsAny = list1.ContainsAny(list2);

            // Assert
            actualContainsAny.Should().BeFalse();
        }

        [Fact]
        public async Task SelectAsync_ExecutesTasks()
        {
            // Arrange
            var intList = Enumerable.Range(0, 10).ToList();

            // Act
            var actualResult = await intList.SelectAsync(Task.FromResult);

            // Assert
            actualResult.Should().BeEquivalentTo(intList);
        }

        [Fact]
        public async Task SelectAsync_ExecutesTasks_OneAfterAnother()
        {
            // Arrange
            var newList = new List<int>();

            // Act
            var actualResult = await Enumerable.Range(0, 10).SelectAsync(i =>
            {
                newList.Add(i);

                return Task.FromResult(i);
            });

            // Assert
            actualResult.Should().BeEquivalentTo(newList);
        }
    }
}