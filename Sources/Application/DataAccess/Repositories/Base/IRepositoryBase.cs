using VerticalSliceBlazorArchitecture.DataAccess.DbContexts.Contexts;

namespace VerticalSliceBlazorArchitecture.DataAccess.Repositories.Base
{
    public interface IRepositoryBase
    {
        internal void Initialize(IAppDbContext dbContext);
    }
}