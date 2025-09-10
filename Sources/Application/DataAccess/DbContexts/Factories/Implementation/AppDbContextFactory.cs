using Microsoft.EntityFrameworkCore;
using VerticalSliceBlazorArchitecture.Common.Settings.Provisioning.Services;
using VerticalSliceBlazorArchitecture.DataAccess.DbContexts.Contexts;
using VerticalSliceBlazorArchitecture.DataAccess.DbContexts.Contexts.Implementation;

namespace VerticalSliceBlazorArchitecture.DataAccess.DbContexts.Factories.Implementation
{
    public class AppDbContextFactory(
        IDbContextOptionsFactory optionsFactory,
        ISettingsProvider appSettingsProvider)
        : IAppDbContextFactory
    {
        private readonly Lazy<DbContextOptions> _lazyOptions = new(() => optionsFactory
            .CreateForSqlite(appSettingsProvider.AppSettings.ConnectionString)
        );

        public IAppDbContext Create()
        {
            return new AppDbContext(_lazyOptions.Value);
        }
    }
}