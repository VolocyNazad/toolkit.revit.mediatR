
using Autodesk.Revit.DB;
using MediatR;
using Revit.Context.Abstractions.Services;
using Revit.MediatR.Models;

namespace Revit.MediatR.Services;

/// <summary>
/// Handler for <see cref="LambdaExternalEventTransactionCommand"/> — invokes the command's delegate against
/// the target document. Opening/committing the transaction and marshaling onto the Revit API thread are
/// performed by <see cref="RevitTransactionBehaviour{TRequest,TResponse}"/> and
/// <see cref="RevitExternalEventBehaviour{TRequest,TResponse}"/> in the MediatR pipeline.
/// </summary>
/// <param name="revitContext">The source of the current Revit context (the active document).</param>
public class LambdaExternalEventTransactionCommandHandler(IRevitContext revitContext)
    : IRequestHandler<LambdaExternalEventTransactionCommand, Response<bool>>
{
    /// <inheritdoc/>
    /// <exception cref="System.InvalidOperationException">
    /// Neither the command nor <see cref="IRevitContext"/> has an active document.
    /// </exception>
    public async Task<Response<bool>> Handle(LambdaExternalEventTransactionCommand request, CancellationToken cancellation)
    {
        Guard.ThrowIfNull(request, nameof(request));

        Document targetDocument = request.Document ?? revitContext.ActiveDocument
            ?? throw new InvalidOperationException($"Document is null");
        return new Response<bool> { Result = await request.Delegate.Invoke(targetDocument, cancellation) };
    }
}
