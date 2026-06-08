
using Autodesk.Revit.DB;
using MediatR;
using Microsoft.Extensions.Logging;
using Revit.Context.Abstractions.Services;
using Toolkit.Revit.MediatR.Abstractions.Models;

namespace Toolkit.Revit.MediatR.Services;

public class RevitTransactionBehaviour<TRequest, TResponse>(
        IRevitContext revitContext, ILogger<RevitTransactionBehaviour<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse> where TRequest : ITransactionCommand
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellation)
    {
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