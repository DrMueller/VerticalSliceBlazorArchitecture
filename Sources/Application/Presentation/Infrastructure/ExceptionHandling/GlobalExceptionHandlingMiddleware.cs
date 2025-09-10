using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using VerticalSliceBlazorArchitecture.Common.Logging.Services;

namespace VerticalSliceBlazorArchitecture.Presentation.Infrastructure.ExceptionHandling
{
    [PublicAPI]
    [SuppressMessage("Style", "VSTHRD200:Use \"Async\" suffix for async methods", Justification = "Microsoft Interface")]
    public class GlobalExceptionHandlingMiddleware(RequestDelegate next, ILoggingService loggingService)
    {
        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await next(httpContext);
            }
            catch (Exception exception)
            {
                loggingService.LogException(exception);

                throw;
            }
        }
    }
}