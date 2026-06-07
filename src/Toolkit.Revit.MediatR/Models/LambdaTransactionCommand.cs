using Autodesk.Revit.DB;
using Toolkit.Revit.MediatR.Abstractions.Models;

namespace Toolkit.Revit.MediatR.Models;

public class LambdaTransactionCommand(Document document, string transactionName, Func<Document, CancellationToken, Task<bool>> @delegate)
    : LambdaDocumentCommand(document, @delegate), ITransactionCommand
{
    public string TransactionName { get; } = transactionName;
}