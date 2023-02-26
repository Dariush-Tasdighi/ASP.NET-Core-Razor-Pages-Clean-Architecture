using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Routing.Template;
using System.Linq;

namespace Infrastructure.Security.Routing;

public class RouteAnalyzer : object, IRouteAnalyzer
{
	public RouteAnalyzer(Microsoft.AspNetCore.Mvc.Infrastructure
		.IActionDescriptorCollectionProvider actionDescriptorCollectionProvider)
	{
		ActionDescriptorCollectionProvider =
			actionDescriptorCollectionProvider;
	}

	private Microsoft.AspNetCore.Mvc.Infrastructure
		.IActionDescriptorCollectionProvider ActionDescriptorCollectionProvider
	{ get; }

	public System.Collections.Generic.IList<RouteInformation> GetAllRouteInformations()
	{
		System.Collections.Generic.List<RouteInformation> result = new();

		var actions =
			ActionDescriptorCollectionProvider.ActionDescriptors.Items
			.Where(current => current.AttributeRouteInfo is not null)
			.OrderBy(current => current.DisplayName)
			.ToList()
			;

		foreach (var action in actions)
		{
			var routeType = action.GetType();
			var routeTypeName = routeType.Name;

			System.Collections.Generic.IList<RouteInformation>? routes = null;

			routes =
				GetRoutesIfIsPageAction(action: action);

			if (routes is not null)
			{
				foreach (var route in routes)
				{
					result.Add(item: route);
				}
			}
		}

		return result;
	}

	public System.Collections.Generic.IList<RouteInformation>?
		GetRoutesIfIsPageAction(Microsoft.AspNetCore.Mvc.Abstractions.ActionDescriptor action)
	{
		var route = action as Microsoft.AspNetCore
			.Mvc.RazorPages.CompiledPageActionDescriptor;

		if (route is null)
		{
			return null;
		}

		var result = new System.Collections
			.Generic.List<RouteInformation>();

		foreach (var handlerMethod in route.HandlerMethods)
		{
			var routeInformation =
				new RouteInformation
				{
					HttpMethod = handlerMethod.HttpMethod,

					AreaName = route.AreaName,

					Path = route.DisplayName,

					Template = route.AttributeRouteInfo?.Template,
					//Template = route.AttributeRouteInfo.Template,

					MethodName = handlerMethod.MethodInfo.Name,

					ClassFullName = route.HandlerTypeInfo.FullName,
				};

			result.Add(item: routeInformation);
		}

		return result;
	}
}
