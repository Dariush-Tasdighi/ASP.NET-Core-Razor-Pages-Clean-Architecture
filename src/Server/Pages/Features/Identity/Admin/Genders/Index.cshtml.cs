using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Server.Pages.Features.Identity.Admin.Genders;

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

	public System.Collections.Generic.List<ViewModels.Pages.Features.Identity.Admin.Genders.IndexItemViewModel> ViewModel { get; private set; }

	#endregion /Properties

	#region Methods

	#region OnGetAsync()
	public async System.Threading.Tasks.Task OnGetAsync()
	{
		var currentUICultureLcid = Domain.Features
			.Common.CultureEnumHelper.GetCurrentUICultureLcid();

		ViewModel =
			await
			DatabaseContext.Genders

			.Where(current => current.Culture != null &&
				current.Culture.Lcid == currentUICultureLcid)

			.OrderBy(current => current.Ordering)
			.ThenBy(current => current.Code)

			.Select(current => new ViewModels.Pages
				.Features.Identity.Admin.Genders.IndexItemViewModel
			{
				Id = current.Id,

				Title = current.Title,
				Prefix = current.Prefix,

				IsActive = current.IsActive,

				Code = current.Code,
				Ordering = current.Ordering,

				UserCount = current.UserProfiles.Count,

				InsertDateTime = current.InsertDateTime,
				UpdateDateTime = current.UpdateDateTime,
			})
			.ToListAsync()
			;
	}
	#endregion /OnGetAsync()

	#endregion /Methods
}
