using MediatR.Pipeline;
using VerticalSliceBlazorArchitecture.Application.Features.Versioning.GetAssetsCacheVersion;
using VerticalSliceBlazorArchitecture.Common.Logging.Services;

namespace VerticalSliceBlazorArchitecture.Application.Mediation.Services.Handlers
{
    public class LogPreProcessor<TRequest>(ILoggingService loggingService) : IRequestPreProcessor<TRequest>
        where TRequest : notnull
    {
        public Task Process(TRequest request, CancellationToken cancellationToken)
        {
            var requestType = request.GetType();

            if (requestType == typeof(GetAssetsCacheVersionQuery))
            {
                return Task.CompletedTask;
            }

            var msg = requestType.Name;
            loggingService.TrackEvent(msg);

            return Task.CompletedTask;
        }
    }
}