using AngleSharp.Dom;
using VerticalSliceBlazorArchitecture.Common.LanguageExtensions.Types.Maybes;
using VerticalSliceBlazorArchitecture.Common.LanguageExtensions.Types.Maybes.Implementation;
using VerticalSliceBlazorArchitecture.QualityTests.TestingInfrastructure.BlazorMetadata.Models.Components;
using VerticalSliceBlazorArchitecture.QualityTests.TestingInfrastructure.BlazorMetadata.Models.Forms;
using Xunit;

namespace VerticalSliceBlazorArchitecture.QualityTests.TestingInfrastructure.BlazorMetadata.Services.Servants
{
    public static class BlazorFormFactory
    {
        private static readonly IReadOnlyCollection<string> _inputElements = new List<string>
        {
            "inputtext",
            "inputnumber",
            "inputdate",
            "inputselect",
            "inputcheckbox"
        };

        public static IReadOnlyCollection<BlazorForm> CreateAll(BlazorComponentMarkup markup)
        {
            using var parsed = markup.Parse();

            return parsed
                .GetElementsByTagName("EditForm")
                .Select(f => CreateForm(f, markup.FileNamespace))
                .ToList();
        }

        private static BlazorForm CreateForm(IElement form, string fileName)
        {
            var elements = form.GetElementsByClassName("mb-3")
                .Select(f => CreateFormElement(f, fileName))
                .Where(f => f != null)
                .ToList();

            return new BlazorForm(
                form,
                elements!);
        }

        private static BlazorFormElement? CreateFormElement(IElement formElement, string fileName)
        {
            var label = CreateFormLabel(formElement, fileName);
            var input = CreateFormInput(formElement, fileName);
            var validator = TryCreatingFormInputValidationMessage(formElement, fileName);

            if (label == null || input == null)
            {
                return null;
            }

            return new BlazorFormElement(input, label, validator);
        }

        private static BlazorFormInput? CreateFormInput(IElement formElement, string fileName)
        {
            var inputs = formElement
                .Descendants()
                .OfType<IElement>()
                .Where(e => _inputElements.Contains(e.TagName.ToLower()))
                .ToList();

            if (inputs.Count > 1)
            {
                Assert.Fail($"Strange form at {fileName}");
            }

            if (inputs.Count == 0)
            {
                return null;
            }

            var input = inputs.Single();
            var bindValueAttr = input.Attributes["@bind-value"];

            return new BlazorFormInput(input.TagName.ToLower(), input.Id, bindValueAttr?.Value);
        }

        private static BlazorFormLabel? CreateFormLabel(
            IElement formElement,
            string fileName)
        {
            var labels = formElement.GetElementsByTagName("label")
                .ToList();

            if (labels.Count > 1)
            {
                Assert.Fail($"Strange labelsform at {fileName}");
            }

            if (labels.Count == 0)
            {
                return null;
            }

            var label = labels.Single();
            var forAttr = label.GetAttribute("for");

            return new BlazorFormLabel(forAttr, label.ClassName);
        }

        private static Maybe<BlazorFormInputValidationMessage> TryCreatingFormInputValidationMessage(IElement formElement, string fileName)
        {
            var validations = formElement
                .Descendants()
                .OfType<IElement>()
                .Where(e => e.TagName.ToLower() == "validationmessage")
                .ToList();

            if (validations.Count > 1)
            {
                Assert.Fail($"Strange form at {fileName}");
            }

            if (!validations.Any())
            {
                return None.Value;
            }

            var validation = validations.Single();
            var forAttr = validation.Attributes["for"];

            if (forAttr == null)
            {
                Assert.Fail($"ValidationMessage required for attribute at {fileName}");
            }

            return new BlazorFormInputValidationMessage(forAttr.Value);
        }
    }
}