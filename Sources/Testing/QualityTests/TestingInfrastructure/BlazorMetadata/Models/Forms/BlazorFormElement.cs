using FluentAssertions;
using VerticalSliceBlazorArchitecture.Common.LanguageExtensions.Types.Maybes;

namespace VerticalSliceBlazorArchitecture.QualityTests.TestingInfrastructure.BlazorMetadata.Models.Forms
{
    public class BlazorFormElement(BlazorFormInput input, BlazorFormLabel label, Maybe<BlazorFormInputValidationMessage> inputValidator)
    {
        public BlazorFormInput Input { get; } = input;
        private Maybe<BlazorFormInputValidationMessage> InputValidationMessage { get; } = inputValidator;
        private BlazorFormLabel Label { get; } = label;

        public void AssertClassMatches(string fileName)
        {
            Label.ClassName.Should().Be(Input.TagName.Equals("inputcheckbox", StringComparison.CurrentCultureIgnoreCase) ? "form-check-label" : "form-label", fileName);
        }

        public void AssertForIdMatches(string fileName)
        {
            Label.ForId.Should().NotBeNullOrEmpty(fileName);
            Label.ForId.Should().Be(Input.Id, fileName);
        }

        public void AssertInputAndValidationMessageMatch(string fileName)
        {
            InputValidationMessage.WhenSome(msg => { msg.TargetProperty.Should().Be(Input.BindingProperty, fileName); });
        }
    }
}