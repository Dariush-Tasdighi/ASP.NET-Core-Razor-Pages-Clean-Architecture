using Dtat;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Server.Pages.Features.Cms.Admin.SubMenuItems;

[Microsoft.AspNetCore.Authorization
	.Authorize(Roles = Constants.Role.Administrator)]
public class CreateModel :
	Infrastructure.BasePageModelWithDatabaseContext
{
	#region Constructor
	public CreateModel
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
	public ViewModels.Pages.Features.Cms.Admin.MenuItems.CreateViewModel ViewModel { get; set; }

	#endregion /Properties

	#region Methods

	#region OnGetAsync()
	public async System.Threading.Tasks.Task
		<Microsoft.AspNetCore.Mvc.IActionResult> OnGetAsync(System.Guid? parentId)
	{
		// **************************************************
		if (parentId is null)
		{
			return RedirectToPage(pageName:
				Constants.CommonRouting.BadRequest);
		}

		var parentMenuItem =
			await
			DatabaseContext.MenuItems
			.Where(current => current.Id == parentId.Value)
			.FirstOrDefaultAsync();

		if (parentMenuItem is null)
		{
			return RedirectToPage(pageName:
				Constants.CommonRouting.BadRequest);
		}

		ParentMenuItem = parentMenuItem;
		// **************************************************

		return Page();
	}
	#endregion /OnGetAsync()

	#region OnPostAsync()
	public async System.Threading.Tasks.Task
		<Microsoft.AspNetCore.Mvc.IActionResult> OnPostAsync(System.Guid? parentId)
	{
		// **************************************************
		if (parentId is null)
		{
			return RedirectToPage(pageName:
				Constants.CommonRouting.BadRequest);
		}

		var parentMenuItem =
			await
			DatabaseContext.MenuItems
			.Where(current => current.Id == parentId.Value)
			.FirstOrDefaultAsync();

		if (parentMenuItem is null)
		{
			return RedirectToPage(pageName:
				Constants.CommonRouting.BadRequest);
		}

		ParentMenuItem = parentMenuItem;
		// **************************************************

		if (ModelState.IsValid == false)
		{
			return Page();
		}

		// **************************************************
		var currentUICultureLcid = Domain.Features
			.Common.CultureEnumHelper.GetCurrentUICultureLcid();

		var currentCulture =
			await
			DatabaseContext.Cultures
			.Where(current => current.Lcid == currentUICultureLcid)
			.FirstOrDefaultAsync();

		if (currentCulture is null)
		{
			return RedirectToPage(pageName:
				Constants.CommonRouting.InternalServerError);
		}
		// **************************************************

		// **************************************************
		ViewModel.Title =
			ViewModel.Title.Fix()!;

		//ViewModel.Title =
		//	ViewModel.Title.Fix();

		ViewModel.Description =
			ViewModel.Description.Fix();

		ViewModel.NavigationUrl =
			ViewModel.NavigationUrl.Fix();

		var newEntity =
			new Domain.Features.Cms.MenuItem
			(cultureId: currentCulture.Id, title: ViewModel.Title)
			{
				ParentId = parentId.Value,

				Ordering = ViewModel.Ordering,

				Description = ViewModel.Description,
				NavigationUrl = ViewModel.NavigationUrl,

				IsVisible = ViewModel.IsVisible,
				IsDisabled = ViewModel.IsDisabled,
				OpenUrlInNewWindow = ViewModel.OpenUrlInNewWindow,
			};

		var entityEntry =
			await
			DatabaseContext.AddAsync(entity: newEntity);

		var affectedRows =
			await
			DatabaseContext.SaveChangesAsync();
		// **************************************************

		// **************************************************
		var successMessage = string.Format
			(Resources.Messages.Successes.Created,
			Resources.DataDictionary.MenuItem);

		AddToastSuccess(message: successMessage);
		// **************************************************

		return RedirectToPage(pageName:
			"Index", routeValues: new { parentId });
	}
	#endregion /OnPostAsync()

	#endregion /Methods
}
