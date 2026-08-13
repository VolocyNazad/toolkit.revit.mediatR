using Autodesk.Revit.DB;
using Revit.MediatR.Abstractions.Models;
using Revit.MediatR.Base;

namespace Revit.MediatR.Models;

/// <summary>
/// Команда уровня документа, выполняющая переданный асинхронный делегат через Revit <c>ExternalEvent</c>
/// внутри именованной транзакции.
/// </summary>
/// <param name="document">Целевой документ.</param>
/// <param name="transactionName">Имя транзакции, отображаемое в истории отмены/повтора Revit.</param>
/// <param name="delegate">Асинхронный делегат, выполняемый над целевым <see cref="Document"/> внутри транзакции.</param>
public class LambdaExternalEventTransactionCommand(Document document, string transactionName, Func<Document, CancellationToken, Task<bool>> @delegate)
    : LambdaDocumentCommand(document, @delegate), ITransactionCommand, IExternalEventCommand
{
    /// <inheritdoc/>
    public string TransactionName { get; } = transactionName;
}
