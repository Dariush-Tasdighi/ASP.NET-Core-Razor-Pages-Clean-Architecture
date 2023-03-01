using System.Linq;
using Microsoft.AspNetCore.Http;

namespace Server.Pages;

public class ChangeCultureModel : Infrastructure.BasePageModel
{
	public ChangeCultureModel
		(Infrastructure.Settings.ApplicationSettings applicationSettings) : base()
	{
		ApplicationSettings = applicationSettings;
	}

	private Infrastructure.Settings.ApplicationSettings ApplicationSettings { get; }

	public Microsoft.AspNetCore.Mvc.IActionResult OnGet(string? cultureName)
	{
		// **************************************************
		// using Microsoft.AspNetCore.Http;
		var typedHeaders =
			HttpContext.Request.GetTypedHeaders();

		var httpReferer =
			typedHeaders?.Referer?.AbsoluteUri;

		if (string.IsNullOrWhiteSpace(value: httpReferer))
		{
			return RedirectToPage(pageName:
				Constants.CommonRouting.RootIndex);
		}
		// **************************************************

		// **************************************************
		var defaultCultureName =
			ApplicationSettings.CultureSettings.DefaultCultureName;

		var supportedCultureNames =
			ApplicationSettings.CultureSettings.SupportedCultureNames?
			.ToList()
			;
		// **************************************************

		// **************************************************
		if (string.IsNullOrWhiteSpace(value: cultureName))
		{
			cultureName =
				defaultCultureName;
		}
		// **************************************************

		// **************************************************
		if (supportedCultureNames is null ||
			supportedCultureNames.Contains(item: cultureName!) == false)
		{
			cultureName =
				defaultCultureName;
		}
		// **************************************************

		// **************************************************
		Infrastructure.Middlewares
			.CultureCookieHandlerMiddleware
			.SetCulture(cultureName: cultureName);
		// **************************************************

		// **************************************************
		Infrastructure.Middlewares.CultureCookieHandlerMiddleware
			.CreateCookie(httpContext: HttpContext, cultureName: cultureName!);
		// **************************************************

		// **************************************************
		var cultureNameRoutingPart = $"/{cultureName}/".ToLower();

		if (httpReferer.ToLower().Contains(value: "/fa-ir/"))
		{
			httpReferer =
				httpReferer.ToLower()
				.Replace(oldValue: "/fa-ir/", newValue: cultureNameRoutingPart)
				;
		}
		else
		{
			if (httpReferer.ToLower().Contains(value: "/en-us/"))
			{
				httpReferer =
					httpReferer.ToLower()
					.Replace(oldValue: "/en-us/", newValue: cultureNameRoutingPart)
					;
			}
		}
		// **************************************************

		return Redirect(url: httpReferer);
	}
}
