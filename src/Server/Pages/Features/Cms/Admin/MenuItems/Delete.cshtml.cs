using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Server.Pages.Features.Cms.Admin.MenuItems;

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
	}
	#endregion /Constructor

	#region Properties

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

		var result =
			await
			DatabaseContext.MenuItems
			.Where(current => current.Id == id.Value)
			.Select(current => new ViewModels.Pages.Features.Cms.Admin.MenuItems.DetailsOrDeleteViewModel
			{
				Id = current.Id,

				Ordering = current.Ordering,
				ChildCount = current.Children.Count,

				Title = current.Title,
				Description = current.Description,
				NavigationUrl = current.NavigationUrl,

				InsertDateTime = current.InsertDateTime,
				UpdateDateTime = current.UpdateDateTime,

				IsVisible = current.IsVisible,
				IsDisabled = current.IsDisabled,
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
			.Where(current => current.ParentId != null
				&& current.ParentId == id.Value)
			.AnyAsync();

		if (hasAnyChildren)
		{
			// **************************************************
			var errorMessage = string.Format
				(format: Resources.Messages.Errors.CascadeDelete,
				arg0: Resources.DataDictionary.MenuItem);

			AddToastError(message: errorMessage);
			// **************************************************

			return RedirectToPage(pageName: "Index");
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

		return RedirectToPage(pageName: "Index");
	}
	#endregion /OnPostAsync()

	#endregion /Methods
}
