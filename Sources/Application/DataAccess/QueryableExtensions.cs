using JetBrains.Annotations;
using System.Linq.Expressions;
using VerticalSliceBlazorArchitecture.Common.LanguageExtensions.Types.Maybes;
using VerticalSliceBlazorArchitecture.Common.LanguageExtensions.Types.Maybes.Implementation;

namespace VerticalSliceBlazorArchitecture.DataAccess
{
    [PublicAPI]
    internal static class QueryableExtensions
    {
        internal static IQueryable<T> WhereOptional<T, TVal>(
            this IQueryable<T> qry,
            Maybe<TVal> maybe,
            Func<TVal, Expression<Func<T, bool>>> predicateFactory)
        {
            if (maybe is None<TVal>)
            {
                return qry;
            }

            TVal value = (Some<TVal>)maybe;

            return qry.Where(predicateFactory(value));
        }
    }
}