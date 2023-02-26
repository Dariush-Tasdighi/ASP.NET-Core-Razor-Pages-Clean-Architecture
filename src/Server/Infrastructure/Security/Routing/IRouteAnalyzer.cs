namespace Infrastructure.Security.Routing;

public interface IRouteAnalyzer
{
	System.Collections.Generic.IList<RouteInformation> GetAllRouteInformations();
}
