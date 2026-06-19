using Autodesk.Revit.DB;
using Revit.MediatR.Abstractions.Models;
using Revit.MediatR.Base;

namespace Revit.MediatR.Models;

public class LambdaTransactionCommand(Document document, string transactionName, Func<Document, CancellationToken, Task<bool>> @delegate)
    : LambdaDocumentCommand(document, @delegate), ITransactionCommand
{
    public string TransactionName { get; } = transactionName;
}