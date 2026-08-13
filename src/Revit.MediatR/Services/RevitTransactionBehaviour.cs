
using Autodesk.Revit.DB;
using MediatR;
using Microsoft.Extensions.Logging;
using Revit.Context.Abstractions.Services;
using Revit.MediatR.Abstractions.Models;

namespace Revit.MediatR.Services;

/// <summary>
/// Поведение конвейера MediatR: открывает именованную Revit-транзакцию на целевом документе перед вызовом
/// следующего звена конвейера и коммитит её после, с логированием старта/финиша.
/// </summary>
/// <typeparam name="TRequest">Тип команды, реализующей <see cref="ITransactionCommand"/>.</typeparam>
/// <typeparam name="TResponse">Тип результата обработки команды.</typeparam>
/// <param name="revitContext">Источник текущего контекста Revit (активного документа).</param>
/// <param name="logger">Логгер для трассировки старта/финиша транзакции.</param>
public class RevitTransactionBehaviour<TRequest, TResponse>(
        IRevitContext revitContext, ILogger<RevitTransactionBehaviour<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse> where TRequest : ITransactionCommand
{
    /// <inheritdoc/>
    /// <exception cref="InvalidOperationException">
    /// Ни у команды, ни у <see cref="IRevitContext"/> нет активного документа.
    /// </exception>
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
