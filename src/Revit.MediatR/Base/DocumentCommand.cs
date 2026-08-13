
using Autodesk.Revit.DB;
using Revit.MediatR.Abstractions.Models;

namespace Revit.MediatR.Base;

/// <summary>
/// Базовая реализация <see cref="IDocumentCommand"/> для команд уровня документа Revit.
/// </summary>
/// <param name="document">
/// Целевой документ. Если не задан, обработчик использует активный документ из <c>IRevitContext</c>.
/// </param>
public abstract class DocumentCommand(Document? document = null) : IDocumentCommand
{
    /// <inheritdoc/>
    public Document? Document { get; } = document;
}
