using Dtat;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Server.Pages.Features.Cms.Admin.PostCategories;

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
	public ViewModels.Pages.Features.Cms.Admin.PostCategories.UpdateViewModel ViewModel { get; set; }

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
			.Select(current => new ViewModels.Pages
				.Features.Cms.Admin.PostCategories.UpdateViewModel()
			{
				Id = current.Id,
				Body = current.Body,
				Hits = current.Hits,
				Name = current.Name,
				Title = current.Title,
				IsActive = current.IsActive,
				Ordering = current.Ordering,
				Description = current.Description,
				DisplayInHomePage = current.DisplayInHomePage,
				MaxDisplayPostCount = current.MaxDisplayPostCount,
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
		var foundedItem =
			await
			DatabaseContext.PostCategories
			.Where(current => current.Id == ViewModel.Id)
			.FirstOrDefaultAsync();

		if (foundedItem is null)
		{
			return RedirectToPage(pageName:
				Constants.CommonRouting.NotFound);
		}
		// **************************************************

		// **************************************************
		var name =
			ViewModel.Name.Fix()!;

		var isNameFound =
			await
			DatabaseContext.PostCategories

			.Where(current => current.Id != ViewModel.Id)
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

			.Where(current => current.Id != ViewModel.Id)
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
		foundedItem.SetUpdateDateTime();

		foundedItem.Name = name;
		foundedItem.Title = title;

		foundedItem.Hits = ViewModel.Hits;
		foundedItem.Ordering = ViewModel.Ordering;

		foundedItem.Body = ViewModel.Body.Fix();
		foundedItem.ImageUrl = ViewModel.ImageUrl.Fix();
		foundedItem.Description = ViewModel.Description.Fix();
		foundedItem.WideImageUrl = ViewModel.WideImageUrl.Fix();

		foundedItem.IsActive = ViewModel.IsActive;
		foundedItem.DisplayInHomePage = ViewModel.DisplayInHomePage;
		foundedItem.MaxDisplayPostCount = ViewModel.MaxDisplayPostCount;
		// **************************************************

		await DatabaseContext.SaveChangesAsync();

		// **************************************************
		var successMessage = string.Format
			(format: Resources.Messages.Successes.Updated,
			arg0: Resources.DataDictionary.PostCategory);

		AddToastSuccess(message: successMessage);
		// **************************************************

		return RedirectToPage(pageName:
			Constants.CommonRouting.CurrentIndex);
	}
	#endregion /OnPostAsync()

	#endregion /Methods
}
