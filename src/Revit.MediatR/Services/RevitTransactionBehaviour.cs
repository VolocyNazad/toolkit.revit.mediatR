
using Autodesk.Revit.DB;
using MediatR;
using Microsoft.Extensions.Logging;
using Revit.Context.Abstractions.Services;
using Revit.MediatR.Abstractions.Models;

namespace Revit.MediatR.Services;

/// <summary>
/// MediatR pipeline behaviour: opens a named Revit transaction on the target document before invoking
/// the next stage of the pipeline and commits it afterward, logging the start/finish.
/// </summary>
/// <typeparam name="TRequest">The command type implementing <see cref="ITransactionCommand"/>.</typeparam>
/// <typeparam name="TResponse">The type of the command's processing result.</typeparam>
/// <param name="revitContext">The source of the current Revit context (the active document).</param>
/// <param name="logger">The logger used to trace the start/finish of the transaction.</param>
public partial class RevitTransactionBehaviour<TRequest, TResponse>(
        IRevitContext revitContext, ILogger<RevitTransactionBehaviour<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse> where TRequest : ITransactionCommand
{
    /// <inheritdoc/>
    /// <exception cref="System.InvalidOperationException">
    /// Neither the command nor <see cref="IRevitContext"/> has an active document.
    /// </exception>
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        Guard.ThrowIfNull(request, nameof(request));
        Guard.ThrowIfNull(next, nameof(next));

        Document? targetDocument = request.Document ?? revitContext.ActiveDocument
            ?? throw new InvalidOperationException("Document is null");
        using Transaction transaction = new(targetDocument, request.TransactionName);

        LogTransactionStarting(logger, request.Document?.Title, request.TransactionName);

        transaction.Start();

        TResponse? response = await next(cancellationToken).ConfigureAwait(false);

        transaction.Commit();

        LogTransactionCommitted(logger, request.Document?.Title, request.TransactionName);

        return response;
    }

    [LoggerMessage(EventId = 1, Level = LogLevel.Trace,
        Message = "Transaction for document '{DocumentTitle}' with name '{TransactionName}' is starting.")]
    private static partial void LogTransactionStarting(ILogger logger, string? documentTitle, string transactionName);

    [LoggerMessage(EventId = 2, Level = LogLevel.Trace,
        Message = "Transaction for document '{DocumentTitle}' with name '{TransactionName}' is committed.")]
    private static partial void LogTransactionCommitted(ILogger logger, string? documentTitle, string transactionName);
}
