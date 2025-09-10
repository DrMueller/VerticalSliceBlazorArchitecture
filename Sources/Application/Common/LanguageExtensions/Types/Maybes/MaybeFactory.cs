using JetBrains.Annotations;
using VerticalSliceBlazorArchitecture.Common.LanguageExtensions.Types.Maybes.Implementation;

namespace VerticalSliceBlazorArchitecture.Common.LanguageExtensions.Types.Maybes
{
    [PublicAPI]
    public static class MaybeFactory
    {
        public static Maybe<T> CreateFromNullable<T>(T? possiblyNull)
        {
            return possiblyNull == null ? None.Value : possiblyNull;
        }

        public static Maybe<T> CreateFromNullable<T>(T? possiblyNull)
            where T : struct
        {
            return possiblyNull == null ? None.Value : possiblyNull;
        }
    }
}