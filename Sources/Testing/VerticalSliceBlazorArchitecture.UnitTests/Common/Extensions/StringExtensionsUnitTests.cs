using FluentAssertions;
using VerticalSliceBlazorArchitecture.Common.Extensions;
using Xunit;

namespace VerticalSliceBlazorArchitecture.UnitTests.Common.Extensions
{
    public class StringExtensionsUnitTests
    {
        [Fact]
        public void AppendingWithSeparatorIfNotNullOrWhitespace_AppendTextAndBaseTextNotNullOrWhitespace_AppendsWithSeparator()
        {
            // Arrange
            var baseText = "test";
            var separator = ",";
            var appendText = "append";

            // Act
            var actualText = baseText.AppendWithSeparatorIfNotNullOrWhitespace(separator, appendText);

            // Assert
            actualText.Should().Be(baseText + separator + appendText);
        }

        [Fact]
        public void StartsWithLetter_StringIsEmpty_ReturnsFalse()
        {
            // Arrange
            var value = "";

            // Act
            var result = value.StartsWithLetter();

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void StartsWithLetter_StringIsNull_ReturnsFalse()
        {
            // Arrange
            string? value = null;

            // Act
            var result = value.StartsWithLetter();

            // Assert
            result.Should().BeFalse();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void AppendingWithSeparatorIfNotNullOrWhitespace_AppendTextNullOrWhitespace_ReturnsBaseText(string? appendText)
        {
            // Arrange
            var baseText = "test";
            var separator = ",";

            // Act
            var actualText = baseText.AppendWithSeparatorIfNotNullOrWhitespace(separator, appendText);

            // Assert
            actualText.Should().Be(baseText);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        public void AppendingWithSeparatorIfNotNullOrWhitespace_BaseTextEmptyOrWhitespace_AppendsTextWithoutSeparator(string baseText)
        {
            // Arrange
            var separator = ",";
            var appendText = "append";

            // Act
            var actualText = baseText.AppendWithSeparatorIfNotNullOrWhitespace(separator, appendText);

            // Assert
            actualText.Should().Be(appendText);
        }
    }
}