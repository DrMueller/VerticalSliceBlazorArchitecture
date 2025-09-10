using VerticalSliceBlazorArchitecture.DataAccess.DbContexts.Contexts;

namespace VerticalSliceBlazorArchitecture.DataAccess.DbContexts.Factories
{
    public interface IAppDbContextFactory
    {
        IAppDbContext Create();
    }
}