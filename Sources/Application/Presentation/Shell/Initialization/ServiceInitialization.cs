using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.UI;
using MudBlazor.Services;
using VerticalSliceBlazorArchitecture.Common.Settings.Provisioning.Models;

namespace VerticalSliceBlazorArchitecture.Presentation.Shell.Initialization
{
    public static class ServiceInitialization
    {
        public static void Initialize(IServiceCollection services, IWebHostEnvironment hostingEnv)
        {
            services.AddControllers();
            services.AddMudServices();
            services.Configure<AppSettings>(Program.Configuration.GetSection(AppSettings.SectionKey));

            services.AddRazorComponents()
                .AddInteractiveServerComponents();

            services.AddCors();
            services.AddAntiforgery();

            services.AddHsts(options =>
            {
                options.IncludeSubDomains = true;
                options.MaxAge = TimeSpan.FromDays(730);
            });

            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = _ => true;
                options.MinimumSameSitePolicy = SameSiteMode.Unspecified;
            });

            services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
                .AddMicrosoftIdentityWebApp(Program.Configuration.GetSection("AzureAd"));

            services.AddAuthorization(options => { options.FallbackPolicy = options.DefaultPolicy; });

            services.AddControllersWithViews()
                .AddMicrosoftIdentityUI();

            services.AddApplicationInsightsTelemetry(Program.Configuration);

            services.AddDataProtection()
                .SetApplicationName(Common.Constants.AppDescription + hostingEnv.EnvironmentName)
                .SetDefaultKeyLifetime(TimeSpan.FromDays(90))
                .PersistKeysToFileSystem(new DirectoryInfo("/App_Keys/"));

            services.Configure<OpenIdConnectOptions>(OpenIdConnectDefaults.AuthenticationScheme, options =>
            {
                var prev = options.Events.OnTokenValidated; // preserve Microsoft.Identity.Web handlers
                options.Events.OnTokenValidated = async ctx =>
                {
                    await prev(ctx);
                    ctx.Properties!.RedirectUri = IdpRedirect.IdpRedirectPage.Path; // final redirect after sign-in
                };;
            });
        }
    }
}