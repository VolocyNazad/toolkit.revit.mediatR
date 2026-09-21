namespace Revit.MediatR.Abstractions.Models;

/// <summary>
/// A document-level command that must be executed via Revit's <c>ExternalEvent</c>
/// (i.e. on the Revit API thread, not on the calling thread).
/// </summary>
public interface IExternalEventCommand : IDocumentCommand
{
}
