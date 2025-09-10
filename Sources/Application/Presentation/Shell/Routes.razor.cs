using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Threading.Tasks;
using VerticalSliceBlazorArchitecture.Application.Context.Services;
using VerticalSliceBlazorArchitecture.Common.Logging.Services;
using VerticalSliceBlazorArchitecture.Presentation.Areas.Home;
using VerticalSliceBlazorArchitecture.Presentation.Infrastructure.Navigation.Services;
using VerticalSliceBlazorArchitecture.Presentation.Infrastructure.Security.Exceptions;
using VerticalSliceBlazorArchitecture.Presentation.Shell.ExceptionHandling;

namespace VerticalSliceBlazorArchitecture.Presentation.Shell
{
    public partial class Routes
    {
        [Inject]
        public ILoggingService LoggingService { get; set; } = null!;

        [Inject]
        public required IHttpContextAccessor HttpContextAccessor { get; set; }

        [Inject]
        public INavigator Navigator { get; set; } = null!;

        [Inject]
        public required IBenutzerContextState BenutzerContextState { get; set; }

        private AppError? AppError { get; set; }

        private AppErrorBoundary? ErrorBoundary { get; set; }

        protected override void OnParametersSet()
        {
            ErrorBoundary?.Recover();
        }

        private async Task HandleExceptionThrownAsync(Exception exception)
        {
            if (exception is JSDisconnectedException)
            {
                return;
            }

            if (exception is UserAuthenticationException)
            {
                await BenutzerContextState.ClearAsync();

                Navigator.NavigateTo("/MicrosoftIdentity/Account/SignOut", forceLoad: true);

                return;
            }

            if (exception is JSException || exception is ObjectDisposedException)
            {
                LoggingService.LogWarning(exception.Message);

                return;
            }

            AppError = new AppError(exception.GetType().Name, exception.Message, exception.StackTrace!);
        }
    }
}