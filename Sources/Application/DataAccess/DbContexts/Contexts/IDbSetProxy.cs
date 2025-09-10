using JetBrains.Annotations;

namespace VerticalSliceBlazorArchitecture.DataAccess.DbContexts.Contexts
{
    [PublicAPI]
    public interface IDbSetProxy<TEntity>
        where TEntity : class
    {
        Task AddAsync(TEntity entity);

        IQueryable<TEntity> AsQueryable();

        void Remove(TEntity entity);
    }
}