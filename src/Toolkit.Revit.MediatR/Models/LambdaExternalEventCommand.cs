using Autodesk.Revit.DB;
using Toolkit.Revit.MediatR.Abstractions.Models;

namespace Toolkit.Revit.MediatR.Models;

public class LambdaExternalEventCommand(Document document, Func<Document, CancellationToken, Task<bool>> @delegate)
    : LambdaDocumentCommand(document, @delegate), IExternalEventCommand
{
}
