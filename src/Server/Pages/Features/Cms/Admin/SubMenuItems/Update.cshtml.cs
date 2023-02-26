using Dtat;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Server.Pages.Features.Cms.Admin.SubMenuItems;

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

		// یک دستور الکی برای جلوگیری از اخطار ویژوال استودیو
		ParentMenuItem =
			new(cultureId: new System.Guid(), title: string.Empty);
	}
	#endregion /Constructor

	#region Properties

	public Domain.Features.Cms.MenuItem ParentMenuItem { get; private set; }

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

		// **************************************************
		var foundedItem =
			await
			DatabaseContext.MenuItems
			.Include(current => current.Parent)
			.Where(current => current.Id == id.Value)
			.FirstOrDefaultAsync();

		if (foundedItem is null)
		{
			return RedirectToPage(pageName:
				Constants.CommonRouting.NotFound);
		}

		if(foundedItem.Parent is null)
		{
			return RedirectToPage(pageName:
				Constants.CommonRouting.InternalServerError);
		}

		ParentMenuItem = foundedItem.Parent;
		// **************************************************

		ViewModel =
			new ViewModels.Pages.Features.Cms.Admin.MenuItems.UpdateViewModel()
			{
				Id = foundedItem.Id,
				Title = foundedItem.Title,
				Ordering = foundedItem.Ordering,
				IsVisible = foundedItem.IsVisible,
				IsDisabled = foundedItem.IsDisabled,
				Description = foundedItem.Description,
				NavigationUrl = foundedItem.NavigationUrl,
				OpenUrlInNewWindow = foundedItem.OpenUrlInNewWindow,
			};

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

		return RedirectToPage(pageName: "Index",
			routeValues: new { foundedItem.ParentId });
	}
	#endregion /OnPostAsync()

	#endregion /Methods
}
