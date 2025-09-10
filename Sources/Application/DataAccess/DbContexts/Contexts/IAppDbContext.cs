using System.Diagnostics.CodeAnalysis;
using VerticalSliceBlazorArchitecture.Data.Base;
using VerticalSliceBlazorArchitecture.Domain.Infrastructure.Data.Querying;

namespace VerticalSliceBlazorArchitecture.DataAccess.DbContexts.Contexts
{
    public interface IAppDbContext : IDisposable, IQueryBase
    {
        [SuppressMessage("Naming", "CA1716:Identifiers should not match keywords", Justification = "Same name as the one on the DbContext needed")]
        IDbSetProxy<TTable> DbSet<TTable>() where TTable : EfRecordBase;

        Task<int> SaveChangesAsync(CancellationToken token = default);
    }
}