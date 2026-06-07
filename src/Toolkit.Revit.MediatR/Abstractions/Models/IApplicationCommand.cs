
using MediatR;
using Toolkit.Revit.MediatR.Models;

namespace Toolkit.Revit.MediatR.Abstractions.Models;

public interface IApplicationCommand : IRequest<Response<bool>>
{
}