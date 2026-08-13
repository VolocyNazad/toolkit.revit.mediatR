namespace Revit.MediatR.Abstractions.Models;

/// <summary>
/// Команда уровня документа, которая должна выполняться через Revit <c>ExternalEvent</c>
/// (то есть в потоке Revit API, а не в вызывающем потоке).
/// </summary>
public interface IExternalEventCommand : IDocumentCommand
{
}
