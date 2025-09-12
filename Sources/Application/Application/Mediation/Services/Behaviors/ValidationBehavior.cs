using FluentValidation;
using MediatR;
using VerticalSliceBlazorArchitecture.Common.InformationHandling;
using VerticalSliceBlazorArchitecture.Common.LanguageExtensions.Types.Eithers;

namespace VerticalSliceBlazorArchitecture.Application.Mediation.Services.Behaviors
{
    public class ValidationBehavior<TRequest, TValue>(IEnumerable<IValidator<TRequest>> validators) : IPipelineBehavior<TRequest, Either<InformationEntries, TValue>>
        where TRequest : IRequest<Either<InformationEntries, TValue>>
    {
        public async Task<Either<InformationEntries, TValue>> Handle(
            TRequest request,
            RequestHandlerDelegate<Either<InformationEntries, TValue>> next,
            CancellationToken cancellationToken)
        {
            var context = new ValidationContext<TRequest>(request);

            var failures = validators
                .Select(v => v.Validate(context))
                .SelectMany(r => r.Errors)
                .Where(f => f != null)
                .ToList();

            if (failures.Any())
            {
                return failures.ToInformationEntries();
            }

            return await next(cancellationToken);
        }
    }
}