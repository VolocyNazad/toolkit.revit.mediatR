
using Autodesk.Revit.DB;
using MediatR;
using Revit.Context.Abstractions.Services;
using Revit.MediatR.Models;

namespace Revit.MediatR.Services;

/// <summary>
/// Обработчик <see cref="LambdaExternalEventCommand"/> — вызывает делегат команды над целевым документом
/// (запрошенным явно или активным документом из <see cref="IRevitContext"/>).
/// </summary>
/// <param name="revitContext">Источник текущего контекста Revit (активного документа).</param>
public class LambdaExternalEventCommandHandler(IRevitContext revitContext)
    : IRequestHandler<LambdaExternalEventCommand, Response<bool>>
{
    /// <inheritdoc/>
    /// <exception cref="InvalidOperationException">
    /// Ни у команды, ни у <see cref="IRevitContext"/> нет активного документа.
    /// </exception>
    public async Task<Response<bool>> Handle(LambdaExternalEventCommand request, CancellationToken cancellation)
    {
        Document targetDocument = request.Document ?? revitContext.ActiveDocument
            ?? throw new InvalidOperationException($"Document is null");
        return new Response<bool> { Result = await request.Delegate.Invoke(targetDocument, cancellation) };
    }
}
