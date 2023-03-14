using Dtat;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Server.Pages;

public class IndexModel :
	Infrastructure.BasePageModelWithDatabaseContext
{
	public IndexModel
		(Persistence.DatabaseContext databaseContext) : base(databaseContext: databaseContext)
	{
	}

	public async System.Threading.Tasks.Task
		<Microsoft.AspNetCore.Mvc.IActionResult> OnGetAsync(string? culture = null)
	{
		// **************************************************
		culture = culture.Fix();

		if (culture is null)
		{
			var currentUICultureName = Domain.Features
				.Common.CultureEnumHelper.GetCurrentUICultureName();

			var url =
				$"{Constants.CommonRouting.RootIndex}/{currentUICultureName}/";

			return RedirectPermanent(url: url);
		}

		culture = culture.Replace
			(oldValue: " ", newValue: string.Empty);

		var foundedCulture =
			await
			DatabaseContext.Cultures
			.Where(current => current.CultureName.ToLower() == culture.ToLower())
			.FirstOrDefaultAsync();

		if (foundedCulture is null)
		{
			return RedirectToPage(pageName:
				Constants.CommonRouting.BadRequest);
		}

		if (foundedCulture.IsActive == false)
		{
			return RedirectToPage(pageName:
				Constants.CommonRouting.NotFound);
		}
		// **************************************************

		return Page();
	}
}
