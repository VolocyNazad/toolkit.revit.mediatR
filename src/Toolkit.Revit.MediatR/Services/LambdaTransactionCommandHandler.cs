
using Autodesk.Revit.DB;
using MediatR;
using Revit.Context.Abstractions.Services;
using Toolkit.Revit.MediatR.Models;

namespace Toolkit.Revit.MediatR.Services;

public class LambdaTransactionCommandHandler(IRevitContext revitContext)
    : IRequestHandler<LambdaTransactionCommand, Response<bool>>
{
    public async Task<Response<bool>> Handle(LambdaTransactionCommand request, CancellationToken cancellation)
    {
        Document targetDocument = request.Document ?? revitContext.ActiveDocument 
            ?? throw new InvalidOperationException($"Document is null");
        return new Response<bool> { Result = await request.Delegate.Invoke(targetDocument, cancellation) };
    }
}