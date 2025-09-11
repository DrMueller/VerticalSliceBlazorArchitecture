using FluentAssertions;
using VerticalSliceBlazorArchitecture.Common.LanguageExtensions.Types.Maybes;
using VerticalSliceBlazorArchitecture.Common.LanguageExtensions.Types.Maybes.Implementation;
using Xunit;

namespace VerticalSliceBlazorArchitecture.UnitTests.Common.LanguageExtensions.Types.Maybes
{
    public class MaybeUnitTests
    {
        [Fact]
        public void CastingSome_CastsToValue()
        {
            // Arrange
            const string Str = "Test";
            var actualSome = new Some<string>(Str);

            // Act
            string actualString = actualSome;

            // Assert
            actualString.Should().Be(Str);
        }

        [Fact]
        public void CastingValue_CastsToSome()
        {
            // Arrange
            const string Str = "Test";

            // Act
            Maybe<string> actualMaybe = Str;

            // Assert
            actualMaybe.Should().BeOfType<Some<string>>();
        }

        [Fact]
        public void ComparingMaybes_BothBeingNone_ReturnsTrue()
        {
            // Arrange
            Maybe<string> none1 = None.Value;
            Maybe<string> none2 = None.Value;

            // Act
            var areEqual = none1 == none2;

            // Assert
            areEqual.Should().BeTrue();
        }

        [Fact]
        public void ComparingMaybes_OneBeingNoneOneBeingSome_ReturnsFalse()
        {
            // Arrange
            Maybe<string> none = None.Value;
            Maybe<string> some = "tra";

            // Act
            var areEqual = none == some;

            // Assert
            areEqual.Should().BeFalse();
        }

        [Fact]
        public void Mapping_MaybeBeingNone_ReturnsSameNone()
        {
            // Arrange
            Maybe<string> noneMaybe = None.Value;

            // Act
            var actualMaybe = noneMaybe.Map(f => f.ToString());

            // Assert
            actualMaybe.Should().BeOfType(typeof(None<string>));
        }

        [Fact]
        public void Mapping_MaybeBeingSome_ReturnsSome_WithNewMappedValue()
        {
            // Arrange
            const string InitialValue = "1234";

            Maybe<string> someMaybe = InitialValue;

            // Act
            var actualMaybe = someMaybe.Map(int.Parse);

            // Assert
            actualMaybe.Should().BeOfType(typeof(Some<int>));
            var actualValue = (int)(Some<int>)actualMaybe;

            var expectedValue = int.Parse(InitialValue);
            actualValue.Should().Be(expectedValue);
        }

        [Fact]
        public void ReduceNullable_MaybeBeingNone_ReturnsCallbackValue()
        {
            // Arrange
            Maybe<int> noneMaybe = None.Value;

            // Act
            var actualValue = noneMaybe.ReduceNullable(() => null);

            // Assert
            actualValue.Should().BeNull();
        }

        [Fact]
        public void ReduceNullable_MaybeBeingSome_ReturnsValue()
        {
            // Arrange
            const int Value = 1234;
            Maybe<int> someMaybe = Value;

            // Act
            var actualValue = someMaybe.ReduceNullable(() => null);

            // Assert
            actualValue.Should().Be(Value);
        }

        [Fact]
        public void Reducing_MaybeBeingNone_ReturnsCallbackValue()
        {
            // Arrange
            const string CallbackValue = "1234";

            Maybe<string> someMaybe = None.Value;

            // Act
            var actualValue = someMaybe.Reduce(() => CallbackValue);

            // Assert
            actualValue.Should().Be(CallbackValue);
        }

        [Fact]
        public void Reducing_MaybeBeingSome_ReturnsValue()
        {
            // Arrange
            const string InitialValue = "1234";

            Maybe<string> someMaybe = InitialValue;

            // Act
            var actualValue = someMaybe.Reduce(() => "tra");

            // Assert
            actualValue.Should().Be(InitialValue);
        }

        [Fact]
        public async Task ReducingAsync_MaybeBeingNone_ReturnsCallbackValue()
        {
            // Arrange
            const string CallbackValue = "1234";

            Maybe<string> someMaybe = None.Value;

            // Act
            var actualValue = await someMaybe.ReduceAsync(() => Task.FromResult(CallbackValue));

            // Assert
            actualValue.Should().Be(CallbackValue);
        }

        [Fact]
        public async Task ReducingAsync_MaybeBeingSome_ReturnsValue()
        {
            // Arrange
            const string InitialValue = "1234";

            Maybe<string> someMaybe = InitialValue;

            // Act
            var actualValue = await someMaybe.ReduceAsync(() => Task.FromResult("tra"));

            // Assert
            actualValue.Should().Be(InitialValue);
        }

        [Fact]
        public void SelectingSome_OnlyNones_ReturnsEmptyList()
        {
            // Arrange
            var maybes = new List<Maybe<string>>
            {
                None.Value,
                None.Value
            };

            // Act
            var possibleSomes = maybes.SelectSome().ToList();

            // Assert
            possibleSomes.Should().NotBeNull();
            possibleSomes.Should().BeEmpty();
        }

        [Fact]
        public void SelectingSome_SelectsOnlySome()
        {
            // Arrange
            var strings = new List<string>
            {
                "String1",
                "String2"
            };

            var maybes = new List<Maybe<string>>
            {
                None.Value
            };

            foreach (var str in strings)
            {
                maybes.Add(str);
            }

            // Act
            var some = maybes.SelectSome();

            // Assert
            some.Should().BeEquivalentTo(strings);
        }

        [Fact]
        public async Task WhenSomeAsync_MaybeBeingNone_DoesNotExecuteCallback()
        {
            // Arrange 
            var sut = new None<string>();

            var callbackCalled = false;

            // Act
            await sut.WhenSomeAsync(_ =>
            {
                callbackCalled = true;

                return Task.CompletedTask;
            });

            // Assert
            callbackCalled.Should().BeFalse();
        }

        [Fact]
        public async Task WhenSomeAsync_MaybeBeingSome_ExecutesCallback()
        {
            // Arrange 
            var sut = new Some<string>("test");

            var callbackCalled = false;

            // Act
            await sut.WhenSomeAsync(_ =>
            {
                callbackCalled = true;

                return Task.CompletedTask;
            });

            // Assert
            callbackCalled.Should().BeTrue();
        }
    }
}