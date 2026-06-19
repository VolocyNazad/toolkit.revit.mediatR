using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Revit.Context.DI;
using Revit.Events.DI;
using Revit.MediatR.Models;
using Revit.MediatR.Services;

namespace Revit.MediatR.DI;

public static class Registrator
{
	extension(IServiceCollection services)
	{
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
