using JetBrains.Annotations;
using VerticalSliceBlazorArchitecture.DataAccess.DbContexts.Factories;
using VerticalSliceBlazorArchitecture.Domain.Infrastructure.Data.Writing;

namespace VerticalSliceBlazorArchitecture.DataAccess.UnitOfWorks.Implementation
{
    [UsedImplicitly]
    internal class UnitOfWorkFactory(
        Func<IUnitOfWork> unitOfWorkFactory,
        IAppDbContextFactory dbContextFactory)
        : IUnitOfWorkFactory
    {
        public IUnitOfWork Create()
        {
            var dbContext = dbContextFactory.Create();
            var unitOfWork = unitOfWorkFactory();
            ((UnitOfWork)unitOfWork).Initialize(dbContext);

            return unitOfWork;
        }
    }
}