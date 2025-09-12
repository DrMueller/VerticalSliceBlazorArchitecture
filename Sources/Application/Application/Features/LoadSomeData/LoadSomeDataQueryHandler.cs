using FluentValidation;
using JetBrains.Annotations;
using MediatR;
using VerticalSliceBlazorArchitecture.Application.Context.Services;
using VerticalSliceBlazorArchitecture.Application.Mediation.Models;
using VerticalSliceBlazorArchitecture.Application.Mediation.Models.Logging;
using VerticalSliceBlazorArchitecture.Common.InformationHandling;
using VerticalSliceBlazorArchitecture.Common.LanguageExtensions.Types.Eithers;

namespace VerticalSliceBlazorArchitecture.Application.Features.LoadSomeData
{
    [UsedImplicitly]
    public class LoadSomeDataQueryValidator : AbstractValidator<LoadSomeDataQuery>
    {
        public LoadSomeDataQueryValidator()
        {
            RuleFor(x => x.Text).NotEmpty().NotNull();
        }
    }

    public record LoadSomeDataQuery(string Text) : IQuery<Either<InformationEntries, string>>, IRequestLogParamsProvider
    {
        public Task<string> ProvideAsync()
        {
            return Task.FromResult("Custom Data");
        }
    }

    public class LoadSomeDataQueryHandler(IBenutzerContextState benutzerContextState) : IRequestHandler<LoadSomeDataQuery, Either<InformationEntries, string>>
    {
        public async Task<Either<InformationEntries, string>> Handle(LoadSomeDataQuery request, CancellationToken cancellationToken)
        {
            var benutzer = await benutzerContextState.GetBenutzerAsync();

            return benutzer.EmailAddress;
        }
    }
}