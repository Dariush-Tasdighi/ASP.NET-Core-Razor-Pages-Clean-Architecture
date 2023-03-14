using Dtat;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Server.Pages;

public class IndexModel :
	Infrastructure.BasePageModelWithDatabaseContext
{
	#region Constructor
	public IndexModel
		(Persistence.DatabaseContext databaseContext,
		Services.Features.Common.LocalizedApplicationSettingService localizedApplicationSettingService) : base(databaseContext: databaseContext)
	{
		LocalizedApplicationSettingService = localizedApplicationSettingService;
	}
	#endregion /Constructor

	#region Properties
	public Domain.Features.Common.LocalizedApplicationSetting? LocalizedApplicationSetting { get; set; }
	private Services.Features.Common.LocalizedApplicationSettingService LocalizedApplicationSettingService { get; }
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

		LocalizedApplicationSetting =
			await
			LocalizedApplicationSettingService.GetInstanceAsync();

		return Page();
	}
	#endregion /OnGetAsync()

	#endregion /Methods
}
