using Dtat;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Server.Pages.Features.Common.Admin.ApplicationSettings;

[Microsoft.AspNetCore.Authorization
	.Authorize(Roles = Constants.Role.Administrator)]
public class UpdateModel :
	Infrastructure.BasePageModelWithDatabaseContext
{
	#region Constructor
	public UpdateModel
		(Persistence.DatabaseContext databaseContext) :
		base(databaseContext: databaseContext)
	{
		ViewModel = new();
	}
	#endregion /Constructor

	#region Properties

	[Microsoft.AspNetCore.Mvc.BindProperty]
	public ViewModels.Pages.Features.Common.Admin.ApplicationSettings.UpdateViewModel ViewModel { get; set; }

	#endregion /Properties

	#region Methods

	#region OnGetAsync()
	public async System.Threading.Tasks.Task
		<Microsoft.AspNetCore.Mvc.IActionResult> OnGetAsync()
	{
		var result =
			await
			DatabaseContext.ApplicationSettings

			.Select(current => new ViewModels.Pages
				.Features.Common.Admin.ApplicationSettings.UpdateViewModel()
			{
				Id = current.Id,

				MasterPassword = current.MasterPassword,
				GoogleAnalyticsCode = current.GoogleAnalyticsCode,
				GoogleTagManagerCode = current.GoogleTagManagerCode,

				IsSslEnabled = current.IsSslEnabled,
				IsLoginVisible = current.IsLoginVisible,
				IsRegistrationEnabled = current.IsRegistrationEnabled,

				FavoriteUserProfileLevel = current.FavoriteUserProfileLevel,
			})
			.FirstOrDefaultAsync();

		if (result is null)
		{
			return RedirectToPage(pageName:
				Constants.CommonRouting.NotFound);
		}

		ViewModel = result;

		return Page();
	}
	#endregion /OnGetAsync()

	#region OnPostAsync()
	public async System.Threading.Tasks.Task
		<Microsoft.AspNetCore.Mvc.IActionResult> OnPostAsync()
	{
		if (ModelState.IsValid == false)
		{
			return Page();
		}

		// **************************************************
		var foundedItem =
			await
			DatabaseContext.ApplicationSettings

			.FirstOrDefaultAsync();

		if (foundedItem is null)
		{
			return RedirectToPage(pageName:
				Constants.CommonRouting.NotFound);
		}
		// **************************************************

		// **************************************************
		foundedItem.SetUpdateDateTime();

		foundedItem.IsLoginVisible = ViewModel.IsLoginVisible;
		foundedItem.IsSslEnabled = ViewModel.IsSslEnabled;
		foundedItem.IsRegistrationEnabled = ViewModel.IsRegistrationEnabled;

		foundedItem.MasterPassword = ViewModel.MasterPassword.Fix();
		foundedItem.GoogleAnalyticsCode = ViewModel.GoogleAnalyticsCode.Fix();
		foundedItem.GoogleTagManagerCode = ViewModel.GoogleTagManagerCode.Fix();

		foundedItem.FavoriteUserProfileLevel = ViewModel.FavoriteUserProfileLevel;
		// **************************************************

		await DatabaseContext.SaveChangesAsync();

		// **************************************************
		var successMessage = string.Format
			(format: Resources.Messages.Successes.Updated,
			arg0: Resources.DataDictionary.Data);

		AddToastSuccess(message: successMessage);
		// **************************************************

		return Page();
	}
	#endregion /OnPostAsync()

	#endregion /Methods
}
