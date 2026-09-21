namespace Revit.MediatR.Abstractions.Models;

/// <summary>
/// A document-level command executed within a named Revit transaction.
/// </summary>
public interface ITransactionCommand : IDocumentCommand
{
    /// <summary>
    /// The transaction name, shown in Revit's undo/redo history.
    /// </summary>
    string TransactionName { get; }
}
