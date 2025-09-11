using MediatR.Pipeline;
using VerticalSliceBlazorArchitecture.Application.Mediation.Models.Logging;
using VerticalSliceBlazorArchitecture.Common.Logging.Services;

namespace VerticalSliceBlazorArchitecture.Application.Mediation.Services.Handlers
{
    public class LogPreProcessor<TRequest>(ILoggingService loggingService) : IRequestPreProcessor<TRequest>
        where TRequest : notnull
    {
        public async Task Process(TRequest request, CancellationToken cancellationToken)
        {
            if (request is INotLoggedRequest)
            {
                return;
            }

            var logMessage = await GetLogMessageAsync(request);

            loggingService.TrackEvent(logMessage);
        }

        private static async Task<string> GetLogMessageAsync(TRequest request)
        {
            var msg = request.GetType().Name;

            if (request is not IRequestLogParamsProvider paramsProvider)
            {
                return msg;
            }

            var requestParams = await paramsProvider.ProvideAsync();
            msg += $" | Params: {requestParams}";

            return msg;
        }
    }
}