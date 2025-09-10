using System.Text.Json;
using JetBrains.Annotations;
using VerticalSliceBlazorArchitecture.Application.Context.Models;
using VerticalSliceBlazorArchitecture.Application.Context.Services;
using VerticalSliceBlazorArchitecture.Common.LanguageExtensions.Types.Maybes;
using VerticalSliceBlazorArchitecture.Common.LanguageExtensions.Types.Maybes.Implementation;
using VerticalSliceBlazorArchitecture.Presentation.Infrastructure.HttpContexts;
using VerticalSliceBlazorArchitecture.Presentation.Infrastructure.Security.Exceptions;
using VerticalSliceBlazorArchitecture.Presentation.Infrastructure.Storage;

namespace VerticalSliceBlazorArchitecture.Presentation.Infrastructure.Context.Services.Implementation
{
    [PublicAPI]
    public class BenutzerContextState(
        IHttpContextAccessor httpContextAccessor,
        IProtectedLocalStorageProxy store)
        : IBenutzerContextState
    {
        public const string Key = "Users";

        private bool IsIdentityAuthenticated
        {
            get
            {
                var identityExists = httpContextAccessor.HttpContext?.User.Identity;

                return identityExists is { IsAuthenticated: true };
            }
        }

        public async Task<bool> CheckBenutzerExistsAsync()
        {
            if (!IsIdentityAuthenticated)
            {
                return false;
            }

            Maybe<BenutzerContext> maybeContext = None.Value;

            try
            {
                maybeContext = await store.GetAsync<BenutzerContext>(Key);
            }
            catch (JsonException)
            {
                // Best effort: in case the BenutzerContext doesn't match the persisted one, we force a re-login to set the BenutzerContext
            }

            var existsMaybe = await maybeContext.MapAsync(async benutzer =>
            {
                var idpGuid = httpContextAccessor.HttpContext!.GetIdpIdentifier();

                // If the user logged in before using IDP, he doesn't have a guid saved. So we force a re-login.
                if (benutzer.IdpId == string.Empty && idpGuid != string.Empty)
                {
                    return false;
                }

                // If the user logged in with a differnet user in IDP, we force logout and re-login.
                if (benutzer.IdpId != idpGuid)
                {
                    await ClearAsync();

                    return false;
                }

                return true;
            });

            return existsMaybe.Reduce(() => false);
        }

        public async Task ClearAsync()
        {
            await store.DeleteAsync(Key);
        }

        public async Task<BenutzerContext> GetBenutzerAsync()
        {
            if (!IsIdentityAuthenticated)
            {
                throw new UserAuthenticationException();
            }

            var storeResult = await store.GetAsync<BenutzerContext>(Key);
            var benutzerContext = storeResult.Reduce(() => throw new UserAuthenticationException());

            var cookieGuid = httpContextAccessor.HttpContext!.GetIdpIdentifier();
            var storageGuid = benutzerContext.IdpId;

            if (cookieGuid != storageGuid)
            {
                throw new UserAuthenticationException();
            }

            return benutzerContext;
        }

        public async Task SetAsync(BenutzerContext benutzerContext)
        {
            await store.SetAsync(Key, benutzerContext);
        }
    }
}