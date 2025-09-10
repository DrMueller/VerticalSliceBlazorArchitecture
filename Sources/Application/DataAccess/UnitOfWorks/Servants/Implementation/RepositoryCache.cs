using System.Collections.Concurrent;
using JetBrains.Annotations;
using Lamar;
using VerticalSliceBlazorArchitecture.Common.LanguageExtensions.Types.Maybes;
using VerticalSliceBlazorArchitecture.DataAccess.DbContexts.Contexts;
using VerticalSliceBlazorArchitecture.DataAccess.Repositories.Base;
using VerticalSliceBlazorArchitecture.Domain.Infrastructure.Data.Writing;

namespace VerticalSliceBlazorArchitecture.DataAccess.UnitOfWorks.Servants.Implementation
{
    [UsedImplicitly]
    public class RepositoryCache(IContainer container) : IRepositoryCache
    {
        private readonly ConcurrentDictionary<Type, IRepository> _repos = new();

        public TRepo GetRepository<TRepo>(IAppDbContext dbContext)
            where TRepo : IRepository
        {
            var getRepoResult = TryGettingRepository<TRepo>();
            var repo = getRepoResult.Reduce(() => InitializeRepository<TRepo>(dbContext));

            return repo;
        }

        private TRepo InitializeRepository<TRepo>(IAppDbContext dbContext)
            where TRepo : IRepository
        {
            var repository = container.GetInstance<TRepo>();

            // ReSharper disable once SuspiciousTypeConversion.Global
            if (!(repository is IRepositoryBase repoBase))
            {
                throw new ArgumentException($"{nameof(TRepo)} does not implement RepositoryBase");
            }

            repoBase.Initialize(dbContext);
            _repos.AddOrUpdate(repository.GetType(), repository, (_, repo) => repo);

            return repository;
        }

        private Maybe<TRepo> TryGettingRepository<TRepo>()
            where TRepo : IRepository
        {
            var repoType = typeof(TRepo);

            // For some reason, TryGetValue doesn't work here
            var cachedRepo = _repos.SingleOrDefault(f => repoType.IsAssignableFrom(f.Key));
            var castedRepo = (TRepo)cachedRepo.Value;

            return MaybeFactory.CreateFromNullable(castedRepo);
        }
    }
}