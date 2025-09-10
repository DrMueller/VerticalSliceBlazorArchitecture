using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using VerticalSliceBlazorArchitecture.DataAccess.DbContexts.Contexts;
using VerticalSliceBlazorArchitecture.DataAccess.DbContexts.Contexts.Implementation;
using VerticalSliceBlazorArchitecture.DataAccess.DbContexts.Factories;
using VerticalSliceBlazorArchitecture.DataAccess.DbContexts.Factories.Implementation;

namespace VerticalSliceBlazorArchitecture.Testing.Common.Data
{
    [UsedImplicitly]
    public class TestAppDbContextFactory : IAppDbContextFactory
    {
        private readonly Lazy<IModel> _lazyModel = new(CreateModel);
        private string _databaseName = null!;

        public IAppDbContext Create()
        {
            var finalOptions = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(_databaseName)
                .UseModel(_lazyModel.Value)
                .Options;

            var context = new AppDbContext(finalOptions);
            context.Database.EnsureCreated();

            return context;
        }

        public void InitializeName()
        {
            _databaseName = Guid.NewGuid().ToString();
        }

        private static IModel CreateModel()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase("Tra")
                .Options;

#pragma warning disable IDISP004
            var conventionSet = ConventionSet.CreateConventionSet(new AppDbContext(options));
#pragma warning restore IDISP004
            var mb = new ModelBuilder(conventionSet);

            mb.ApplyConfigurationsFromAssembly(typeof(AppDbContextFactory).Assembly);

            return mb.FinalizeModel();
        }
    }
}