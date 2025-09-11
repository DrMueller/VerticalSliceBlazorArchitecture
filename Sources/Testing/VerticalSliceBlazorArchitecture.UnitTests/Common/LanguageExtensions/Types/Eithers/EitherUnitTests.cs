using FluentAssertions;
using VerticalSliceBlazorArchitecture.Common.LanguageExtensions.Types.Eithers;
using VerticalSliceBlazorArchitecture.Common.LanguageExtensions.Types.Eithers.Implementation;
using Xunit;

namespace VerticalSliceBlazorArchitecture.UnitTests.Common.LanguageExtensions.Types.Eithers
{
    public class EitherUnitTests
    {
        [Fact]
        public void CreatingLeft_CreatesLeft()
        {
            // Act
            Either<string, int> left = "Test";

            // Assert
            left.Should().BeOfType<Left<string, int>>();
        }

        [Fact]
        public void CreatingRight_CreatesRight()
        {
            // Act
            Either<string, int> right = 123;

            // Assert
            right.Should().BeOfType<Right<string, int>>();
        }

        [Fact]
        public void Mapping_EitherBeingLeft_ReturnsLeft()
        {
            // Arrange
            const string String = "Test";
            Either<string, int> right = String;

            // Act
            var actualLeft = right.MapRight(num => num + 123);

            // Assert
            actualLeft.Should().BeOfType<Left<string, int>>();
        }

        [Fact]
        public void Mapping_EitherBeingRight_ReturnsMappedValue()
        {
            // Arrange
            const int IntValue = 123;
            const int ExpectedIntValue = IntValue + IntValue;

            Either<string, int> right = IntValue;

            // Act
            var actualValue = right
                .MapRight(num => num + IntValue)
                .Reduce(_ => 1);

            // Assert
            actualValue.Should().Be(ExpectedIntValue);
        }

        [Fact]
        public void Mapping_EitherBeingRight_ReturnsNewRight()
        {
            // Arrange
            Either<string, int> right = 123;

            // Act
            var actualNewRight = right.MapRight(num => num.ToString());

            // Assert
            actualNewRight.Should().BeOfType<Right<string, string>>();
        }

        [Fact]
        public void Reducing_EitherBeingLeft_ReturnLeftCallbackValue()
        {
            // Arrange
            const int ExpectedInt = 123;
            Either<string, int> left = ExpectedInt.ToString();

            // Act
            var actualValue = left.Reduce(int.Parse);

            // Assert
            actualValue.Should().Be(ExpectedInt);
        }

        [Fact]
        public void Reducing_EitherBeingRight_ReturnsRightValue()
        {
            // Arrange
            const int IntValue = 123;
            Either<string, int> right = IntValue;

            // Act
            var actualValue = right.Reduce(_ => 1);

            // Assert
            actualValue.Should().Be(IntValue);
        }
    }
}