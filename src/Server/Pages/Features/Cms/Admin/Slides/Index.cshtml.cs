using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Server.Pages.Features.Cms.Admin.Slides;

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

	public System.Collections.Generic.List<ViewModels.Pages.Features.Cms.Admin.Slides.IndexItemViewModel> ViewModel { get; set; }

	#endregion /Properties

	#region Methods

	#region OnGetAsync()
	public async System.Threading.Tasks.Task OnGetAsync()
	{
		var currentUICultureLcid = Domain.Features
			.Common.CultureEnumHelper.GetCurrentUICultureLcid();

		ViewModel =
			await
			DatabaseContext.Slides

			.Where(current => current.Culture != null
				&& current.Culture.Lcid == currentUICultureLcid)

			.OrderByDescending(current => current.IsActive)
			.ThenBy(current => current.Ordering)
			.ThenBy(current => current.Title)

			.Select(current => new ViewModels.Pages.Features.Cms.Admin.Slides.IndexItemViewModel
			{
				Id = current.Id,

				IsActive = current.IsActive,
				OpenUrlInNewWindow = current.OpenUrlInNewWindow,

				Interval = current.Interval,
				Ordering = current.Ordering,

				Title = current.Title,
				ImageUrl = current.ImageUrl,
				NavigationUrl = current.NavigationUrl,

				UpdateDateTime = current.UpdateDateTime,
				InsertDateTime = current.InsertDateTime,
				PublishStartDateTime = current.PublishStartDateTime,
				PublishFinishDateTime = current.PublishFinishDateTime,
			})
			.ToListAsync()
			;
	}
	#endregion /OnGetAsync()

	#endregion /Methods
}
