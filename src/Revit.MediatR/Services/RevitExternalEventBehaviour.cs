using MediatR;
using Microsoft.Extensions.Logging;
using Revit.Events.Abstractions.Services;
using Revit.MediatR.Abstractions.Models;

namespace Revit.MediatR.Services;

/// <summary>
/// MediatR pipeline behaviour: marshals execution of <typeparamref name="TRequest"/> onto the Revit API thread
/// via <see cref="IAsyncExternalEvent"/> (a Revit <c>ExternalEvent</c> under the hood) and logs the start/finish.
/// </summary>
/// <typeparam name="TRequest">The document-level command type implementing <see cref="IExternalEventCommand"/>.</typeparam>
/// <typeparam name="TResponse">The type of the command's processing result.</typeparam>
/// <param name="externalEvent">The Revit external event through which the delegate is executed.</param>
/// <param name="logger">The logger used to trace the start/finish of the external event.</param>
public partial class RevitExternalEventBehaviour<TRequest, TResponse>(
        IAsyncExternalEvent externalEvent,
        ILogger<RevitExternalEventBehaviour<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull, IExternalEventCommand
{
    /// <inheritdoc/>
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        Guard.ThrowIfNull(next, nameof(next));

        Task<TResponse>? responseTask = null;
        await externalEvent.Raise(_ =>
        {
            LogExternalEventStarted(logger);
            responseTask = next(cancellationToken);
        }).ConfigureAwait(false);

        if (responseTask is null)
        {
            throw new InvalidOperationException("The external event completed without invoking its action.");
        }

        TResponse response = await responseTask.ConfigureAwait(false);
        LogExternalEventFinished(logger);
        return response;
    }

    [LoggerMessage(EventId = 1, Level = LogLevel.Trace, Message = "External event started.")]
    private static partial void LogExternalEventStarted(ILogger logger);

    [LoggerMessage(EventId = 2, Level = LogLevel.Trace, Message = "External event finished.")]
    private static partial void LogExternalEventFinished(ILogger logger);
}
