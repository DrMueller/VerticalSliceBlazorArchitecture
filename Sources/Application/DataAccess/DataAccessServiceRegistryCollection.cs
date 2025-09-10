using Lamar;
using VerticalSliceBlazorArchitecture.DataAccess.DbContexts.Factories;
using VerticalSliceBlazorArchitecture.DataAccess.DbContexts.Factories.Implementation;
using VerticalSliceBlazorArchitecture.DataAccess.UnitOfWorks.Implementation;
using VerticalSliceBlazorArchitecture.DataAccess.UnitOfWorks.Servants;
using VerticalSliceBlazorArchitecture.DataAccess.UnitOfWorks.Servants.Implementation;
using VerticalSliceBlazorArchitecture.Domain.Infrastructure.Data.Writing;

namespace VerticalSliceBlazorArchitecture.DataAccess
{
    public class DataAccessServiceRegistryCollection : ServiceRegistry
    {
        public DataAccessServiceRegistryCollection()
        {
            RegisterInfrastructure();
        }

        private void RegisterInfrastructure()
        {
            For<IUnitOfWork>().Use<UnitOfWork>().Transient();
            For<IUnitOfWorkFactory>().Use<UnitOfWorkFactory>().Scoped();
            For<IAppDbContextFactory>().Use<AppDbContextFactory>().Singleton();
            For<IDbContextOptionsFactory>().Use<DbContextOptionsFactory>().Singleton();
            For<IRepositoryCache>().Use<RepositoryCache>().Transient();
        }
    }
}