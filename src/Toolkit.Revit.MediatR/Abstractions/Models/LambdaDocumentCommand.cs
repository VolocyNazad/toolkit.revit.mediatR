
using Autodesk.Revit.DB;

namespace Toolkit.Revit.MediatR.Abstractions.Models;

public abstract class LambdaDocumentCommand(Document document, Func<Document, CancellationToken, Task<bool>> @delegate)
    : DocumentCommand(document), ILambdaDocumentAsyncCommand
{
    public Func<Document, CancellationToken, Task<bool>> Delegate { get; } = @delegate;
}