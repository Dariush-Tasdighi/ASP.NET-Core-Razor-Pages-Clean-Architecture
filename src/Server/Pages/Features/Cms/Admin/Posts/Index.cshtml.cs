using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Server.Pages.Features.Cms.Admin.Posts;

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

	public System.Collections.Generic.List<ViewModels.Pages.Features.Cms.Admin.Posts.IndexItemViewModel> ViewModel { get; private set; }

	#endregion /Properties

	#region Methods

	#region OnGetAsync()
	public async System.Threading.Tasks.Task OnGetAsync()
	{
		var currentUICultureLcid = Domain.Features
			.Common.CultureEnumHelper.GetCurrentUICultureLcid();

		string? x = "ali";
		string? y = x?.Trim();

		ViewModel =
			await
			DatabaseContext.Posts

			.Where(current => current.Culture != null
				&& current.Culture.Lcid == currentUICultureLcid)

			.OrderByDescending(current => current.UpdateDateTime)

			.Select(current => new ViewModels.Pages
				.Features.Cms.Admin.Posts.IndexItemViewModel
			{
				Id = current.Id,

				TypeId = current.TypeId,
				UserId = current.UserId,
				CategoryId = current.CategoryId,

				Hits = current.Hits,
				Ordering = current.Ordering,

				IsDraft = current.IsDraft,
				IsActive = current.IsActive,
				IsDeleted = current.IsDeleted,
				IsFeatured = current.IsFeatured,
				IsCommentingEnabled = current.IsCommentingEnabled,

				CommentCount = current.Comments.Count,

				InsertDateTime = current.InsertDateTime,
				UpdateDateTime = current.UpdateDateTime,
				DeleteDateTime = current.DeleteDateTime,

				Title = current.Title,

				//Author = current.Author != null ? current.Author :
				//	current.User != null ? current.User.Username : default,

				//Author = current.User != null ? current.User.EmailAddress : default,

				Author = current.User == null ? current.Author :
					current.User.Username == null ? current.User.EmailAddress : current.User.Username,

				TypeName = current.Type!.Name,
				CategoryName = current.Category!.Name,
			})
			.ToListAsync()
			;
	}
	#endregion /OnGetAsync()

	#endregion /Methods
}
