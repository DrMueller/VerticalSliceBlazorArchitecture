using FluentAssertions;
using VerticalSliceBlazorArchitecture.Common.InformationHandling;
using Xunit;

namespace VerticalSliceBlazorArchitecture.UnitTests.Common.InformationHandling
{
    public class InformationEntriesUnitTests
    {
        [Fact]
        public void AddingEmptyError_DoesNotAppendError()
        {
            // Arrange
            var infoEntries = InformationEntries.CreateNew();

            // Act
            var actualInfoEntries = infoEntries.AddError(string.Empty);

            // Assert
            actualInfoEntries.ErrorMessages.Count.Should().Be(0);
        }

        [Fact]
        public void AddingEmptyInformation_DoesNotAppendInfomration()
        {
            // Arrange
            var infoEntries = InformationEntries.CreateNew();

            // Act
            var actualInfoEntries = infoEntries.AddInformation(string.Empty);

            // Assert
            actualInfoEntries.InfoMessages.Count.Should().Be(0);
        }

        [Fact]
        public void AddingEmptyWarning_DoesNotAppendWarning()
        {
            // Arrange
            var infoEntries = InformationEntries.CreateNew();

            // Act
            var actualInfoEntries = infoEntries.AddWarning(string.Empty);

            // Assert
            actualInfoEntries.WarningMessages.Count.Should().Be(0);
        }

        [Fact]
        public void AddingInformation_CreatesNewInfoEntries()
        {
            // Arrange
            const string InfoMessage1 = "info message 1";
            const string InfoMessage2 = "info message 2";

            var infoEntries = InformationEntries.CreateNew().AddInformation(InfoMessage1);

            // Act
            var actualInfoEntries = infoEntries.AddInformation(InfoMessage2);

            // Assert
            actualInfoEntries.InfoMessages.Count.Should().Be(2);
            actualInfoEntries.InfoMessages.Should().Contain(InfoMessage1);
            actualInfoEntries.InfoMessages.Should().Contain(InfoMessage2);
        }

        [Fact]
        public void CheckingIfEmpty_ContainingEntries_ReturnsTrue()
        {
            // Arrange
            var infoEntries = InformationEntries.CreateNew().AddInformation("Tra");

            // Act & Assert
            infoEntries.IsEmpty.Should().BeFalse();
        }

        [Fact]
        public void CheckingIfEmpty_ContainingNoEntries_ReturnsTrue()
        {
            // Arrange
            var infoEntries = InformationEntries.CreateNew();

            // Act & Assert
            infoEntries.IsEmpty.Should().BeTrue();
        }

        [Fact]
        public void CreatingFromError_CreatesFromError()
        {
            // Arrange
            const string ErrorMessage = "Error message";

            // Act
            var actualInfoEntries = InformationEntries.CreateFromError(ErrorMessage);

            // Assert
            actualInfoEntries.ErrorMessages.Count.Should().Be(1);
            actualInfoEntries.ErrorMessages.Single().Should().Be(ErrorMessage);
        }

        [Fact]
        public void CreatingNew_CreatesEmpty()
        {
            // Act
            var actualInfoEntries = InformationEntries.CreateNew();

            // Assert
            actualInfoEntries.InfoMessages.Should().BeEmpty();
            actualInfoEntries.WarningMessages.Should().BeEmpty();
            actualInfoEntries.ErrorMessages.Should().BeEmpty();
        }

        [Fact]
        public void HasErrors_HavingErrors_ReturnsTrue()
        {
            // Arrange
            var infoEntry = InformationEntries.CreateNew().AddError("tra");

            // Act
            var actualHasErrors = infoEntry.HasErrors;

            // Assert
            actualHasErrors.Should().BeTrue();
        }

        [Fact]
        public void HasErrors_NotHavingErrors_ReturnsFalse()
        {
            // Arrange
            var infoEntry = InformationEntries.CreateNew().AddInformation("Info").AddWarning("Warnings");

            // Act
            var actualHasErrors = infoEntry.HasErrors;

            // Assert
            actualHasErrors.Should().BeFalse();
        }

        [Fact]
        public void Merging_MergesInfoEntries()
        {
            // Arrange
            var infoEntries1 = InformationEntries
                .CreateNew()
                .AddError("1")
                .AddWarning("2")
                .AddInformation("3");

            var infoEntries2 = InformationEntries
                .CreateNew()
                .AddWarning("4")
                .AddWarning("5")
                .AddInformation("6");

            // Act
            var actualInfoEntries = infoEntries1.MergeWith(infoEntries2);

            // Assert
            actualInfoEntries.InfoMessages.Count.Should().Be(infoEntries1.InfoMessages.Count + infoEntries2.InfoMessages.Count);
            actualInfoEntries.WarningMessages.Count.Should().Be(infoEntries1.WarningMessages.Count + infoEntries2.WarningMessages.Count);
            actualInfoEntries.ErrorMessages.Count.Should().Be(infoEntries1.ErrorMessages.Count + infoEntries2.ErrorMessages.Count);
        }
    }
}