using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Server.Pages.Features.Cms.Admin.Posts;

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

	public ViewModels.Pages.Features.Cms.Admin.Posts.DetailsOrDeleteViewModel ViewModel { get; private set; }

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
			DatabaseContext.Posts
			.Where(current => current.Id == id.Value)
			.Select(current => new ViewModels.Pages
				.Features.Cms.Admin.Posts.DetailsOrDeleteViewModel()
			{
				Id = current.Id,
				Body = current.Body,
				Hits = current.Hits,
				Score = current.Score,
				Title = current.Title,
				Author = current.Author,
				IsDraft = current.IsDraft,
				ImageUrl = current.ImageUrl,
				IsActive = current.IsActive,
				Ordering = current.Ordering,
				IsDeleted = current.IsDeleted,
				CategoryId = current.CategoryId,
				IsFeatured = current.IsFeatured,
				Description = current.Description,
				Introduction = current.Introduction,
				AdminDescription = current.AdminDescription,
				IsCommentingEnabled = current.IsCommentingEnabled,
				DoesSearchEnginesIndexIt = current.DoesSearchEnginesIndexIt,
				DoesSearchEnginesFollowIt = current.DoesSearchEnginesFollowIt,
				DisplayCommentsAfterVerification = current.DisplayCommentsAfterVerification,

				UserId = current.UserId,
				CommentCount = current.Comments.Count,
				DeleteDateTime = current.DeleteDateTime,
				InsertDateTime = current.InsertDateTime,
				UpdateDateTime = current.UpdateDateTime,

				CategoryName = current.Category == null ? string.Empty : current.Category.Name,
				UserFullName = current.User == null ? string.Empty : current.User.EmailAddress,
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
