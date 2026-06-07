
using Autodesk.Revit.DB;

namespace Toolkit.Revit.MediatR.Abstractions.Models;

public abstract class ExternalEventTransactionCommand(Document document, string transactionName)
    : TransactionCommand(document, transactionName), IExternalEventCommand
{
}
