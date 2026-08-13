using MediatR;
using Microsoft.Extensions.Logging;
using Revit.Events.Abstractions.Services;
using Revit.MediatR.Abstractions.Models;

namespace Revit.MediatR.Services;

/// <summary>
/// Поведение конвейера MediatR: переносит выполнение <typeparamref name="TRequest"/> в поток Revit API
/// через <see cref="IAsyncExternalEvent"/> (Revit <c>ExternalEvent</c> под капотом) и логирует старт/финиш.
/// </summary>
/// <typeparam name="TRequest">Тип команды уровня документа, реализующей <see cref="IExternalEventCommand"/>.</typeparam>
/// <typeparam name="TResponse">Тип результата обработки команды.</typeparam>
/// <param name="externalEvent">Внешнее событие Revit, через которое выполняется делегат.</param>
/// <param name="logger">Логгер для трассировки старта/финиша внешнего события.</param>
public class RevitExternalEventBehaviour<TRequest, TResponse>(
        IAsyncExternalEvent externalEvent,
        ILogger<RevitExternalEventBehaviour<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull, IExternalEventCommand
{
    /// <inheritdoc/>
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
