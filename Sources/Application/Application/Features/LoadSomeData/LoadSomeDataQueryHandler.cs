using MediatR;
using VerticalSliceBlazorArchitecture.Application.Context.Services;
using VerticalSliceBlazorArchitecture.Application.Mediation.Models;

namespace VerticalSliceBlazorArchitecture.Application.Features.LoadSomeData
{
    public record LoadSomeData : IQuery<string>;

    public class LoadSomeDataQueryHandler(IBenutzerContextState benutzerContextState) : IRequestHandler<LoadSomeData, string>
    {
        public async Task<string> Handle(LoadSomeData request, CancellationToken cancellationToken)
        {
            var benutzer = await benutzerContextState.GetBenutzerAsync();

            return benutzer.EmailAddress;
        }
    }
}