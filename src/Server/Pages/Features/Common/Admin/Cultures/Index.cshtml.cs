using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Server.Pages.Features.Common.Admin.Cultures;

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

	public System.Collections.Generic.List<ViewModels.Pages.Features.Common.Admin.Cultures.IndexItemViewModel> ViewModel { get; set; }

	#endregion /Properties

	#region Methods

	#region OnGetAsync()
	public async System.Threading.Tasks.Task OnGetAsync()
	{
		ViewModel =
			await
			DatabaseContext.Cultures

			.OrderByDescending(current => current.UpdateDateTime)

			.Select(current => new ViewModels.Pages
				.Features.Common.Admin.Cultures.IndexItemViewModel
			{
				Id = current.Id,
				Lcid = current.Lcid,
				IsActive = current.IsActive,

				NativeName = current.NativeName,
				CultureName = current.CultureName,

				UpdateDateTime = current.UpdateDateTime,
				InsertDateTime = current.InsertDateTime,

				PageCount = current.Pages.Count,
				PostCount = current.Posts.Count,
				SlideCount = current.Slides.Count,
				MenuItemCount = current.MenuItems.Count,
				PostCategoryCount = current.PostCategories.Count,
			})
			.ToListAsync()
			;
	}
	#endregion /OnGetAsync()

	#endregion /Methods
}
