using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Server.Pages.Features.Cms.Admin.SubMenuItems;

[Microsoft.AspNetCore.Authorization
	.Authorize(Roles = Constants.Role.Administrator)]
public class DeleteModel :
	Infrastructure.BasePageModelWithDatabaseContext
{
	#region Constructor
	public DeleteModel
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
	public ViewModels.Pages.Features.Cms.Admin.MenuItems.DetailsOrDeleteViewModel ViewModel { get; private set; }

	#endregion /Properties

	#region Methods

	#region OnGetAsync()
	public async System.Threading.Tasks.Task
		<Microsoft.AspNetCore.Mvc.IActionResult> OnGetAsync(System.Guid? id)
	{
		if (id is null)
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

		if (foundedItem.Parent is null)
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

	#region OnPostAsync()
	public async System.Threading.Tasks.Task
		<Microsoft.AspNetCore.Mvc.IActionResult> OnPostAsync(System.Guid? id)
	{
		if (id is null)
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
		// **************************************************

		// **************************************************
		var hasAnyChildren =
			await
			DatabaseContext.MenuItems
			.Where(current => current.ParentId != null && current.ParentId == id.Value)
			.AnyAsync();

		if (hasAnyChildren)
		{
			// **************************************************
			var errorMessage = string.Format
				(format: Resources.Messages.Errors.CascadeDelete,
				arg0: Resources.DataDictionary.MenuItem);

			AddToastError(message: errorMessage);
			// **************************************************

			return RedirectToPage(pageName:
				Constants.CommonRouting.CurrentIndex);
		}
		// **************************************************

		// **************************************************
		var entityEntry =
			DatabaseContext.Remove(entity: foundedItem);

		var affectedRows =
			await
			DatabaseContext.SaveChangesAsync();
		// **************************************************

		// **************************************************
		var successMessage = string.Format
			(format: Resources.Messages.Successes.Deleted,
			arg0: Resources.DataDictionary.MenuItem);

		AddToastSuccess(message: successMessage);
		// **************************************************

		return RedirectToPage(pageName:
			Constants.CommonRouting.CurrentIndex,
			routeValues: new { foundedItem.ParentId });
	}
	#endregion /OnPostAsync()

	#endregion /Methods
}
