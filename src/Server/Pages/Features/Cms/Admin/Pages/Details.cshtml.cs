using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Server.Pages.Features.Cms.Admin.Pages;

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

	public ViewModels.Pages.Features.Cms.Admin.Pages.DetailsOrDeleteViewModel ViewModel { get; private set; }

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
			DatabaseContext.Pages
			.Where(current => current.Id == id.Value)
			.Select(current => new ViewModels.Pages
				.Features.Cms.Admin.Pages.DetailsOrDeleteViewModel()
			{
				Id = current.Id,
				Body = current.Body,
				Name = current.Name,
				Title = current.Title,
				IsActive = current.IsActive,
				LayoutId = current.LayoutId,
				Ordering = current.Ordering,
				Description = current.Description,
				InsertDateTime = current.InsertDateTime,
				UpdateDateTime = current.UpdateDateTime,
				LayoutDisplayName = current.Layout!.DisplayName,
				DoesSearchEnginesIndexIt = current.DoesSearchEnginesIndexIt,
				DoesSearchEnginesFollowIt = current.DoesSearchEnginesFollowIt,
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
