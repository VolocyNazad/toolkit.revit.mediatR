
using Autodesk.Revit.DB;
using MediatR;
using Revit.Context.Abstractions.Services;
using Revit.MediatR.Models;

namespace Revit.MediatR.Services;

/// <summary>
/// Обработчик <see cref="LambdaExternalEventTransactionCommand"/> — вызывает делегат команды над целевым
/// документом. Открытие/коммит транзакции и вынос в поток Revit API выполняют
/// <see cref="RevitTransactionBehaviour{TRequest,TResponse}"/> и <see cref="RevitExternalEventBehaviour{TRequest,TResponse}"/>
/// в конвейере MediatR.
/// </summary>
/// <param name="revitContext">Источник текущего контекста Revit (активного документа).</param>
public class LambdaExternalEventTransactionCommandHandler(IRevitContext revitContext)
    : IRequestHandler<LambdaExternalEventTransactionCommand, Response<bool>>
{
    /// <inheritdoc/>
    /// <exception cref="InvalidOperationException">
    /// Ни у команды, ни у <see cref="IRevitContext"/> нет активного документа.
    /// </exception>
    public async Task<Response<bool>> Handle(LambdaExternalEventTransactionCommand request, CancellationToken cancellation)
    {
        Document targetDocument = request.Document ?? revitContext.ActiveDocument
            ?? throw new InvalidOperationException($"Document is null");
        return new Response<bool> { Result = await request.Delegate.Invoke(targetDocument, cancellation) };
    }
}
