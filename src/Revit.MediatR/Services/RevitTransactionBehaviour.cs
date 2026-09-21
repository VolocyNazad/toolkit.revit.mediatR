
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
public class RevitTransactionBehaviour<TRequest, TResponse>(
        IRevitContext revitContext, ILogger<RevitTransactionBehaviour<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse> where TRequest : ITransactionCommand
{
    /// <inheritdoc/>
    /// <exception cref="System.InvalidOperationException">
    /// Neither the command nor <see cref="IRevitContext"/> has an active document.
    /// </exception>
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellation)
    {
        Guard.ThrowIfNull(request, nameof(request));
        Guard.ThrowIfNull(next, nameof(next));

        Document? targetDocument = request.Document ?? revitContext.ActiveDocument
            ?? throw new InvalidOperationException($"Document is null");
        using Transaction transaction = new(targetDocument, request.TransactionName);

        logger.LogTrace(
            "Transaction for document '{revit.document.title}' with name '{revit.transaction.name}'. Starting...",
            request.Document?.Title, request.TransactionName);

        transaction.Start();

        TResponse? response = await next(cancellation).ConfigureAwait(false);

        transaction.Commit();

        logger.LogTrace(
            "Transaction for document '{revit.document.title}' with name '{revit.transaction.name}'. Commited.",
            request.Document?.Title, request.TransactionName);

        return response;
    }
}
