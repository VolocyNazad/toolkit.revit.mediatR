using Autodesk.Revit.DB;
using Revit.MediatR.Abstractions.Models;
using Revit.MediatR.Base;

namespace Revit.MediatR.Models;

/// <summary>
/// A document-level command that runs the supplied async delegate via Revit's <c>ExternalEvent</c>
/// inside a named transaction.
/// </summary>
/// <param name="document">The target document.</param>
/// <param name="transactionName">The transaction name, shown in Revit's undo/redo history.</param>
/// <param name="delegate">The async delegate executed against the target <see cref="Document"/> inside the transaction.</param>
public class LambdaExternalEventTransactionCommand(Document document, string transactionName, Func<Document, CancellationToken, Task<bool>> @delegate)
    : LambdaDocumentCommand(document, @delegate), ITransactionCommand, IExternalEventCommand
{
    /// <inheritdoc/>
    public string TransactionName { get; } = transactionName;
}
