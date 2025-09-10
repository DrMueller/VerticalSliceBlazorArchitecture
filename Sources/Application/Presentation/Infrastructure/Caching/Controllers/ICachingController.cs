namespace VerticalSliceBlazorArchitecture.Presentation.Infrastructure.Caching.Controllers
{
    public interface ICachingController
    {
        Task<string> LoadCachingSuffixAsync();
    }
}