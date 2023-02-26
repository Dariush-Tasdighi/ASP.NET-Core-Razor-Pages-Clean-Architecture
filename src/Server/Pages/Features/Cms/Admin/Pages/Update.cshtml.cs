using Dtat;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;

namespace Server.Pages.Features.Cms.Admin.Pages;

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

	public Microsoft.AspNetCore.Mvc.Rendering.SelectList? LayoutsSelectList { get; set; }

	[Microsoft.AspNetCore.Mvc.BindProperty]
	public ViewModels.Pages.Features.Cms.Admin.Pages.UpdateViewModel ViewModel { get; set; }

	#endregion /Properties

	#region Methods

	#region OnGetAsync()
	public async System.Threading.Tasks.Task
		<Microsoft.AspNetCore.Mvc.IActionResult> OnGetAsync(System.Guid? id)
	{
		if (id.HasValue == false)
		{
			return RedirectToPage(pageName:
				Constants.CommonRouting.BadRequest);
		}

		var result =
			await
			DatabaseContext.Pages
			.Where(current => current.Id == id.Value)
			.Select(current => new ViewModels.Pages.Features.Cms.Admin.Pages.UpdateViewModel()
			{
				Id = current.Id,
				LayoutId = current.LayoutId,

				IsActive = current.IsActive,

				Ordering = current.Ordering,

				Body = current.Body,
				Name = current.Name,
				Title = current.Title,
				Description = current.Description,

				DoesSearchEnginesIndexIt = current.DoesSearchEnginesIndexIt,
				DoesSearchEnginesFollowIt = current.DoesSearchEnginesFollowIt,
			})
			.FirstOrDefaultAsync();

		if (result is null)
		{
			return RedirectToPage(pageName:
				Constants.CommonRouting.NotFound);
		}

		ViewModel = result;

		LayoutsSelectList =
			await
			Infrastructure.SelectLists.GetLayoutsAsync
			(databaseContext: DatabaseContext, selectedValue: null);

		return Page();
	}
	#endregion /OnGetAsync()

	#region OnPostAsync()
	public async System.Threading.Tasks.Task
		<Microsoft.AspNetCore.Mvc.IActionResult> OnPostAsync()
	{
		if (ModelState.IsValid == false)
		{
			LayoutsSelectList =
				await
				Infrastructure.SelectLists.GetLayoutsAsync
				(databaseContext: DatabaseContext, selectedValue: null);

			return Page();
		}

		// **************************************************
		var foundedItem =
			await
			DatabaseContext.Pages
			.Where(current => current.Id == ViewModel.Id)
			.FirstOrDefaultAsync();

		if (foundedItem is null)
		{
			return RedirectToPage(pageName:
				Constants.CommonRouting.NotFound);
		}
		// **************************************************

		// **************************************************
		ViewModel.Name =
			ViewModel.Name.Fix()!;

		//ViewModel.Name =
		//	ViewModel.Name.Fix();

		var foundedAny =
			await
			DatabaseContext.Pages
			.Where(current => current.Id != ViewModel.Id)
			.Where(current => current.Name.ToLower() == ViewModel.Name.ToLower())
			.AnyAsync();

		if (foundedAny)
		{
			// **************************************************
			var errorMessage = string.Format
				(format: Resources.Messages.Errors.AlreadyExists,
				arg0: Resources.DataDictionary.Name);

			AddPageError(message: errorMessage);
			// **************************************************

			LayoutsSelectList =
				await
				Infrastructure.SelectLists.GetLayoutsAsync
				(databaseContext: DatabaseContext, selectedValue: null);

			return Page();
		}
		// **************************************************

		// **************************************************
		ViewModel.Title =
			ViewModel.Title.Fix()!;

		//ViewModel.Title =
		//	ViewModel.Title.Fix();

		ViewModel.Description =
			ViewModel.Description.Fix();

		foundedItem.SetUpdateDateTime();

		foundedItem.Body = ViewModel.Body;
		foundedItem.Name = ViewModel.Name;
		foundedItem.Title = ViewModel.Title;
		foundedItem.LayoutId = ViewModel.LayoutId;
		foundedItem.Ordering = ViewModel.Ordering;
		foundedItem.IsActive = ViewModel.IsActive;
		foundedItem.Description = ViewModel.Description;
		foundedItem.DoesSearchEnginesIndexIt = ViewModel.DoesSearchEnginesIndexIt;
		foundedItem.DoesSearchEnginesFollowIt = ViewModel.DoesSearchEnginesFollowIt;
		// **************************************************

		var affectedRows =
			await
			DatabaseContext.SaveChangesAsync();

		// **************************************************
		var successMessage = string.Format
			(format: Resources.Messages.Successes.Updated,
			arg0: Resources.DataDictionary.Page);

		AddToastSuccess(message: successMessage);
		// **************************************************

		return RedirectToPage(pageName: "Index");
	}
	#endregion /OnPostAsync()

	#endregion /Methods
}