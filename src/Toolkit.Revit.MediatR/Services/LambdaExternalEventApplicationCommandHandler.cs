
using MediatR;
using Revit.Context.Abstractions.Services;
using Toolkit.Revit.MediatR.Models;

namespace Toolkit.Revit.MediatR.Services;

public class LambdaExternalEventApplicationCommandHandler(IRevitContext revitContext)
    : IRequestHandler<LambdaExternalEventApplicationCommand, Response<bool>>
{
    private readonly IRevitContext _revitContext = revitContext;

    public async Task<Response<bool>> Handle(LambdaExternalEventApplicationCommand request, CancellationToken cancellation)
    {
        return new Response<bool> { Result = await request.Delegate.Invoke(_revitContext.Application!, cancellation) };
    }
}