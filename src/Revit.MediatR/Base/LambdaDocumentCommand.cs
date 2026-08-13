
using Autodesk.Revit.DB;

namespace Revit.MediatR.Base;

/// <summary>
/// Базовая реализация команды уровня документа, выполняющей произвольный асинхронный делегат.
/// </summary>
/// <param name="document">Целевой документ.</param>
/// <param name="delegate">Асинхронный делегат, выполняемый над целевым <see cref="Document"/>.</param>
public abstract class LambdaDocumentCommand(Document document, Func<Document, CancellationToken, Task<bool>> @delegate)
    : DocumentCommand(document)
{
    /// <summary>
    /// Асинхронный делегат, выполняемый над целевым документом.
    /// </summary>
    public Func<Document, CancellationToken, Task<bool>> Delegate { get; } = @delegate;
}
