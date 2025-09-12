using FluentValidation.Results;
using VerticalSliceBlazorArchitecture.Common.InformationHandling;

namespace VerticalSliceBlazorArchitecture.Application.Mediation.Services.Behaviors
{
    public static class InformationEntriesValidationAdapter
    {
        public static InformationEntries ToInformationEntries(this IReadOnlyCollection<ValidationFailure> failures)
        {
            return failures.Aggregate(InformationEntries.CreateNew(), (infos, failure) => infos.AddError($"{failure.PropertyName}: {failure.ErrorMessage}"));
        }
    }
}