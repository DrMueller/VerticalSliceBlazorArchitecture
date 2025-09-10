using FluentAssertions;
using VerticalSliceBlazorArchitecture.Common.LanguageExtensions.Types.Maybes;
using VerticalSliceBlazorArchitecture.Common.LanguageExtensions.Types.Maybes.Implementation;
using Xunit;

namespace VerticalSliceBlazorArchitecture.UnitTests.Common.LanguageExtensions.Types.Maybes
{
    public class MaybeFactoryUnitTests
    {
        [Fact]
        public void CreatingMaybeFromNullable_WithExistingObjects_CreatesSome()
        {
            // Act
            var actualMaybe = MaybeFactory.CreateFromNullable(new object());

            // Assert
            actualMaybe.Should().BeOfType<Some<object>>();
        }

        [Fact]
        public void CreatingMaybeFromNullable_WithNull_CreatesNone()
        {
            // Act
            var actualMaybe = MaybeFactory.CreateFromNullable<object>(null!);

            // Assert
            actualMaybe.Should().BeOfType<None<object>>();
        }
    }
}