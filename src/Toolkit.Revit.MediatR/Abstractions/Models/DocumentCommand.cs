
using Autodesk.Revit.DB;

namespace Toolkit.Revit.MediatR.Abstractions.Models;

public abstract class DocumentCommand(Document? document = null) : IDocumentCommand
{
    public Document? Document { get; } = document;
}