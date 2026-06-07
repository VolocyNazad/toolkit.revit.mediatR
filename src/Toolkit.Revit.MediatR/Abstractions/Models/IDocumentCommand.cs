
using Autodesk.Revit.DB;
using MediatR;
using Toolkit.Revit.MediatR.Models;

namespace Toolkit.Revit.MediatR.Abstractions.Models;

public interface IDocumentCommand : IRequest<Response<bool>>
{
    Document? Document { get; }
}