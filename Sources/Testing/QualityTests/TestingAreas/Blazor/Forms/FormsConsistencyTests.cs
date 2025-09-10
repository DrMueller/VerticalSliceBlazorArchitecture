using AngleSharp.Dom;
using FluentAssertions;
using VerticalSliceBlazorArchitecture.Presentation.Shared.Attributes;
using VerticalSliceBlazorArchitecture.QualityTests.TestingInfrastructure.BlazorMetadata.Services;
using Xunit;

namespace VerticalSliceBlazorArchitecture.QualityTests.TestingAreas.Blazor.Forms
{
    public class FormsConsistencyTests
    {
        [Fact]
        public void FormInputsAndValidationMessages_Match()
        {
            var components = BlazorComponentFactory.CreateAll();

            foreach (var component in components)
            {
                foreach (var form in component.Forms)
                {
                    foreach (var element in form.Elements)
                    {
                        element.AssertInputAndValidationMessageMatch(component.Name);
                    }
                }
            }
        }

        [Fact]
        public void FormLabelsAndInputs_Match()
        {
            var components = BlazorComponentFactory.CreateAll();

            foreach (var component in components)
            {
                foreach (var form in component.Forms)
                {
                    foreach (var element in form.Elements)
                    {
                        element.Input.Id.Should().NotBeNullOrEmpty(component.Name);
                        element.AssertClassMatches(component.Name);
                        element.AssertForIdMatches(component.Name);
                    }
                }
            }
        }

        [Fact]
        public void Forms_HaveValidiation()
        {
            var components = BlazorComponentFactory.CreateAll();
            var failingForms = new List<string>();

            foreach (var component in components)
            {
                foreach (var form in component.Forms)
                {
                    if (form.Validator == null)
                    {
                        failingForms.Add(Path.GetFileName(component.Name));
                    }
                }
            }

            failingForms.Should().BeEmpty();
        }

        [Fact]
        public void StringFormInputs_HaveSanitizeAttribute()
        {
            var propsWithoutSanitize = new List<string>();

            var viewModelTypes = BlazorViewModelFactory.LoadViewModelTypes();

            foreach (var vmType in viewModelTypes)
            {
                var allProps = vmType.GetProperties().ToList();

                foreach (var prop in allProps)
                {
                    var propAttributes = prop.GetCustomAttributes(false).ToList();

                    if (!propAttributes.Any(f => BlazorViewModelFactory.ViewModelPropertyAttributeTypes.Contains(f.GetType())))
                    {
                        continue;
                    }

                    var attrIsString = prop.PropertyType == typeof(string) ||
                                       (prop.PropertyType.IsGenericType &&
                                        prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>) &&
                                        prop.PropertyType.GenericTypeArguments[0] == typeof(string));

                    if (!attrIsString)
                    {
                        continue;
                    }

                    var containsSanitize = propAttributes.Any(attr => attr.GetType() == typeof(SanitizeAttribute));

                    if (!containsSanitize)
                    {
                        propsWithoutSanitize.Add($"{vmType.Name}.{prop.Name}");
                    }
                }
            }

            var allPropsStr = string.Join(Environment.NewLine, propsWithoutSanitize);
            allPropsStr.Should().BeEmpty();
        }

        [Fact]
        public void Validations_AreBeforeInformationEntries()
        {
            var components = BlazorComponentFactory.CreateAll();
            var failingFiles = new List<string>();

            foreach (var component in components)
            {
                foreach (var form in component.Forms)
                {
                    if (form.Validator == null || form.Informations == null)
                    {
                        continue;
                    }

                    var position = form.Validator.CompareDocumentPosition(form.Informations);
                    var following = (position & DocumentPositions.Following) == DocumentPositions.Following;

                    if (!following)
                    {
                        failingFiles.Add(component.Name);
                    }
                }
            }

            failingFiles.Should().BeEmpty();
        }
    }
}