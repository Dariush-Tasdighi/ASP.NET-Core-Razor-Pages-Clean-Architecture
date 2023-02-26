using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Server.Pages.Features.Cms.Admin.MenuItems;

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
	}
	#endregion /Constructor

	#region Properties

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

		var result =
			await
			DatabaseContext.MenuItems
			.Where(current => current.Id == id.Value)
			.Select(current => new ViewModels.Pages
			.Features.Cms.Admin.MenuItems.DetailsOrDeleteViewModel()
			{
				Id = current.Id,
				Title = current.Title,
				Ordering = current.Ordering,
				IsVisible = current.IsVisible,
				IsDisabled = current.IsDisabled,
				Description = current.Description,
				ChildCount = current.Children.Count,
				NavigationUrl = current.NavigationUrl,
				InsertDateTime = current.InsertDateTime,
				UpdateDateTime = current.UpdateDateTime,
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

	#endregion /Methods
}
