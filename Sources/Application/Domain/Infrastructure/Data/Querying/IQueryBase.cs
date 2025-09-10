using JetBrains.Annotations;
using VerticalSliceBlazorArchitecture.Data.Base;

namespace VerticalSliceBlazorArchitecture.Domain.Infrastructure.Data.Querying
{
    [PublicAPI]
    public interface IQueryBase
    {
        IQueryable<T> Query<T>()
            where T : EfRecordBase;
    }
}