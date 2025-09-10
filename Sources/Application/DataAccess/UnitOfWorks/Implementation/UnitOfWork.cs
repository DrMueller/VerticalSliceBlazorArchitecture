using JetBrains.Annotations;
using VerticalSliceBlazorArchitecture.DataAccess.DbContexts.Contexts;
using VerticalSliceBlazorArchitecture.DataAccess.UnitOfWorks.Servants;
using VerticalSliceBlazorArchitecture.Domain.Infrastructure.Data.Writing;

namespace VerticalSliceBlazorArchitecture.DataAccess.UnitOfWorks.Implementation
{
    [UsedImplicitly]
    public sealed class UnitOfWork(
        IRepositoryCache repoCache)
        : IUnitOfWork
    {
        private IAppDbContext _dbContext = null!;

        public async Task CommitAsync()
        {
            //DebugChanges();
            await _dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
#pragma warning disable IDISP007 // Don't dispose injected
            _dbContext.Dispose();
#pragma warning restore IDISP007 // Don't dispose injected
        }

        public TRepo GetRepository<TRepo>() where TRepo : IRepository
        {
            return repoCache.GetRepository<TRepo>(_dbContext);
        }

        public void Initialize(IAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        //Exemplary purpose to debug EF Core Changes
        //private void DebugChanges()
        //{
        //    _dbContext.ChangeTracker.DetectChanges();
        //    Debug.WriteLine(_dbContext.ChangeTracker.DebugView.LongView);
        //}
    }
}