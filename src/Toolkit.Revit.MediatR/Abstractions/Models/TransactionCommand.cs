
using Autodesk.Revit.DB;

namespace Toolkit.Revit.MediatR.Abstractions.Models;

public abstract class TransactionCommand(Document document, string transactionName)
    : DocumentCommand(document), ITransactionCommand
{
    public string TransactionName { get; } = transactionName;
}
