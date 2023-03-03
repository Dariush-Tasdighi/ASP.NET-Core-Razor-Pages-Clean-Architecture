using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Server.Pages.Features.Identity.Admin.Roles;

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
	}
	#endregion /Constructor

	#region Properties

	public System.Collections.Generic.List<ViewModels.Pages.Features.Identity.Admin.Roles.IndexItemViewModel> ViewModel { get; private set; }

	#endregion /Properties

	#region Methods

	#region OnGetAsync()
	public async System.Threading.Tasks.Task
		<Microsoft.AspNetCore.Mvc.IActionResult> OnGetAsync()
	{
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
				Constants.CommonRouting.NotFound);
		}
		// **************************************************

		ViewModel =
			await
			DatabaseContext.Roles

			.OrderBy(current => current.Ordering)
			.ThenBy(current => current.Code)

			.Select(current => new ViewModels.Pages
				.Features.Identity.Admin.Roles.IndexItemViewModel
			{
				Id = current.Id,

				Name = current.Name,

				IsActive = current.IsActive,

				Code = current.Code,
				Ordering = current.Ordering,
				UserCount = current.Users.Count,

				InsertDateTime = current.InsertDateTime,
				UpdateDateTime = current.UpdateDateTime,

#pragma warning disable CS8602

				Title = current.LocalizedRoles.FirstOrDefault
					(other => other.CultureId == currentCulture.Id).Title,

#pragma warning restore CS8602

			})
			.ToListAsync()
			;

		return Page();
	}
	#endregion /OnGetAsync()

	#endregion /Methods
}
