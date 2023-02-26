using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Server.Pages.Features.Cms.Admin.SubMenuItems;

[Microsoft.AspNetCore.Authorization
	.Authorize(Roles = Constants.Role.Administrator)]
public class DetailsModel :
	Infrastructure.BasePageModelWithDatabaseContext
{
	#region Constructor
	public DetailsModel
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

	public ViewModels.Pages.Features.Cms.Admin.MenuItems.DetailsOrDeleteViewModel ViewModel { get; private set; }

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

		ParentMenuItem =
			foundedItem.Parent;
		// **************************************************

		ViewModel =
			new ViewModels.Pages.Features.Cms.Admin.MenuItems.DetailsOrDeleteViewModel()
			{
				Id = foundedItem.Id,
				Title = foundedItem.Title,
				Ordering = foundedItem.Ordering,
				IsVisible = foundedItem.IsVisible,
				IsDisabled = foundedItem.IsDisabled,
				Description = foundedItem.Description,
				ChildCount = foundedItem.Children.Count,
				NavigationUrl = foundedItem.NavigationUrl,
				InsertDateTime = foundedItem.InsertDateTime,
				UpdateDateTime = foundedItem.UpdateDateTime,
				OpenUrlInNewWindow = foundedItem.OpenUrlInNewWindow,
			};

		return Page();
	}
	#endregion /OnGetAsync()

	#endregion /Methods
}
