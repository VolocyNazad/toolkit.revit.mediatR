
using Autodesk.Revit.DB;
using MediatR;
using Revit.Context.Abstractions.Services;
using Revit.MediatR.Models;

namespace Revit.MediatR.Services;

/// <summary>
/// Handler for <see cref="LambdaExternalEventCommand"/> — invokes the command's delegate against the target
/// document (explicitly requested, or the active document from <see cref="IRevitContext"/>).
/// </summary>
/// <param name="revitContext">The source of the current Revit context (the active document).</param>
public class LambdaExternalEventCommandHandler(IRevitContext revitContext)
    : IRequestHandler<LambdaExternalEventCommand, Response<bool>>
{
    /// <inheritdoc/>
    /// <exception cref="System.InvalidOperationException">
    /// Neither the command nor <see cref="IRevitContext"/> has an active document.
    /// </exception>
    public async Task<Response<bool>> Handle(LambdaExternalEventCommand request, CancellationToken cancellation)
    {
        Guard.ThrowIfNull(request, nameof(request));

        Document targetDocument = request.Document ?? revitContext.ActiveDocument
            ?? throw new InvalidOperationException($"Document is null");
        return new Response<bool> { Result = await request.Delegate.Invoke(targetDocument, cancellation) };
    }
}
