using VerticalSliceBlazorArchitecture.Application.Mediation.Models;
using VerticalSliceBlazorArchitecture.Application.Mediation.Models.Logging;

namespace VerticalSliceBlazorArchitecture.Application.Features.Versioning.GetAssetsCacheVersion
{
    public class GetAssetsCacheVersionQuery : IQuery<string>, INotLoggedRequest;
}