using Bunit;
using FluentAssertions;
using Moq;
using VerticalSliceBlazorArchitecture.Presentation.Infrastructure.JavaScript.Services;
using VerticalSliceBlazorArchitecture.Presentation.Infrastructure.Storage.Implementation;
using VerticalSliceBlazorArchitecture.Testing.Common.Infrastructure;
using Xunit;

namespace VerticalSliceBlazorArchitecture.UnitTests.Presentation.Infrastructure.Storage
{
    public class LocalStorageProxyUnitTests : TestContext
    {
        private readonly LocalStorageProxy _sut;
        private readonly BunitJSModuleInterop _moduleInterop;

        public LocalStorageProxyUnitTests()
        {
            var jsLocatorMock = new Mock<IJavaScriptLocator>();
            jsLocatorMock
                .Setup(f => f.LocateAbsoluteJsFilePathAsync(LocalStorageProxy.JavaScriptStoragePath))
                .ReturnsAsync(LocalStorageProxy.JavaScriptStoragePath);

            _moduleInterop = JSInterop.SetupModule(LocalStorageProxy.JavaScriptStoragePath);

            _sut = new LocalStorageProxy(jsLocatorMock.Object, JSInterop.JSRuntime);
        }

        [Fact]
        public async Task GettingBool_ItemExisting_ReturnsBool()
        {
            // Arrange
            const string ItemKey = "Test12";
            const bool ExpectedBool = true;
            _moduleInterop.Setup<string>(LocalStorageProxy.GetItemMethod, ItemKey).SetResult(ExpectedBool.ToString());

            // Act
            var actualBoolMaybe = await _sut.GetBoolAsync(ItemKey);

            // Assert
            actualBoolMaybe.ShouldBeSome();
            actualBoolMaybe.ShouldBeSome().Should().Be(ExpectedBool);
        }

        [Fact]
        public async Task GettingBool_ItemNotExisting_ReturnsNone()
        {
            // Arrange
            const string ItemKey = "Test67";
            _moduleInterop.Setup<string?>(LocalStorageProxy.GetItemMethod, ItemKey).SetResult(null);

            // Act
            var actualBoolMaybe = await _sut.GetBoolAsync(ItemKey);

            // Assert
            actualBoolMaybe.ShouldBeNone();
        }

        [Fact]
        public async Task SettingItem_SetsItem()
        {
            // Arrange
            const string ItemKey = "Test56";
            const string ItemValue = "Value1";

            _moduleInterop.SetupVoid(LocalStorageProxy.SetItemMethod, ItemKey, ItemValue).SetVoidResult();

            // Act
            await _sut.SetItemAsync(ItemKey, ItemValue);

            // Assert
            _moduleInterop.VerifyInvoke(LocalStorageProxy.SetItemMethod, 1);
            var actualInvocation = _moduleInterop.Invocations.Single(f => f.Identifier == LocalStorageProxy.SetItemMethod);
            actualInvocation.Arguments[0].Should().Be(ItemKey);
            actualInvocation.Arguments[1].Should().Be(ItemValue);
        }
    }
}