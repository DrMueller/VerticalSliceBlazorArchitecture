using JetBrains.Annotations;
using Lamar.Microsoft.DependencyInjection;
using VerticalSliceBlazorArchitecture.Common.Settings.Config.Services;
using VerticalSliceBlazorArchitecture.Presentation.Shell.Initialization;

namespace VerticalSliceBlazorArchitecture
{
    [UsedImplicitly]
    public class Program
    {
        public static IConfiguration Configuration { get; } = ConfigurationFactory.Create(typeof(Program).Assembly);

        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Host.UseLamar(serviceRegistry =>
            {
                serviceRegistry.Scan(scanner =>
                {
                    scanner.AssembliesFromApplicationBaseDirectory();
                    scanner.LookForRegistries();
                });
            });

            ServiceInitialization.Initialize(builder.Services, builder.Environment);
            var app = builder.Build();
            AppInitialization.Initialize(app);

            app.Run();
        }
    }
}