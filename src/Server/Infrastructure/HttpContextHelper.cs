using Azure.Core;

namespace Infrastructure;

/// <summary>
/// https://www.sistrix.com/ask-sistrix/technical-seo/site-structure/what-is-the-difference-between-a-url-domain-subdomain-hostname-etc
/// </summary>
public static class HttpContextHelper : object
{
	static HttpContextHelper()
	{
	}

	public static string? GetRemoteIpAddress
		(Microsoft.AspNetCore.Http.HttpContext? httpContext)
	{
		if (httpContext is null)
		{
			return null;
		}

		if (httpContext.Request is null)
		{
			return null;
		}

		var remoteIpAddress =
			httpContext.Request.HttpContext.Connection.RemoteIpAddress;

		var result =
			remoteIpAddress?.ToString();

		return result;
	}

	public static string? GetCurrentHostName
		(Microsoft.AspNetCore.Http.HttpContext? httpContext)
	{
		if (httpContext is null)
		{
			return null;
		}

		if (httpContext.Request is null)
		{
			return null;
		}

		var result =
			httpContext.Request.Host.Value;

		return result;
	}

	public static string? GetCurrentHostProtocol
		(Microsoft.AspNetCore.Http.HttpContext? httpContext)
	{
		if (httpContext is null)
		{
			return null;
		}

		if (httpContext.Request is null)
		{
			return null;
		}

		var result =
			httpContext.Request.Scheme;

		return result;
	}

	public static string? GetCurrentHostUrl
		(Microsoft.AspNetCore.Http.HttpContext? httpContext)
	{
		var currentDomainName =
			GetCurrentHostName(httpContext: httpContext);

		if (currentDomainName is null)
		{
			return null;
		}

		var currentSiteScheme =
			GetCurrentHostProtocol(httpContext: httpContext);

		if (currentSiteScheme is null)
		{
			return null;
		}

		var result =
			$"{currentSiteScheme}://{currentDomainName}";

		return result;
	}
}
