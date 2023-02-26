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
		if (id.HasValue == false)
		{
			return RedirectToPage(pageName:
				Constants.CommonRouting.BadRequest);
		}

		var result =
			await
			DatabaseContext.PostCategories
			.Where(current => current.Id == id.Value)
			.Select(current => new ViewModels.Pages.Features.Cms.Admin.PostCategories.UpdateViewModel()
			{
				Id = current.Id,
				Name = current.Name,
				IsActive = current.IsActive,
				Ordering = current.Ordering,
				Description = current.Description,
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
		ViewModel.Name =
			ViewModel.Name.Fix()!;

		//ViewModel.Name =
		//	ViewModel.Name.Fix();

		var foundedAny =
			await
			DatabaseContext.PostCategories
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

			return Page();
		}
		// **************************************************

		// **************************************************
		ViewModel.Description =
			ViewModel.Description.Fix();

		foundedItem.SetUpdateDateTime();

		foundedItem.Name = ViewModel.Name;
		foundedItem.Ordering = ViewModel.Ordering;
		foundedItem.IsActive = ViewModel.IsActive;
		foundedItem.Description = ViewModel.Description;
		// **************************************************

		var affectedRows =
			await
			DatabaseContext.SaveChangesAsync();

		// **************************************************
		var successMessage = string.Format
			(format: Resources.Messages.Successes.Updated,
			arg0: Resources.DataDictionary.PostCategory);

		AddToastSuccess(message: successMessage);
		// **************************************************

		return RedirectToPage(pageName: "Index");
	}
	#endregion /OnPostAsync()

	#endregion /Methods
}
