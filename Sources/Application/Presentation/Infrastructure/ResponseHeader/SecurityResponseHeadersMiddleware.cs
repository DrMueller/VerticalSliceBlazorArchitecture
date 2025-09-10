using VerticalSliceBlazorArchitecture.Presentation.Infrastructure.ResponseHeader.Models;

namespace VerticalSliceBlazorArchitecture.Presentation.Infrastructure.ResponseHeader
{
    public class SecurityResponseHeadersMiddleware(RequestDelegate next)
    {
        public async Task InvokeAsync(HttpContext httpContext)
        {
            foreach (var header in SecurityResponseHeader.AllHeaders)
            {
                httpContext.Response.Headers.Append(header.Name, header.Value);
            }

            await next(httpContext);
        }
    }
}