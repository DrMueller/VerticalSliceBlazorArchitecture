using VerticalSliceBlazorArchitecture.Application.Features.Versioning.GetAssetsCacheVersion;
using VerticalSliceBlazorArchitecture.Application.Mediation.Services;

namespace VerticalSliceBlazorArchitecture.Presentation.Infrastructure.Caching.Controllers.Implementation
{
    public class CachingController(IMediationService mediator) : ICachingController
    {
        internal const string SuffixTemplate = "?v={0}";

        public async Task<string> LoadCachingSuffixAsync()
        {
            var version = await mediator.SendAsync(new GetAssetsCacheVersionQuery());
            var result = string.Format(SuffixTemplate, version);

            return result;
        }
    }
}