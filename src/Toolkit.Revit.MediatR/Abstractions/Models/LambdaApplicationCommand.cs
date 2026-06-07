
using Autodesk.Revit.ApplicationServices;

namespace Toolkit.Revit.MediatR.Abstractions.Models;

public abstract class LambdaApplicationCommand(Func<Application, CancellationToken, Task<bool>> @delegate)
   : ApplicationCommand(), ILambdaApplicationAsyncCommand
{
    public Func<Application, CancellationToken, Task<bool>> Delegate { get; } = @delegate;
}