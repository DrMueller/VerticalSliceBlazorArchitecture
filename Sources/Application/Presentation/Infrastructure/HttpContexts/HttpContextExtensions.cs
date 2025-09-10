using Microsoft.Identity.Web;

namespace VerticalSliceBlazorArchitecture.Presentation.Infrastructure.HttpContexts
{
    public static class HttpContextExtensions
    {
        public static bool CheckHasIdpIdentifier(this HttpContext httpContext)
        {
            return httpContext.User.Claims.Any(f => f.Type == ClaimConstants.ObjectId);
        }

        public static string GetIdpIdentifier(this HttpContext httpContext)
        {
            return httpContext.User.Claims.Single(f => f.Type == ClaimConstants.ObjectId).Value;
        }
    }
}