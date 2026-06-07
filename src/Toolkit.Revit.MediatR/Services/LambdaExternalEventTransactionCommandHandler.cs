
using Autodesk.Revit.DB;
using MediatR;
using Revit.Context.Abstractions.Services;
using Toolkit.Revit.MediatR.Models;

namespace Toolkit.Revit.MediatR.Services;

public class LambdaExternalEventTransactionCommandHandler(IRevitContext revitContext)
    : IRequestHandler<LambdaExternalEventTransactionCommand, Response<bool>>
{
    private readonly IRevitContext _revitContext = revitContext;

    public async Task<Response<bool>> Handle(LambdaExternalEventTransactionCommand request, CancellationToken cancellation)
    {
        Document targetDocument = request.Document ?? _revitContext.ActiveDocument ?? throw new InvalidOperationException($"Document is null");
        return new Response<bool> { Result = await request.Delegate.Invoke(targetDocument, cancellation) };
    }
}
