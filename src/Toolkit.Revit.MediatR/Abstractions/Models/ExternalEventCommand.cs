
using Autodesk.Revit.DB;

namespace Toolkit.Revit.MediatR.Abstractions.Models;

public abstract class ExternalEventCommand(Document document)
    : DocumentCommand(document), IExternalEventCommand
{
}
