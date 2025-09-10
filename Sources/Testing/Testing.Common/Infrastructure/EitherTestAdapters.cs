using FluentAssertions;
using JetBrains.Annotations;
using VerticalSliceBlazorArchitecture.Common.InformationHandling;
using VerticalSliceBlazorArchitecture.Common.LanguageExtensions.Types.Eithers;
using VerticalSliceBlazorArchitecture.Common.LanguageExtensions.Types.Eithers.Implementation;

namespace VerticalSliceBlazorArchitecture.Testing.Common.Infrastructure
{
    [PublicAPI]
    public static class EitherTestAdapters
    {
        public static void AssertForbidden<T>(this Either<InformationEntries, T> either)
        {
            either.ShouldBeLeft();
            var actualInfoEntries = either.ReduceLeft();
            actualInfoEntries.AssertForbidden();
        }

        public static TLeft ReduceLeft<TLeft, TRight>(
            this Either<TLeft, TRight> either)
        {
            return (Left<TLeft, TRight>)either;
        }

        public static TRight ReduceRight<TLeft, TRight>(
            this Either<TLeft, TRight> either)
        {
            return (Right<TLeft, TRight>)either;
        }

        public static void ShouldBeLeft<TLeft, TRight>(this Either<TLeft, TRight> either)
        {
            either.Should().BeOfType<Left<TLeft, TRight>>();
        }

        public static TRight ShouldBeRight<TLeft, TRight>(this Either<TLeft, TRight> either)
        {
            return either.Should().BeOfType<Right<TLeft, TRight>>().Subject;
        }
    }
}