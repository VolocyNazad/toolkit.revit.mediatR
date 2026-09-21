
using Autodesk.Revit.DB;
using Revit.MediatR.Abstractions.Models;

namespace Revit.MediatR.Base;

/// <summary>
/// Base implementation of <see cref="IDocumentCommand"/> for Revit document-level commands.
/// </summary>
/// <param name="document">
/// The target document. If not set, the handler uses the active document from <c>IRevitContext</c>.
/// </param>
public abstract class DocumentCommand(Document? document = null) : IDocumentCommand
{
    /// <inheritdoc/>
    public Document? Document { get; } = document;
}
