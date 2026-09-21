
using Autodesk.Revit.DB;

namespace Revit.MediatR.Base;

/// <summary>
/// Base implementation of a document-level command that executes an arbitrary async delegate.
/// </summary>
/// <param name="document">The target document.</param>
/// <param name="delegate">The async delegate executed against the target <see cref="Document"/>.</param>
public abstract class LambdaDocumentCommand(Document document, Func<Document, CancellationToken, Task<bool>> @delegate)
    : DocumentCommand(document)
{
    /// <summary>
    /// The async delegate executed against the target document.
    /// </summary>
    public Func<Document, CancellationToken, Task<bool>> Delegate { get; } = @delegate;
}
