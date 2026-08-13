using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Revit.Context.DI;
using Revit.Events.DI;
using Revit.MediatR.Models;
using Revit.MediatR.Services;

namespace Revit.MediatR.DI;

/// <summary>
/// Регистрация MediatR, встроенных lambda-команд и Revit-специфичных поведений конвейера
/// (транзакции, <c>ExternalEvent</c>) в контейнере зависимостей.
/// </summary>
public static class Registrator
{
	extension(IServiceCollection services)
	{
        /// <summary>
        /// Регистрирует <c>VolocyNazad.Revit.Events</c> (<c>AddEvents</c>) и <c>VolocyNazad.Revit.Context</c>
        /// (<c>AddRevitContext</c>), затем MediatR вместе с поведениями конвейера, которые открывают
        /// Revit-транзакции и переносят выполнение в поток Revit API через <c>ExternalEvent</c>.
        /// </summary>
        /// <returns>Тот же <see cref="IServiceCollection"/> для дальнейшей цепочки вызовов.</returns>
        public IServiceCollection AddMediatR() => services
            .AddEvents().AddRevitContext()
            .AddMediatR(cfg => cfg
               .RegisterServicesFromAssemblyContaining(typeof(Registrator))
               .AddBehavior<
                   IPipelineBehavior<LambdaExternalEventTransactionCommand, Response<bool>>,
                   RevitExternalEventBehaviour<LambdaExternalEventTransactionCommand, Response<bool>>>()
               .AddBehavior<
                   IPipelineBehavior<LambdaExternalEventTransactionCommand, Response<bool>>,
                   RevitTransactionBehaviour<LambdaExternalEventTransactionCommand, Response<bool>>>()
               .AddBehavior<
                   IPipelineBehavior<LambdaExternalEventCommand, Response<bool>>,
                   RevitExternalEventBehaviour<LambdaExternalEventCommand, Response<bool>>>()
               .AddBehavior<
                   IPipelineBehavior<LambdaTransactionCommand, Response<bool>>,
                   RevitTransactionBehaviour<LambdaTransactionCommand, Response<bool>>>()
           );
    }
}
