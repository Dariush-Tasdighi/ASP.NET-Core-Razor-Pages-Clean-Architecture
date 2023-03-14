using Dtat;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Server.Pages.Features.Cms;

public class PageModel :
	Infrastructure.BasePageModelWithDatabaseContext
{
	#region Constructor
	public PageModel
		(Persistence.DatabaseContext databaseContext) :
		base(databaseContext: databaseContext)
	{
		ViewModel = new();
	}
	#endregion /Constructor

	#region Properties
	public ViewModels.Pages.Features.Cms.PageViewModel ViewModel { get; set; }
	#endregion /Properties

	#region Methods

	#region OnGetAsync()
	public async System.Threading.Tasks.Task
		<Microsoft.AspNetCore.Mvc.IActionResult> OnGetAsync
		(string? culture = null, string? name = null)
	{
		// **************************************************
		culture = culture.Fix();

		if (culture is null)
		{
			return RedirectToPage(pageName:
				Constants.CommonRouting.BadRequest);
		}

		culture = culture.Replace
			(oldValue: " ", newValue: string.Empty);
		// **************************************************

		// **************************************************
		name = name.Fix();

		if (name is null)
		{
			return RedirectToPage(pageName:
				Constants.CommonRouting.BadRequest);
		}

		name = name.Replace
			(oldValue: " ", newValue: string.Empty);
		// **************************************************

		// **************************************************
		// دارد SEO‌ روش و نگاه ذیل مشکل
		// **************************************************
		//var currentUICulture =
		//	System.Globalization.CultureInfo.CurrentUICulture;
		// **************************************************

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

		// **************************************************
		var foundedPage =
			await
			DatabaseContext.Pages

			.Include(current => current.Layout)

			.Where(current => current.CultureId  == foundedCulture.Id)
			.Where(current => current.Name.ToLower() == name.ToLower())

			.FirstOrDefaultAsync();

		if (foundedPage is null)
		{
			return RedirectToPage(pageName:
				Constants.CommonRouting.NotFound);
		}

		if (foundedPage.IsActive == false)
		{
			return RedirectToPage(pageName:
				Constants.CommonRouting.NotFound);
		}
		// **************************************************

		// **************************************************
		foundedPage.Hits++;

		await DatabaseContext.SaveChangesAsync();
		// **************************************************

		// **************************************************
		ViewModel =
			new ViewModels.Pages.Features.Cms.PageViewModel
			{
				Title = foundedPage.Title,
				LayoutName = foundedPage.Layout!.Name,

				Body = foundedPage.Body,
 				Description = foundedPage.Description,

				UpdateDateTime = foundedPage.UpdateDateTime,
			};
		// **************************************************

		return Page();
	}
	#endregion /OnGetAsync()

	#endregion /Methods
}
