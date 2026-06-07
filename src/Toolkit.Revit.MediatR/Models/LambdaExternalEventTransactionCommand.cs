using Autodesk.Revit.DB;
using Toolkit.Revit.MediatR.Abstractions.Models;

namespace Toolkit.Revit.MediatR.Models;

public class LambdaExternalEventTransactionCommand(Document document, string transactionName, Func<Document, CancellationToken, Task<bool>> @delegate)
   : LambdaTransactionCommand(document, transactionName, @delegate), IExternalEventCommand
{
}
