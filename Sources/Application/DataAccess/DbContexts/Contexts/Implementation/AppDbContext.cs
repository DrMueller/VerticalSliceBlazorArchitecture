using Microsoft.EntityFrameworkCore;
using VerticalSliceBlazorArchitecture.Data.Base;

namespace VerticalSliceBlazorArchitecture.DataAccess.DbContexts.Contexts.Implementation
{
    public class AppDbContext(DbContextOptions options) : DbContext(options), IAppDbContext
    {
        public IDbSetProxy<TTable> DbSet<TTable>() where TTable : EfRecordBase
        {
            var set = Set<TTable>();

            return new DbSetProxy<TTable>(set);
        }

        public IQueryable<TTable> Query<TTable>() where TTable : EfRecordBase
        {
            return Set<TTable>().AsQueryable();
        }

        // Only called if the models are not configured
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }
    }
}