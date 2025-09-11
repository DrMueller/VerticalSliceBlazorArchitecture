using FluentAssertions;
using VerticalSliceBlazorArchitecture.Common.LanguageExtensions.Invariance;
using Xunit;

namespace VerticalSliceBlazorArchitecture.UnitTests.Common.LanguageExtensions.Invariance
{
    public class GuardUnitTests
    {
        [Fact]
        public void CheckCollectionNullOrEmpty_WithEmptyCollection_ThrowsArgumentException()
        {
            // Arrange
            var modelWithEmptyCollection = new GuardTestModel("Test", new object(), new List<object>());

            // Act & Assert
            Assert.Throws<ArgumentException>(() => Guard.CollectionNotNullOrEmpty(() => modelWithEmptyCollection.TestCollection));
        }

        [Fact]
        public void CheckCollectionNullOrEmpty_WithFilledCollection_DoesNotThrowException()
        {
            // Arrange
            var modelWithFilledCollection = new GuardTestModel(
                "Test",
                new object(),
                new List<object>
                {
                    new()
                });

            var act = () => Guard.CollectionNotNullOrEmpty(() => modelWithFilledCollection.TestCollection);

            // Act & Assert
            act.Should().NotThrow();
        }

        [Fact]
        public void CheckCollectionNullOrEmpty_WithNullCollection_ThrowsArgumentException()
        {
            // Arrange
            var modelWithNullCollection = new GuardTestModel("Test", new object(), null!);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => Guard.CollectionNotNullOrEmpty(() => modelWithNullCollection.TestCollection));
        }

        [Fact]
        public void CheckingObjectNull_WithNullObject_ThrowsArgumentException()
        {
            // Given
            var modelWithObjectNull = new GuardTestModel("Test", null!, new List<object>());

            // Act & Assert
            Assert.Throws<ArgumentException>(() => Guard.ObjectNotNull(() => modelWithObjectNull.TestObject));
        }

        [Fact]
        public void CheckingObjectNull_WithSetObject_DoesNotThrowException()
        {
            // Arrange
            var modelWithSetobject = new GuardTestModel("Test", new object(), new List<object>());

            var act = () => Guard.ObjectNotNull(() => modelWithSetobject.TestObject);

            // Act & Assert
            act.Should().NotThrow();
        }

        [Fact]
        public void CheckingStringNullOrEmpty_WithSetString_DoesNotThrowException()
        {
            // Arrange
            var modelWithSetString = new GuardTestModel("Test", new object(), new List<object>());

            var act = () => Guard.StringNotNullOrEmpty(() => modelWithSetString.TestString);

            // Act & Assert
            act.Should().NotThrow();
        }

        [Fact]
        public void CheckingValueNotDefault_WithValueNotDefault_DoesNotThrowException()
        {
            // Arrange
            var actualGuid = Guid.NewGuid();

            var act = () => Guard.ValueNotDefault(() => actualGuid);

            // Act & Assert
            act.Should().NotThrow();
        }

        [Fact]
        public void CheckingValuePositive_ValueBeing0_ThrowsArgumentException()
        {
            // Arrange
            var model = new GuardTestModel("Test", new object(), new List<object>());

            // Act & Assert
            Assert.Throws<ArgumentException>(() => Guard.ValuePositive(() => model.TestInt));
        }

        [Fact]
        public void CheckingValuePositive_ValueBeingNegative_ThrowsArgumentException()
        {
            // Arrange
            var modelWithNegativeInt = new GuardTestModel("Test", new object(), new List<object>(), -1);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => Guard.ValuePositive(() => modelWithNegativeInt.TestInt));
        }

        [Fact]
        public void CheckingValuePositive_ValueBeingPositive_DoesNotThrowException()
        {
            // Arrange
            var model = new GuardTestModel("Test", new object(), new List<object>(), 123);

            var act = () => Guard.ValuePositive(() => model.TestInt);

            // Act & Assert
            act.Should().NotThrow();
        }

        [Fact]
        public void CheckThat_WithThatBeingFalse_DoesThrowArgumentException_WithPassedMessage()
        {
            // Arrange
            var modelWithoutString = new GuardTestModel(string.Empty, null!, new List<object>());
            var passedMessage = Guid.NewGuid() + "String must not be null or empty.";

            var act = () => Guard.That(() => !string.IsNullOrEmpty(modelWithoutString.TestString), passedMessage);

            // Act & Assert
            act.Should().Throw<ArgumentException>().WithMessage(passedMessage);
        }

        [Fact]
        public void CheckThat_WithThatBeingTrue_DoesNotThrowException()
        {
            // Arrange
            var modelWithoutString = new GuardTestModel(string.Empty, null!, new List<object>());
            var passedMessage = Guid.NewGuid() + "String must not be null or empty.";

            var act = () => Guard.That(() => string.IsNullOrEmpty(modelWithoutString.TestString), passedMessage);

            // Act & Assert
            act.Should().NotThrow();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void CheckingStringNullOrEmpty_WithNullOrEmptyString_ThrowsArgumentException(string? actual)
        {
            // Arrange
            var modelWithNullString = new GuardTestModel(actual, new object(), new List<object>());

            // Act & Assert
            Assert.Throws<ArgumentException>(() => Guard.StringNotNullOrEmpty(() => modelWithNullString.TestString));
        }

        [Theory]
        [InlineData(default(int))]
        [InlineData(default(long))]
        [InlineData(default(short))]
        public void CheckingValueNotDefault_WithValueDefault_ThrowsArgumentException<T>(T actualDefault)
            where T : struct
        {
            // Arrange
            var act = () => Guard.ValueNotDefault(() => actualDefault);

            // Act & Assert
            act.Should().ThrowExactly<ArgumentException>();
        }
    }
}