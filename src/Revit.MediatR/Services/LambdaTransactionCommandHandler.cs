
using Autodesk.Revit.DB;
using MediatR;
using Revit.Context.Abstractions.Services;
using Revit.MediatR.Models;

namespace Revit.MediatR.Services;

/// <summary>
/// Handler for <see cref="LambdaTransactionCommand"/> — invokes the command's delegate against the target document.
/// Opening/committing the transaction itself is performed by <see cref="RevitTransactionBehaviour{TRequest,TResponse}"/>
/// in the MediatR pipeline.
/// </summary>
/// <param name="revitContext">The source of the current Revit context (the active document).</param>
public class LambdaTransactionCommandHandler(IRevitContext revitContext)
    : IRequestHandler<LambdaTransactionCommand, Response<bool>>
{
    /// <inheritdoc/>
    /// <exception cref="System.InvalidOperationException">
    /// Neither the command nor <see cref="IRevitContext"/> has an active document.
    /// </exception>
    public async Task<Response<bool>> Handle(LambdaTransactionCommand request, CancellationToken cancellation)
    {
        Guard.ThrowIfNull(request, nameof(request));

        Document targetDocument = request.Document ?? revitContext.ActiveDocument
            ?? throw new InvalidOperationException($"Document is null");
        return new Response<bool> { Result = await request.Delegate.Invoke(targetDocument, cancellation) };
    }
}
