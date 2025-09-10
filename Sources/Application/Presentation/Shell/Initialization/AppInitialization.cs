using VerticalSliceBlazorArchitecture.Presentation.Infrastructure.ExceptionHandling;
using VerticalSliceBlazorArchitecture.Presentation.Infrastructure.ResponseHeader;

namespace VerticalSliceBlazorArchitecture.Presentation.Shell.Initialization
{
    public static class AppInitialization
    {
        public static void Initialize(WebApplication app)
        {
            app.UseMiddleware<SecurityResponseHeadersMiddleware>();
            app.UseGlobalExceptionHandler();
            app.UseHttpsRedirection();
            app.UseAntiforgery();
            app.MapStaticAssets();
            app.MapControllers();
            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();
        }
    }
}