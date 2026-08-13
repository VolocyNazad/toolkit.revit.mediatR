using Autodesk.Revit.DB;
using Revit.MediatR.Abstractions.Models;
using Revit.MediatR.Base;

namespace Revit.MediatR.Models;

/// <summary>
/// Команда уровня документа, выполняющая переданный асинхронный делегат через Revit <c>ExternalEvent</c>
/// (без открытия транзакции).
/// </summary>
/// <param name="document">Целевой документ.</param>
/// <param name="delegate">Асинхронный делегат, выполняемый над целевым <see cref="Document"/>.</param>
public class LambdaExternalEventCommand(Document document, Func<Document, CancellationToken, Task<bool>> @delegate)
    : LambdaDocumentCommand(document, @delegate), IExternalEventCommand
{
}
