namespace Toolkit.Revit.MediatR.Abstractions.Models;

public interface ITransactionCommand : IDocumentCommand
{
    string TransactionName { get; }
}
