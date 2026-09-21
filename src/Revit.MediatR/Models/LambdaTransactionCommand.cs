using Autodesk.Revit.DB;
using Revit.MediatR.Abstractions.Models;
using Revit.MediatR.Base;

namespace Revit.MediatR.Models;

/// <summary>
/// A document-level command that runs the supplied async delegate inside a named Revit transaction
/// (without <c>ExternalEvent</c> — it is assumed to already be invoked on the Revit API thread).
/// </summary>
/// <param name="document">The target document.</param>
/// <param name="transactionName">The transaction name, shown in Revit's undo/redo history.</param>
/// <param name="delegate">The async delegate executed against the target <see cref="Document"/> inside the transaction.</param>
public class LambdaTransactionCommand(Document document, string transactionName, Func<Document, CancellationToken, Task<bool>> @delegate)
    : LambdaDocumentCommand(document, @delegate), ITransactionCommand
{
    /// <inheritdoc/>
    public string TransactionName { get; } = transactionName;
}
