using FluentAssertions;
using JetBrains.Annotations;
using VerticalSliceBlazorArchitecture.Common.LanguageExtensions.Types.Maybes;
using VerticalSliceBlazorArchitecture.Common.LanguageExtensions.Types.Maybes.Implementation;

namespace VerticalSliceBlazorArchitecture.Testing.Common.Infrastructure
{
    [PublicAPI]
    public static class MaybeTestAdapter
    {
        public static void ShouldBeNone<T>(this Maybe<T> actualValue)
        {
            actualValue.Should().BeOfType<None<T>>();
        }

        public static T ShouldBeSome<T>(this Maybe<T> actualValue)
        {
            var some = actualValue.Should().BeOfType<Some<T>>().Subject;

            return (T)some;
        }
    }
}