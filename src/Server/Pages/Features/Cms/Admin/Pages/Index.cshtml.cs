using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Server.Pages.Features.Cms.Admin.Pages;

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

	public System.Collections.Generic.List<ViewModels.Pages.Features.Cms.Admin.Pages.IndexItemViewModel> ViewModel { get; set; }

	#endregion /Properties

	#region Methods

	#region OnGetAsync()
	public async System.Threading.Tasks.Task OnGetAsync()
	{
		var currentUICultureLcid = Domain.Features
			.Common.CultureEnumHelper.GetCurrentUICultureLcid();

		ViewModel =
			await
			DatabaseContext.Pages

			.Where(current => current.Culture != null
				&& current.Culture.Lcid == currentUICultureLcid)

			.OrderByDescending(current => current.UpdateDateTime)

			.Select(current => new ViewModels.Pages.Features.Cms.Admin.Pages.IndexItemViewModel
			{
				Id = current.Id,
				Hits = current.Hits,
				IsActive = current.IsActive,

				Name = current.Name,
				Title = current.Title,

				LayoutName = current.Layout!.Name,
				//LayoutName = current.Layout.Name,

				UpdateDateTime = current.UpdateDateTime,
				InsertDateTime = current.InsertDateTime,
			})
			.ToListAsync()
			;
	}
	#endregion /OnGetAsync()

	#endregion /Methods
}
