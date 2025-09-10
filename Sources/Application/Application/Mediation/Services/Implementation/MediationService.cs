using JetBrains.Annotations;
using MediatR;
using VerticalSliceBlazorArchitecture.Application.Mediation.Models;

namespace VerticalSliceBlazorArchitecture.Application.Mediation.Services.Implementation
{
    [UsedImplicitly]
    internal class MediationService(IMediator mediator) : IMediationService
    {
        public async Task<T> SendAsync<T>(ICommand<T> command)
        {
            return await mediator.Send(command);
        }

        public async Task SendAsync(ICommand command)
        {
            await mediator.Send(command);
        }

        public async Task<T> SendAsync<T>(IQuery<T> query)
        {
            return await mediator.Send(query);
        }
    }
}