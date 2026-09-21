using Autodesk.Revit.DB;
using Revit.MediatR.Abstractions.Models;
using Revit.MediatR.Base;

namespace Revit.MediatR.Models;

/// <summary>
/// A document-level command that runs the supplied async delegate via Revit's <c>ExternalEvent</c>
/// (without opening a transaction).
/// </summary>
/// <param name="document">The target document.</param>
/// <param name="delegate">The async delegate executed against the target <see cref="Document"/>.</param>
public class LambdaExternalEventCommand(Document document, Func<Document, CancellationToken, Task<bool>> @delegate)
    : LambdaDocumentCommand(document, @delegate), IExternalEventCommand
{
}
