using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Server.Pages.Features.Cms.Admin.PostTypes;

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

	public System.Collections.Generic.List<ViewModels.Pages.Features.Cms.Admin.PostTypes.IndexItemViewModel> ViewModel { get; private set; }

	#endregion /Properties

	#region Methods

	#region OnGetAsync()
	public async System.Threading.Tasks.Task OnGetAsync()
	{
		var currentUICultureLcid = Domain.Features
			.Common.CultureEnumHelper.GetCurrentUICultureLcid();

		ViewModel =
			await
			DatabaseContext.PostTypes

			.Where(current => current.Culture != null
				&& current.Culture.Lcid == currentUICultureLcid)

			.OrderBy(current => current.Ordering)
			.ThenBy(current => current.Name)

			.Select(current => new ViewModels.Pages
				.Features.Cms.Admin.PostTypes.IndexItemViewModel
			{
				Id = current.Id,

				Name = current.Name,
				Title = current.Title,

				InsertDateTime = current.InsertDateTime,
				UpdateDateTime = current.UpdateDateTime,

				IsActive = current.IsActive,
				DisplayInHomePage = current.DisplayInHomePage,

				Hits = current.Hits,
				Ordering = current.Ordering,
				PostCount = current.Posts.Count,
			})
			.ToListAsync()
			;
	}
	#endregion /OnGetAsync()

	#endregion /Methods
}
