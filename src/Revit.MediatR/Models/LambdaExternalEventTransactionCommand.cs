using Autodesk.Revit.DB;
using Revit.MediatR.Abstractions.Models;
using Revit.MediatR.Base;

namespace Revit.MediatR.Models;

public class LambdaExternalEventTransactionCommand(Document document, string transactionName, Func<Document, CancellationToken, Task<bool>> @delegate)
    : LambdaDocumentCommand(document, @delegate), ITransactionCommand, IExternalEventCommand
{
    public string TransactionName { get; } = transactionName;
}
