﻿using JetBrains.Annotations;
using VerticalSliceBlazorArchitecture.Common.LanguageExtensions.Types.Eithers;
using VerticalSliceBlazorArchitecture.Common.LanguageExtensions.Types.Eithers.Implementation;

namespace VerticalSliceBlazorArchitecture.Common.InformationHandling
{
    [PublicAPI]
    public static class InformationEntriesEitherAdapters
    {
        public static async Task<(InformationEntries, TRight?)> ToNullableTupleAsync<TRight>(
            this Task<Either<InformationEntries, TRight>> eitherTask, Func<TRight?> whenNone)
        {
#pragma warning disable VSTHRD003
            var either = await eitherTask;
#pragma warning restore VSTHRD003
            if (either is Right<InformationEntries, TRight> right)
            {
                return (InformationEntries.Empty, right);
            }

            var left = (Left<InformationEntries, TRight>)either;

            return (left, whenNone());
        }

        public static(InformationEntries, TRight) ToTuple<TRight>(
            this Either<InformationEntries, TRight> either, Func<TRight> whenNone)
        {
            if (either is Right<InformationEntries, TRight> right)
            {
                return (InformationEntries.Empty, right);
            }

            var left = (Left<InformationEntries, TRight>)either;

            return (left, whenNone());
        }

        public static(InformationEntries, TRight?) ToTuple<TRight>(
            this Either<InformationEntries, TRight> either)
            where TRight : class
        {
            if (either is Right<InformationEntries, TRight?> right)
            {
                return (InformationEntries.Empty, right);
            }

            var left = (Left<InformationEntries, TRight>)either;

            return (left, null);
        }

        public static async Task<(InformationEntries, TRight)> ToTupleAsync<TRight>(
            this Task<Either<InformationEntries, TRight>> eitherTask, Func<TRight> whenNone)
        {
#pragma warning disable VSTHRD003
            var either = await eitherTask;
#pragma warning restore VSTHRD003
            if (either is Right<InformationEntries, TRight> right)
            {
                return (InformationEntries.Empty, right);
            }

            var left = (Left<InformationEntries, TRight>)either;

            return (left, whenNone());
        }
    }
}