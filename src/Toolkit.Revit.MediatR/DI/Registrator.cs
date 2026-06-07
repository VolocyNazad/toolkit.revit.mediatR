using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Toolkit.Revit.MediatR.Models;
using Toolkit.Revit.MediatR.Services;

namespace Toolkit.Revit.MediatR.DI;

public static class Registrator
{
	extension(IServiceCollection services)
	{
        public IServiceCollection AddRevitContext() => services
            .AddTransient<IRequestHandler<LambdaExternalEventTransactionCommand, Response<bool>>, LambdaExternalEventTransactionCommandHandler>()
            .AddTransient<IRequestHandler<LambdaTransactionCommand, Response<bool>>, LambdaTransactionCommandHandler>()
            .AddTransient<IRequestHandler<LambdaExternalEventCommand, Response<bool>>, LambdaExternalEventCommandHandler>()

            .AddTransient<IRequestHandler<LambdaExternalEventApplicationCommand, Response<bool>>, LambdaExternalEventApplicationCommandHandler>()


            .AddTransient<IRequestHandler<LambdaExternalEventTransactionCommand, Response<bool>>, LambdaExternalEventTransactionCommandHandler>()
            .AddTransient<IRequestHandler<LambdaTransactionCommand, Response<bool>>, LambdaTransactionCommandHandler>()
            .AddTransient<IRequestHandler<LambdaExternalEventCommand, Response<bool>>, LambdaExternalEventCommandHandler>()

            .AddTransient<IRequestHandler<LambdaExternalEventApplicationCommand, Response<bool>>, LambdaExternalEventApplicationCommandHandler>()

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

                .AddBehavior<
                    IPipelineBehavior<LambdaExternalEventApplicationCommand, Response<bool>>,
                    RevitExternalEventApplicationBehaviour<LambdaExternalEventApplicationCommand, Response<bool>>>()


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

                .AddBehavior<
                    IPipelineBehavior<LambdaExternalEventApplicationCommand, Response<bool>>,
                    RevitExternalEventApplicationBehaviour<LambdaExternalEventApplicationCommand, Response<bool>>>()
            );
    }
}
