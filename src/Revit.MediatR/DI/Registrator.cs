using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Revit.Context.DI;
using Revit.Events.DI;
using Revit.MediatR.Models;
using Revit.MediatR.Services;

namespace Revit.MediatR.DI;

/// <summary>
/// Registers MediatR, the built-in lambda commands, and Revit-specific pipeline behaviours
/// (transactions, <c>ExternalEvent</c>) in the dependency injection container.
/// </summary>
public static class Registrator
{
	extension(IServiceCollection services)
	{
        /// <summary>
        /// Registers <c>VolocyNazad.Revit.Events</c> (<c>AddEvents</c>) and <c>VolocyNazad.Revit.Context</c>
        /// (<c>AddRevitContext</c>), then MediatR along with the pipeline behaviours that open
        /// Revit transactions and marshal execution onto the Revit API thread via <c>ExternalEvent</c>.
        /// </summary>
        /// <returns>The same <see cref="IServiceCollection"/>, for further chaining.</returns>
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
