using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Server.Pages.Features.Cms.Admin.SubMenuItems;

[Microsoft.AspNetCore.Authorization
	.Authorize(Roles = Constants.Role.Administrator)]
public class IndexModel :
	Infrastructure.BasePageModelWithDatabaseContext
{
	#region Constructor
	public IndexModel
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

	public System.Collections.Generic.List<ViewModels.Pages.Features.Cms.Admin.MenuItems.IndexItemViewModel> ViewModel { get; private set; }

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
				Constants.CommonRouting.NotFound);
		}

		ParentMenuItem = parentMenuItem;
		// **************************************************

		ViewModel =
			await
			DatabaseContext.MenuItems

			.Where(current => current.ParentId == ParentMenuItem.Id)

			.OrderBy(current => current.Ordering)
			.ThenBy(current => current.Title)

			.Select(current => new ViewModels.Pages.Features.Cms.Admin.MenuItems.IndexItemViewModel
			{
				Id = current.Id,

				Ordering = current.Ordering,
				ChildCount = current.Children.Count,

				Title = current.Title,
				NavigationUrl = current.NavigationUrl,

				InsertDateTime = current.InsertDateTime,
				UpdateDateTime = current.UpdateDateTime,

				IsVisible = current.IsVisible,
				IsDisabled = current.IsDisabled,
				OpenUrlInNewWindow = current.OpenUrlInNewWindow,
			})
			.ToListAsync()
			;

		return Page();
	}
	#endregion /OnGetAsync()

	#endregion /Methods
}
