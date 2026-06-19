using MediatR;
using Microsoft.Extensions.Logging;
using Revit.Events.Abstractions.Services;
using Revit.MediatR.Abstractions.Models;

namespace Revit.MediatR.Services;

public class RevitExternalEventBehaviour<TRequest, TResponse>(
        IAsyncExternalEvent externalEvent,
        ILogger<RevitExternalEventBehaviour<TRequest, TResponse>> logger) 
    : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull, IExternalEventCommand
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellation)
    {
        TResponse? response = default;
        await externalEvent.Raise(async (uiapp) =>
        {
            logger.LogTrace("External event started. ");

            response = await next(cancellation).ConfigureAwait(false);

            logger.LogTrace("External event finished. ");

        });
        return response;
    }
}
