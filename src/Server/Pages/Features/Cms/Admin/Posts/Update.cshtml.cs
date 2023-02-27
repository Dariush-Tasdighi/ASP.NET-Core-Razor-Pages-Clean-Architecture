using Dtat;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Server.Pages.Features.Cms.Admin.Posts;

[Microsoft.AspNetCore.Authorization
	.Authorize(Roles = Constants.Role.Administrator)]
public class UpdateModel :
	Infrastructure.BasePageModelWithDatabaseContext
{
	#region Constructor
	public UpdateModel
		(Persistence.DatabaseContext databaseContext) :
		base(databaseContext: databaseContext)
	{
		ViewModel = new();
	}
	#endregion /Constructor

	#region Properties

	[Microsoft.AspNetCore.Mvc.BindProperty]
	public ViewModels.Pages.Features.Cms.Admin.Posts.UpdateViewModel ViewModel { get; set; }

	public Microsoft.AspNetCore.Mvc.Rendering.SelectList? PostCategoriesSelectList { get; set; }

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
			.Select(current => new ViewModels.Pages.Features.Cms.Admin.Posts.UpdateViewModel()
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
			})
			.FirstOrDefaultAsync();

		if (result is null)
		{
			return RedirectToPage(pageName:
				Constants.CommonRouting.NotFound);
		}

		ViewModel = result;

		PostCategoriesSelectList =
			await
			Infrastructure.SelectLists.GetPostCategoriesAsync
			(databaseContext: DatabaseContext, selectedValue: ViewModel.CategoryId);

		return Page();
	}
	#endregion /OnGetAsync()

	#region OnPostAsync()
	public async System.Threading.Tasks.Task
		<Microsoft.AspNetCore.Mvc.IActionResult> OnPostAsync()
	{
		if (ModelState.IsValid == false)
		{
			PostCategoriesSelectList =
				await
				Infrastructure.SelectLists.GetPostCategoriesAsync
				(databaseContext: DatabaseContext, selectedValue: ViewModel.CategoryId);

			return Page();
		}

		// **************************************************
		var foundedItem =
			await
			DatabaseContext.Posts
			.Where(current => current.Id == ViewModel.Id)
			.FirstOrDefaultAsync();

		if (foundedItem is null)
		{
			return RedirectToPage(pageName:
				Constants.CommonRouting.NotFound);
		}
		// **************************************************

		// **************************************************
		foundedItem.CategoryId = ViewModel.CategoryId;

		foundedItem.Hits = ViewModel.Hits;
		foundedItem.Score = ViewModel.Score;
		foundedItem.Ordering = ViewModel.Ordering;

		foundedItem.Title = ViewModel.Title.Fix()!;
		//foundedItem.Title = ViewModel.Title.Fix();

		foundedItem.Body = ViewModel.Body.Fix();
		foundedItem.Author = ViewModel.Author.Fix();
		foundedItem.ImageUrl = ViewModel.ImageUrl.Fix();
		foundedItem.Description = ViewModel.Description.Fix();
		foundedItem.Introduction = ViewModel.Introduction.Fix();
		foundedItem.AdminDescription = ViewModel.AdminDescription.Fix();

		foundedItem.IsDraft = ViewModel.IsDraft;
		foundedItem.IsActive = ViewModel.IsActive;
		foundedItem.IsFeatured = ViewModel.IsFeatured;
		foundedItem.IsCommentingEnabled = ViewModel.IsCommentingEnabled;
		foundedItem.DoesSearchEnginesIndexIt = ViewModel.DoesSearchEnginesIndexIt;
		foundedItem.DoesSearchEnginesFollowIt = ViewModel.DoesSearchEnginesFollowIt;
		foundedItem.DisplayCommentsAfterVerification = ViewModel.DisplayCommentsAfterVerification;
		// **************************************************

		// **************************************************
		foundedItem.SetUpdateDateTime();

		if (ViewModel.IsDeleted)
		{
			foundedItem.Delete();
		}
		else
		{
			foundedItem.Undelete();
		}
		// **************************************************

		var affectedRows =
			await
			DatabaseContext.SaveChangesAsync();

		// **************************************************
		var successMessage = string.Format
			(format: Resources.Messages.Successes.Updated,
			arg0: Resources.DataDictionary.Post);

		AddToastSuccess(message: successMessage);
		// **************************************************

		return RedirectToPage(pageName:
			Constants.CommonRouting.CurrentIndex);
	}
	#endregion /OnPostAsync()

	#endregion /Methods
}
