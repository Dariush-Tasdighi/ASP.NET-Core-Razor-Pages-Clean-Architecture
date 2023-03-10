using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Server.Pages.Features.Cms.Admin.PostCategories;

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

	public ViewModels.Pages.Features.Cms.Admin.PostCategories.DetailsOrDeleteViewModel ViewModel { get; private set; }

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
			DatabaseContext.PostCategories
			.Where(current => current.Id == id.Value)
			.Select(current => new ViewModels.Pages.Features.Cms.Admin.PostCategories.DetailsOrDeleteViewModel()
			{
				Id = current.Id,
				Body = current.Body,
				Hits = current.Hits,
				Name = current.Name,
				Title = current.Title,
				ImageUrl = current.ImageUrl,
				IsActive = current.IsActive,
				Ordering = current.Ordering,
				PostCount = current.Posts.Count,
				Description = current.Description,
				CoverImageUrl = current.CoverImageUrl,
				InsertDateTime = current.InsertDateTime,
				UpdateDateTime = current.UpdateDateTime,
				DisplayInHomePage = current.DisplayInHomePage,
				MaxPostCountInHomePage = current.MaxPostCountInHomePage,
				MaxPostCountInMainPage = current.MaxPostCountInMainPage,
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
