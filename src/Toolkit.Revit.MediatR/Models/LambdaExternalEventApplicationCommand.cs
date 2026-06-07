using Autodesk.Revit.ApplicationServices;
using Toolkit.Revit.MediatR.Abstractions.Models;

namespace Toolkit.Revit.MediatR.Models;

public class LambdaExternalEventApplicationCommand(Func<Application, CancellationToken, Task<bool>> @delegate)
   : LambdaApplicationCommand(@delegate), IExternalEventApplicationCommand
{
}