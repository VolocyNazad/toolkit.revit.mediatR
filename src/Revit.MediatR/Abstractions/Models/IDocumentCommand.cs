
using Autodesk.Revit.DB;
using MediatR;
using Revit.MediatR.Models;

namespace Revit.MediatR.Abstractions.Models;

/// <summary>
/// A MediatR request executed in the context of a specific Revit document.
/// </summary>
public interface IDocumentCommand : IRequest<Response<bool>>
{
    /// <summary>
    /// The target document. If <see langword="null"/>, the handler uses the active document from <c>IRevitContext</c>.
    /// </summary>
    Document? Document { get; }
}
