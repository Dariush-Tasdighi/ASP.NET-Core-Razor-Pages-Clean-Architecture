using Dtat;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Server.Pages.Features.Cms.Admin.PostCategories;

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
	public ViewModels.Pages.Features.Cms.Admin.PostCategories.CreateViewModel ViewModel { get; set; }

	#endregion /Properties

	#region Methods

	#region OnGetAsync()
	public async System.Threading.Tasks.Task OnGetAsync()
	{
		await System.Threading.Tasks.Task.CompletedTask;
	}
	#endregion /OnGetAsync()

	#region OnPostAsync()
	public async System.Threading.Tasks.Task
		<Microsoft.AspNetCore.Mvc.IActionResult> OnPostAsync()
	{
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
				Constants.CommonRouting.NotFound);
		}
		// **************************************************

		if (ModelState.IsValid == false)
		{
			return Page();
		}

		// **************************************************
		var name =
			ViewModel.Name.Fix()!;

		var isNameFound =
			await
			DatabaseContext.PostCategories

			.Where(current => current.CultureId == currentCulture.Id)
			.Where(current => current.Name.ToLower() == name.ToLower())

			.AnyAsync();

		if (isNameFound)
		{
			var errorMessage = string.Format
				(Resources.Messages.Errors.AlreadyExists,
				Resources.DataDictionary.Name);

			AddPageError(message: errorMessage);
		}
		// **************************************************

		// **************************************************
		var title =
			ViewModel.Title.Fix()!;

		var isTitleFound =
			await
			DatabaseContext.PostCategories

			.Where(current => current.CultureId == currentCulture.Id)
			.Where(current => current.Title.ToLower() == title.ToLower())

			.AnyAsync();

		if (isTitleFound)
		{
			var errorMessage = string.Format
				(Resources.Messages.Errors.AlreadyExists,
				Resources.DataDictionary.Title);

			AddPageError(message: errorMessage);
		}
		// **************************************************

		if (isNameFound || isTitleFound)
		{
			return Page();
		}

		// **************************************************
		var newEntity =
			new Domain.Features.Cms.PostCategory
			(cultureId: currentCulture.Id,
			name: name, title: title)
			{
				Body = ViewModel.Body.Fix(),
				Description = ViewModel.Description.Fix(),

				ImageUrl = ViewModel.ImageUrl.Fix(),
				CoverImageUrl = ViewModel.CoverImageUrl.Fix(),

				IsActive = ViewModel.IsActive,
				DisplayInHomePage = ViewModel.DisplayInHomePage,

				Hits = ViewModel.Hits,
				Ordering = ViewModel.Ordering,
				MaxPostCountInHomePage = ViewModel.MaxPostCountInHomePage,
				MaxPostCountInMainPage = ViewModel.MaxPostCountInMainPage,
			};

		await DatabaseContext.AddAsync(entity: newEntity);

		await DatabaseContext.SaveChangesAsync();
		// **************************************************

		// **************************************************
		var successMessage = string.Format
			(Resources.Messages.Successes.Created,
			Resources.DataDictionary.PostCategory);

		AddToastSuccess(message: successMessage);
		// **************************************************

		return RedirectToPage(pageName:
			Constants.CommonRouting.CurrentIndex);
	}
	#endregion /OnPostAsync()

	#endregion /Methods
}
