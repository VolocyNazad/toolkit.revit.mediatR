using Microsoft.Extensions.DependencyInjection;

namespace Revit.Context.DI;

public static class Registrator
{
	extension(IServiceCollection services)
	{
        public IServiceCollection AddRevitContext() => services;
    }
}
