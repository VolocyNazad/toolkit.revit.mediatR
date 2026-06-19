
using Autodesk.Revit.DB;

namespace Revit.MediatR.Base;

public abstract class LambdaDocumentCommand(Document document, Func<Document, CancellationToken, Task<bool>> @delegate)
    : DocumentCommand(document)
{
    public Func<Document, CancellationToken, Task<bool>> Delegate { get; } = @delegate;
}