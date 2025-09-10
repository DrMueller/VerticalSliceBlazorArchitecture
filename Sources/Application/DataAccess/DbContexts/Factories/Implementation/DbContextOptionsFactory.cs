using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace VerticalSliceBlazorArchitecture.DataAccess.DbContexts.Factories.Implementation
{
    [UsedImplicitly]
    public class DbContextOptionsFactory : IDbContextOptionsFactory
    {
        public DbContextOptions CreateForSqlite(string connectionString)
        {
            var conventions = SqliteConventionSetBuilder.Build();
            var mb = new ModelBuilder(conventions);

            // apply IEntityTypeConfiguration<T> from this assembly
            mb.ApplyConfigurationsFromAssembly(typeof(AppDbContextFactory).Assembly);

            return new DbContextOptionsBuilder()
                .UseSqlite(connectionString)
                .LogTo(Console.WriteLine, LogLevel.Information)
                .EnableSensitiveDataLogging()
                .AddInterceptors(new CommandInterceptor()) // your DbCommandInterceptor
                .UseModel(mb.FinalizeModel())
                .Options;
        }
    }
}