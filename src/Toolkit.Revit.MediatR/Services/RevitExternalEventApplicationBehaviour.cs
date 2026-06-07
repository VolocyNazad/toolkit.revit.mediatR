
using MediatR;
using Microsoft.Extensions.Logging;
using Toolkit.Revit.MediatR.Abstractions.Models;

namespace Toolkit.Revit.MediatR.Services;

public class RevitExternalEventApplicationBehaviour<TRequest, TResponse>(ILogger<RevitExternalEventApplicationBehaviour<TRequest, TResponse>> logger) : IPipelineBehavior<TRequest, TResponse>
   where TRequest : notnull, IExternalEventApplicationCommand
{
    private readonly ILogger<RevitExternalEventApplicationBehaviour<TRequest, TResponse>> _logger = logger;

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellation)
    {
        return await RevitTask.RunAsync(async () =>
        {
            _logger.LogTrace("External event stared. ");

            TResponse response = await next(cancellation).ConfigureAwait(false);

            _logger.LogTrace("External event finished. ");

            return response;
        });
    }
}
