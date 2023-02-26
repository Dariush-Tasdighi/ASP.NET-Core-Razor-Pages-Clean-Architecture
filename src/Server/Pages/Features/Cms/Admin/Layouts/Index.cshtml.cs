using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Server.Pages.Features.Cms.Admin.Layouts;

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

	public System.Collections.Generic.List<ViewModels.Pages.Features.Cms.Admin.Layouts.IndexItemViewModel> ViewModel { get; set; }

	#endregion /Properties

	#region Methods

	#region OnGetAsync()
	public async System.Threading.Tasks.Task OnGetAsync()
	{
		ViewModel =
			await
			DatabaseContext.Layouts

			.OrderByDescending(current => current.UpdateDateTime)

			.Select(current => new ViewModels.Pages.Features.Cms.Admin.Layouts.IndexItemViewModel
			{
				Id = current.Id,

				Name = current.Name,
				Title = current.Title,

				IsActive = current.IsActive,

				PageCount = current.Pages.Count,

				UpdateDateTime = current.UpdateDateTime,
				InsertDateTime = current.InsertDateTime,
			})
			.ToListAsync()
			;
	}
	#endregion /OnGetAsync()

	#endregion /Methods
}
