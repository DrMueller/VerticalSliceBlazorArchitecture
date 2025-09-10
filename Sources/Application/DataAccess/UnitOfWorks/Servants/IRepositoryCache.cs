using VerticalSliceBlazorArchitecture.DataAccess.DbContexts.Contexts;
using VerticalSliceBlazorArchitecture.Domain.Infrastructure.Data.Writing;

namespace VerticalSliceBlazorArchitecture.DataAccess.UnitOfWorks.Servants
{
    public interface IRepositoryCache
    {
        TRepo GetRepository<TRepo>(IAppDbContext dbContext)
            where TRepo : IRepository;
    }
}