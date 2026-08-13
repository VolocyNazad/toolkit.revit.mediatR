namespace Revit.MediatR.Abstractions.Models;

/// <summary>
/// Команда уровня документа, выполняемая внутри именованной Revit-транзакции.
/// </summary>
public interface ITransactionCommand : IDocumentCommand
{
    /// <summary>
    /// Имя транзакции, отображаемое в истории отмены/повтора Revit.
    /// </summary>
    string TransactionName { get; }
}
