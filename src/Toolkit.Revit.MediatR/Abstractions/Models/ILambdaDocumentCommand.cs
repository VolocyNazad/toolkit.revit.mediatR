
using Autodesk.Revit.DB;

namespace Toolkit.Revit.MediatR.Abstractions.Models;

public interface ILambdaDocumentCommand : IDocumentCommand
{
    Func<Document, bool> Delegate { get; }
}

public interface ILambdaDocumentAsyncCommand : IDocumentCommand
{
    Func<Document, CancellationToken, Task<bool>> Delegate { get; }
}
