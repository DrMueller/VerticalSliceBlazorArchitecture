using Microsoft.EntityFrameworkCore;

namespace VerticalSliceBlazorArchitecture.DataAccess.DbContexts.Factories
{
    public interface IDbContextOptionsFactory
    {
        DbContextOptions CreateForSqlite(string connectionString);
    }
}