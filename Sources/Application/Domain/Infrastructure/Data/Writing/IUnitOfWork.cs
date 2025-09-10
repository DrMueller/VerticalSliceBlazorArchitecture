using JetBrains.Annotations;

namespace VerticalSliceBlazorArchitecture.Domain.Infrastructure.Data.Writing
{
    [PublicAPI]
    public interface IUnitOfWork : IDisposable
    {
        Task CommitAsync();

        TRepo GetRepository<TRepo>()
            where TRepo : IRepository;
    }
}