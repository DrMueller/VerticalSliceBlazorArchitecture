using JetBrains.Annotations;
using VerticalSliceBlazorArchitecture.Common.LanguageExtensions.Invariance;

namespace VerticalSliceBlazorArchitecture.Presentation.Infrastructure.ExceptionHandling
{
    [PublicAPI]
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseGlobalExceptionHandler(this IApplicationBuilder app)
        {
            Guard.ObjectNotNull(() => app);
            app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

            return app;
        }
    }
}