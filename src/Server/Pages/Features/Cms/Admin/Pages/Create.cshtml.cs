using Dtat;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Server.Pages.Features.Cms.Admin.Pages;

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

	public Microsoft.AspNetCore.Mvc.Rendering.SelectList? LayoutsSelectList { get; set; }

	[Microsoft.AspNetCore.Mvc.BindProperty]
	public ViewModels.Pages.Features.Cms.Admin.Pages.CreateViewModel ViewModel { get; set; }

	#endregion /Properties

	#region Methods

	#region OnGetAsync()
	public async System.Threading.Tasks.Task OnGetAsync()
	{
		LayoutsSelectList =
			await
			Infrastructure.SelectLists.GetLayoutsAsync
			(databaseContext: DatabaseContext, selectedValue: null);

		await System.Threading.Tasks.Task.CompletedTask;
	}
	#endregion /OnGetAsync()

	#region OnPostAsync()
	public async System.Threading.Tasks.Task
		<Microsoft.AspNetCore.Mvc.IActionResult> OnPostAsync()
	{
		// **************************************************
		if (ModelState.IsValid == false)
		{
			LayoutsSelectList =
				await
				Infrastructure.SelectLists.GetLayoutsAsync
				(databaseContext: DatabaseContext, selectedValue: null);

			return Page();
		}
		// **************************************************

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

		// **************************************************
		var name =
			ViewModel.Name.Fix()!.ToLower();

		var foundedAny =
			await
			DatabaseContext.Pages

			.Where(current => current.Culture != null &&
				current.Culture.Lcid == currentUICultureLcid)

			.Where(current => current.Name.ToLower() == name)

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
		var newEntity =
			new Domain.Features.Cms.Page(cultureId: currentCulture.Id,
			layoutId: ViewModel.LayoutId, name: name, title: ViewModel.Title.Fix()!)
			{
				Ordering = ViewModel.Ordering,
				IsActive = ViewModel.IsActive,

				Body = ViewModel.Body.Fix(),
				Description = ViewModel.Description.Fix(),

				DoesSearchEnginesIndexIt = ViewModel.DoesSearchEnginesIndexIt,
				DoesSearchEnginesFollowIt = ViewModel.DoesSearchEnginesFollowIt,
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
			Resources.DataDictionary.Page);

		AddToastSuccess(message: successMessage);
		// **************************************************

		return RedirectToPage(pageName:
			Constants.CommonRouting.CurrentIndex);
	}
	#endregion /OnPostAsync()

	#endregion /Methods
}
