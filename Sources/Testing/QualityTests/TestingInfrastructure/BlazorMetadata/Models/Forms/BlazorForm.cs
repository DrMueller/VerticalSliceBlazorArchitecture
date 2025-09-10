using AngleSharp.Dom;
using VerticalSliceBlazorArchitecture.Common.LanguageExtensions.Invariance;

namespace VerticalSliceBlazorArchitecture.QualityTests.TestingInfrastructure.BlazorMetadata.Models.Forms
{
    public class BlazorForm
    {
        private readonly IElement _formElement;

        public IReadOnlyCollection<BlazorFormElement> Elements { get; }

        public IElement? Informations => _formElement.GetElementsByTagName("Informations").SingleOrDefault();

        public IElement? Validator => _formElement.GetElementsByTagName("DataAnnotationsValidator").SingleOrDefault();

        public BlazorForm(
            IElement formElement,
            IReadOnlyCollection<BlazorFormElement> elements)
        {
            Guard.ObjectNotNull(() => formElement);

            Elements = elements;
            _formElement = formElement;
        }
    }
}