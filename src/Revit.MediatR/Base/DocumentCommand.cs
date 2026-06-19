
using Autodesk.Revit.DB;
using Revit.MediatR.Abstractions.Models;

namespace Revit.MediatR.Base;

public abstract class DocumentCommand(Document? document = null) : IDocumentCommand
{
    public Document? Document { get; } = document;
}