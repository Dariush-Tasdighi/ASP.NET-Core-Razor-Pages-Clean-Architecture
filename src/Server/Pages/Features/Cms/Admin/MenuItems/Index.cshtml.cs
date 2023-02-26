using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Server.Pages.Features.Cms.Admin.MenuItems;

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

	public System.Collections.Generic.List<ViewModels.Pages.Features.Cms.Admin.MenuItems.IndexItemViewModel> ViewModel { get; private set; }

	#endregion /Properties

	#region Methods

	#region OnGetAsync()
	public async System.Threading.Tasks.Task OnGetAsync()
	{
		var currentUICultureLcid = Domain.Features
			.Common.CultureEnumHelper.GetCurrentUICultureLcid();

		ViewModel =
			await
			DatabaseContext.MenuItems

			.Where(current => current.ParentId == null)
			//.Where(current => current.ParentId is null)

			.Where(current => current.Culture != null
				&& current.Culture.Lcid == currentUICultureLcid)

			.OrderBy(current => current.Ordering)
			.ThenBy(current => current.Title)

			.Select(current => new ViewModels.Pages.Features.Cms.Admin.MenuItems.IndexItemViewModel
			{
				Id = current.Id,

				Ordering = current.Ordering,
				ChildCount = current.Children.Count,

				Title = current.Title,
				NavigationUrl = current.NavigationUrl,

				InsertDateTime = current.InsertDateTime,
				UpdateDateTime = current.UpdateDateTime,

				IsVisible = current.IsVisible,
				IsDisabled = current.IsDisabled,
				OpenUrlInNewWindow = current.OpenUrlInNewWindow,
			})
			.ToListAsync()
			;
	}
	#endregion /OnGetAsync()

	#endregion /Methods
}
