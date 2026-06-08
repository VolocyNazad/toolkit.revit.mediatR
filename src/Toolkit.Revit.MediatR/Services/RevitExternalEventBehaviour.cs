
using MediatR;
using Microsoft.Extensions.Logging;
using Toolkit.Revit.MediatR.Abstractions.Models;

namespace Toolkit.Revit.MediatR.Services;

public class RevitExternalEventBehaviour<TRequest, TResponse>(
        ILogger<RevitExternalEventBehaviour<TRequest, TResponse>> logger) 
    : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull, IExternalEventCommand
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellation)
    {
        return await RevitTask.RunAsync(async () =>
        {
            logger.LogTrace("External event for document '{revit.document.title}'. Starting...", request.Document?.Title);

            TResponse response = await next(cancellation).ConfigureAwait(false);

            logger.LogTrace("External event for document '{revit.document.title}'. Finished.", request.Document?.Title);

            return response;
        });
    }
}
