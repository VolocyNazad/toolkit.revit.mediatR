
using Autodesk.Revit.DB;
using MediatR;
using Revit.MediatR.Models;

namespace Revit.MediatR.Abstractions.Models;

/// <summary>
/// MediatR-запрос, выполняемый в контексте конкретного документа Revit.
/// </summary>
public interface IDocumentCommand : IRequest<Response<bool>>
{
    /// <summary>
    /// Целевой документ. Если <see langword="null"/>, обработчик использует активный документ из <c>IRevitContext</c>.
    /// </summary>
    Document? Document { get; }
}
