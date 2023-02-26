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
		if (ModelState.IsValid == false)
		{
			return Page();
		}

		ViewModel.Name =
			ViewModel.Name.Fix()!;

		//ViewModel.Name =
		//	ViewModel.Name.Fix();

		var foundedAny =
			await
			DatabaseContext.PostCategories

			.Where(current => current.Name.ToLower() == ViewModel.Name.ToLower())

			.AnyAsync();

		if (foundedAny)
		{
			// **************************************************
			var errorMessage = string.Format
				(Resources.Messages.Errors.AlreadyExists,
				Resources.DataDictionary.Name);

			AddPageError(message: errorMessage);
			// **************************************************

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
		ViewModel.Description =
			ViewModel.Description.Fix();

		var newEntity =
			new Domain.Features.Cms.PostCategory(cultureId: currentCulture.Id, name: ViewModel.Name)
			{
				Ordering = ViewModel.Ordering,
				IsActive = ViewModel.IsActive,
				Description = ViewModel.Description,
			};

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
			Resources.DataDictionary.PostCategory);

		AddToastSuccess(message: successMessage);
		// **************************************************

		return RedirectToPage(pageName: "Index");
	}
	#endregion /OnPostAsync()

	#endregion /Methods
}
