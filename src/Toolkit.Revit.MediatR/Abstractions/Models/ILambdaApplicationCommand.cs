
using Autodesk.Revit.ApplicationServices;

namespace Toolkit.Revit.MediatR.Abstractions.Models;

public interface ILambdaApplicationCommand : IApplicationCommand
{
    Func<Application, CancellationToken, bool> Delegate { get; }
}

public interface ILambdaApplicationAsyncCommand : IApplicationCommand
{
    Func<Application, CancellationToken, Task<bool>> Delegate { get; }
}