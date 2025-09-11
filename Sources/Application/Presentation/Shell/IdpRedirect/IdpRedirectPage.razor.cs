using System.Security.Claims;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Components;
using VerticalSliceBlazorArchitecture.Application.Context.Models;
using VerticalSliceBlazorArchitecture.Application.Context.Services;
using VerticalSliceBlazorArchitecture.Presentation.Areas.Home;
using VerticalSliceBlazorArchitecture.Presentation.Infrastructure.HttpContexts;
using VerticalSliceBlazorArchitecture.Presentation.Infrastructure.Navigation.Services;

namespace VerticalSliceBlazorArchitecture.Presentation.Shell.IdpRedirect
{
    [UsedImplicitly]
    public partial class IdpRedirectPage
    {
        public const string Path = "/idpredirect";

        [Inject]
        public required IBenutzerContextState Context { get; set; }

        [Inject]
        public required IHttpContextAccessor HttpContextAccessor { get; set; }

        [Inject]
        public required INavigator Navigator { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var httpContext = HttpContextAccessor.HttpContext;

            if (httpContext == null || !httpContext.CheckHasIdpIdentifier())
            {
                // TODO What to do here? Show error message?
                return;
            }

            var user = httpContext.User;
            var name = user.Identity?.Name ?? user.FindFirst("name")?.Value;
            var email = user.FindFirst("preferred_username")?.Value
                        ?? user.FindFirst(ClaimTypes.Email)?.Value;

            var benutzerContext = new BenutzerContext
            {
                IdpId = httpContext.GetIdpIdentifier(),
                Name = name ?? string.Empty,
                EmailAddress = email ?? string.Empty
            };

            await Context.SetAsync(benutzerContext);

            Navigator.NavigateTo(HomePage.Path);
        }
    }
}