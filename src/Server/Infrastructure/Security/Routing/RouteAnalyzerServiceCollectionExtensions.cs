using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Security.Routing;

public static class RouteAnalyzerServiceCollectionExtensions : object
{
	static RouteAnalyzerServiceCollectionExtensions()
	{
	}

	public static Microsoft.Extensions.DependencyInjection.IServiceCollection
		AddRouteAnalyzer(this Microsoft.Extensions.DependencyInjection.IServiceCollection services)
	{
		// using Microsoft.Extensions.DependencyInjection;
		services.AddSingleton<IRouteAnalyzer, RouteAnalyzer>();

		return services;
	}
}
