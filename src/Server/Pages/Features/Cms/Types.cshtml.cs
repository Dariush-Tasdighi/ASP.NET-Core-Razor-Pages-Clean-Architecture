using Dtat;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Server.Pages.Features.Cms;

public class TypesModel :
	Infrastructure.BasePageModelWithDatabaseContext
{
	#region Constructor
	public TypesModel
		(Persistence.DatabaseContext databaseContext) :
		base(databaseContext: databaseContext)
	{
		CultureId = new System.Guid();
	}
	#endregion /Constructor

	#region Properties
	public System.Guid CultureId { get; set; }
	#endregion /Properties

	#region Methods

	#region OnGetAsync()
	public async System.Threading.Tasks.Task
		<Microsoft.AspNetCore.Mvc.IActionResult> OnGetAsync(string? culture = null)
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
		var foundedCulture =
			await
			DatabaseContext.Cultures

			.Where(current => current.CultureName.ToLower() == culture.ToLower())

			.FirstOrDefaultAsync();

		if (foundedCulture is null)
		{
			return RedirectToPage(pageName:
				Constants.CommonRouting.NotFound);
		}

		if (foundedCulture.IsActive == false)
		{
			return RedirectToPage(pageName:
				Constants.CommonRouting.NotFound);
		}
		// **************************************************

		CultureId = foundedCulture.Id;

		return Page();
	}
	#endregion /OnGetAsync()

	#endregion /Methods
}
