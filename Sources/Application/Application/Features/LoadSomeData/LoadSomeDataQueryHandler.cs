using MediatR;
using VerticalSliceBlazorArchitecture.Application.Context.Services;
using VerticalSliceBlazorArchitecture.Application.Mediation.Models;
using VerticalSliceBlazorArchitecture.Application.Mediation.Models.Logging;

namespace VerticalSliceBlazorArchitecture.Application.Features.LoadSomeData
{
    public record LoadSomeDataQuery : IQuery<string>, IRequestLogParamsProvider
    {
        public Task<string> ProvideAsync()
        {
            return Task.FromResult("Custom Data");
        }
    }

    public class LoadSomeDataQueryHandler(IBenutzerContextState benutzerContextState) : IRequestHandler<LoadSomeDataQuery, string>
    {
        public async Task<string> Handle(LoadSomeDataQuery request, CancellationToken cancellationToken)
        {
            var benutzer = await benutzerContextState.GetBenutzerAsync();

            return benutzer.EmailAddress;
        }
    }
}