using Domain.Features.Identity.Enums;

namespace Infrastructure.Security.Routing;

public class RouteInformation : object
{
	#region Constructor
	public RouteInformation() : base()
	{
		Path = string.Empty;
		Template = string.Empty;
		MethodName= string.Empty;
		HttpMethod = string.Empty;
		ClassFullName = string.Empty;

		AccessType =
			AccessTypeEnum.Special;
	}
	#endregion /Constructor

	#region Properties

	#region public string HttpMethod { get; set; }
	public string HttpMethod { get; set; }
	#endregion /public string HttpMethod { get; set; }

	#region public string? AreaName { get; set; }
	public string? AreaName { get; set; }
	#endregion /public string? AreaName { get; set; }

	#region public string? Path { get; set; }
	public string? Path { get; set; }
	#endregion /public string? Path { get; set; }

	#region public string? Template { get; set; }
	public string? Template { get; set; }
	#endregion /public string? Template { get; set; }

	#region public string MethodName { get; set; }
	public string MethodName { get; set; }
	#endregion /public string MethodName { get; set; }

	#region public string? ClassFullName { get; set; }
	public string? ClassFullName { get; set; }
	#endregion /public string? ClassFullName { get; set; }

	#region public Domain.Enumerations.AccessType AccessType { get; set; }
	public AccessTypeEnum AccessType { get; set; }
	#endregion /public Domain.Enumerations.AccessType AccessType { get; set; }

	#endregion /Properties
}
