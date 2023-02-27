using Dtat;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Server.Pages.Features.Cms.Admin.Posts;

[Microsoft.AspNetCore.Authorization
	.Authorize(Roles = Constants.Role.Administrator)]
public class CreateModel :
	Infrastructure.BasePageModelWithDatabaseContext
{
	#region Constructor
	public CreateModel
		(Persistence.DatabaseContext databaseContext) :
		base(databaseContext: databaseContext)
	{
		ViewModel = new();
	}
	#endregion /Constructor

	#region Properties

	[Microsoft.AspNetCore.Mvc.BindProperty]
	public ViewModels.Pages.Features.Cms.Admin.Posts.CreateViewModel ViewModel { get; set; }

	public Microsoft.AspNetCore.Mvc.Rendering.SelectList? PostCategoriesSelectList { get; set; }

	#endregion /Properties

	#region Methods

	#region OnGetAsync()
	public async System.Threading.Tasks.Task OnGetAsync()
	{
		PostCategoriesSelectList =
			await
			Infrastructure.SelectLists.GetPostCategoriesAsync
			(databaseContext: DatabaseContext, selectedValue: null);

		await System.Threading.Tasks.Task.CompletedTask;
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
				(databaseContext: DatabaseContext, selectedValue: null);

			return Page();
		}

		// **************************************************
		var currentUICultureLcid = Domain.Features
			.Common.CultureEnumHelper.GetCurrentUICultureLcid();

		var currentCulture =
			await
			DatabaseContext.Cultures
			.Where(current => current.Lcid == currentUICultureLcid)
			.FirstOrDefaultAsync();

		if (currentCulture is null)
		{
			return RedirectToPage(pageName:
				Constants.CommonRouting.InternalServerError);
		}
		// **************************************************

		// **************************************************
		if (User is null ||
			User.Identity is null ||
			User.Identity.IsAuthenticated == false ||
			string.IsNullOrWhiteSpace(value: User.Identity.Name))
		{
			return RedirectToPage(pageName:
				Constants.CommonRouting.Logout);
		}

		var userEmailAddress = User.Identity.Name;

		var currentUser =
			await
			DatabaseContext.Users
			.Where(current => current.EmailAddress.ToLower() == userEmailAddress.ToLower())
			.FirstOrDefaultAsync();

		if (currentUser is null)
		{
			return RedirectToPage(pageName:
				Constants.CommonRouting.InternalServerError);
		}
		// **************************************************

		// **************************************************
		var newEntity =
			new Domain.Features.Cms.Post(cultureId: currentCulture.Id,
			userId: currentUser.Id, ViewModel.CategoryId, title: ViewModel.Title.Fix()!)
			{
				Hits = ViewModel.Hits,
				Ordering = ViewModel.Ordering,

				Body = ViewModel.Body.Fix(),
				Author = ViewModel.Author.Fix(),
				ImageUrl = ViewModel.ImageUrl.Fix(),
				Description = ViewModel.Description.Fix(),
				Introduction = ViewModel.Introduction.Fix(),
				AdminDescription = ViewModel.AdminDescription.Fix(),

				IsDraft = ViewModel.IsDraft,
				IsActive = ViewModel.IsActive,
				IsFeatured = ViewModel.IsFeatured,
				IsCommentingEnabled = ViewModel.IsCommentingEnabled,
				DoesSearchEnginesIndexIt = ViewModel.DoesSearchEnginesIndexIt,
				DoesSearchEnginesFollowIt = ViewModel.DoesSearchEnginesFollowIt,
				DisplayCommentsAfterVerification = ViewModel.DisplayCommentsAfterVerification,
			};

		if(ViewModel.IsDeleted)
		{
			newEntity.Delete();
		}

		var entityEntry =
			await
			DatabaseContext.AddAsync(entity: newEntity);

		var affectedRows =
			await
			DatabaseContext.SaveChangesAsync();
		// **************************************************

		// **************************************************
		var successMessage = string.Format
			(Resources.Messages.Successes.Created,
			Resources.DataDictionary.Post);

		AddToastSuccess(message: successMessage);
		// **************************************************

		return RedirectToPage(pageName: "Index");
	}
	#endregion /OnPostAsync()

	#endregion /Methods
}
