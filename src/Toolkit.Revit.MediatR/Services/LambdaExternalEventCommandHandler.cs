
using Autodesk.Revit.DB;
using MediatR;
using Revit.Context.Abstractions.Services;
using Toolkit.Revit.MediatR.Models;

namespace Toolkit.Revit.MediatR.Services;

public class LambdaExternalEventCommandHandler(IRevitContext revitContext)
    : IRequestHandler<LambdaExternalEventCommand, Response<bool>>
{
    private readonly IRevitContext _revitContext = revitContext;

    public async Task<Response<bool>> Handle(LambdaExternalEventCommand request, CancellationToken cancellation)
    {
        Document targetDocument = request.Document ?? _revitContext.ActiveDocument
            ?? throw new InvalidOperationException($"Document is null");
        return new Response<bool> { Result = await request.Delegate.Invoke(targetDocument, cancellation) };
    }
}
