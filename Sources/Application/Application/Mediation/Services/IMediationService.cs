using JetBrains.Annotations;
using VerticalSliceBlazorArchitecture.Application.Mediation.Models;

namespace VerticalSliceBlazorArchitecture.Application.Mediation.Services
{
    [PublicAPI]
    public interface IMediationService
    {
        Task<T> SendAsync<T>(ICommand<T> command);

        Task SendAsync(ICommand command);

        Task<T> SendAsync<T>(IQuery<T> query);
    }
}