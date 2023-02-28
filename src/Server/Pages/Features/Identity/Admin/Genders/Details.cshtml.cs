using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Server.Pages.Features.Identity.Admin.Genders;

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

	public ViewModels.Pages.Features.Identity.Admin.Genders.DetailsOrDeleteViewModel ViewModel { get; private set; }

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
			DatabaseContext.Genders

			.Where(current => current.Id == id.Value)

			.Select(current => new ViewModels.Pages.Features
				.Identity.Admin.Genders.DetailsOrDeleteViewModel()
			{
				Id = current.Id,
				Code = current.Code,
				Title = current.Title,
				Prefix = current.Prefix,
				IsActive = current.IsActive,
				Ordering = current.Ordering,
				Description = current.Description,
				UserCount = current.UserProfiles.Count,
				InsertDateTime = current.InsertDateTime,
				UpdateDateTime = current.UpdateDateTime,
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
