using Autodesk.Revit.DB;
using Revit.MediatR.Abstractions.Models;
using Revit.MediatR.Base;

namespace Revit.MediatR.Models;

/// <summary>
/// Команда уровня документа, выполняющая переданный асинхронный делегат внутри именованной Revit-транзакции
/// (без <c>ExternalEvent</c> — предполагается, что она уже вызывается в потоке Revit API).
/// </summary>
/// <param name="document">Целевой документ.</param>
/// <param name="transactionName">Имя транзакции, отображаемое в истории отмены/повтора Revit.</param>
/// <param name="delegate">Асинхронный делегат, выполняемый над целевым <see cref="Document"/> внутри транзакции.</param>
public class LambdaTransactionCommand(Document document, string transactionName, Func<Document, CancellationToken, Task<bool>> @delegate)
    : LambdaDocumentCommand(document, @delegate), ITransactionCommand
{
    /// <inheritdoc/>
    public string TransactionName { get; } = transactionName;
}
