using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Server.Pages;

public class ChangeCultureModel : Infrastructure.BasePageModel
{
	public ChangeCultureModel
		(Persistence.DatabaseContext databaseContext) : base()
	{
		DatabaseContext = databaseContext;
	}

	private Persistence.DatabaseContext DatabaseContext { get; }

	public async System.Threading.Tasks.Task
		<Microsoft.AspNetCore.Mvc.IActionResult> OnGet(string? cultureName)
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
		// **************************************************
		// **************************************************
		var defaultCultureName = "fa-IR";

		var applicationSetting =
			await
			DatabaseContext.ApplicationSettings
			.FirstOrDefaultAsync();

		if (applicationSetting is not null)
		{
			if (applicationSetting.DefaultCulture is not null)
			{
				defaultCultureName =
					applicationSetting.DefaultCulture.CultureName;
			}
		}

		var supportedCultureNames =
			await
			DatabaseContext.Cultures
			.Where(current => current.IsActive)
			.Select(current => current.CultureName)
			.ToListAsync()
			;
		// **************************************************
		// **************************************************
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
