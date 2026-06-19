
using Autodesk.Revit.DB;
using MediatR;
using Revit.MediatR.Models;

namespace Revit.MediatR.Abstractions.Models;

public interface IDocumentCommand : IRequest<Response<bool>>
{
    Document? Document { get; }
}