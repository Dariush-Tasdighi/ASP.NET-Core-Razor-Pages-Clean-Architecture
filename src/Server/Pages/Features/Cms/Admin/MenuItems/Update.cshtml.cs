using Dtat;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Server.Pages.Features.Cms.Admin.MenuItems;

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
	public ViewModels.Pages.Features.Cms.Admin.MenuItems.UpdateViewModel ViewModel { get; set; }

	#endregion /Properties

	#region Methods

	#region OnGetAsync()
	public async System.Threading.Tasks.Task
		<Microsoft.AspNetCore.Mvc.IActionResult> OnGetAsync(System.Guid? id)
	{
		if (id.HasValue == false)
		{
			return RedirectToPage(pageName:
				Constants.CommonRouting.BadRequest);
		}

		var result =
			await
			DatabaseContext.MenuItems
			.Where(current => current.Id == id.Value)
			.Select(current => new ViewModels.Pages
			.Features.Cms.Admin.MenuItems.UpdateViewModel()
			{
				Id = current.Id,
				Title = current.Title,
				Ordering = current.Ordering,
				IsVisible = current.IsVisible,
				IsDisabled = current.IsDisabled,
				Description = current.Description,
				NavigationUrl = current.NavigationUrl,
				OpenUrlInNewWindow = current.OpenUrlInNewWindow,
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
			DatabaseContext.MenuItems
			.Where(current => current.Id == ViewModel.Id)
			.FirstOrDefaultAsync();

		if (foundedItem is null)
		{
			return RedirectToPage(pageName:
				Constants.CommonRouting.NotFound);
		}
		// **************************************************

		// **************************************************
		ViewModel.Title =
			ViewModel.Title.Fix();

		ViewModel.Description =
			ViewModel.Description.Fix();

		ViewModel.NavigationUrl =
			ViewModel.NavigationUrl.Fix();

		foundedItem.SetUpdateDateTime();

		foundedItem.Title = ViewModel.Title!;
		//foundedItem.Title = ViewModel.Title;

		foundedItem.Ordering = ViewModel.Ordering;
		foundedItem.IsVisible = ViewModel.IsVisible;
		foundedItem.IsDisabled = ViewModel.IsDisabled;
		foundedItem.Description = ViewModel.Description;
		foundedItem.NavigationUrl = ViewModel.NavigationUrl;
		foundedItem.OpenUrlInNewWindow = ViewModel.OpenUrlInNewWindow;
		// **************************************************

		var affectedRows =
			await
			DatabaseContext.SaveChangesAsync();

		// **************************************************
		var successMessage = string.Format
			(format: Resources.Messages.Successes.Updated,
			arg0: Resources.DataDictionary.MenuItem);

		AddToastSuccess(message: successMessage);
		// **************************************************

		return RedirectToPage(pageName: "Index");
	}
	#endregion /OnPostAsync()

	#endregion /Methods
}
