namespace VerticalSliceBlazorArchitecture.Application.Mediation.Models.Logging
{
    public interface IRequestLogParamsProvider
    {
        Task<string> ProvideAsync();
    }
}
