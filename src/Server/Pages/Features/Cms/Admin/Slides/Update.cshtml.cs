using Dtat;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Server.Pages.Features.Cms.Admin.Slides;

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
	public ViewModels.Pages.Features.Cms.Admin.Slides.UpdateViewModel ViewModel { get; set; }

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
			DatabaseContext.Slides
			.Where(current => current.Id == id.Value)
			.Select(current => new ViewModels.Pages
				.Features.Cms.Admin.Slides.UpdateViewModel()
			{
				Id = current.Id,
				Title = current.Title,
				Caption = current.Caption,
				ImageUrl = current.ImageUrl,
				Interval = current.Interval,
				IsActive = current.IsActive,
				Ordering = current.Ordering,
				Description = current.Description,
				NavigationUrl = current.NavigationUrl,
				OpenUrlInNewWindow = current.OpenUrlInNewWindow,
				PublishStartDateTime = current.PublishStartDateTime,
				PublishFinishDateTime = current.PublishFinishDateTime,
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
		var foundedItem =
			await
			DatabaseContext.Slides
			.Where(current => current.Id == ViewModel.Id)
			.FirstOrDefaultAsync();

		if (foundedItem is null)
		{
			return RedirectToPage(pageName:
				Constants.CommonRouting.NotFound);
		}
		// **************************************************

		// **************************************************
		foundedItem.SetUpdateDateTime();

		foundedItem.Ordering = ViewModel.Ordering;
		foundedItem.Interval = ViewModel.Interval;

		foundedItem.Title = ViewModel.Title.Fix()!;
		foundedItem.ImageUrl = ViewModel.ImageUrl.Fix()!;

		foundedItem.Caption = ViewModel.Caption.Fix();
		foundedItem.Description = ViewModel.Description.Fix();
		foundedItem.NavigationUrl = ViewModel.NavigationUrl.Fix();

		foundedItem.IsActive = ViewModel.IsActive;
		foundedItem.OpenUrlInNewWindow = ViewModel.OpenUrlInNewWindow;

		foundedItem.PublishStartDateTime = ViewModel.PublishStartDateTime;
		foundedItem.PublishFinishDateTime = ViewModel.PublishFinishDateTime;
		// **************************************************

		var affectedRows =
			await
			DatabaseContext.SaveChangesAsync();

		// **************************************************
		var successMessage = string.Format
			(format: Resources.Messages.Successes.Updated,
			arg0: Resources.DataDictionary.Slide);

		AddToastSuccess(message: successMessage);
		// **************************************************

		return RedirectToPage(pageName:
			Constants.CommonRouting.CurrentIndex);
	}
	#endregion /OnPostAsync()

	#endregion /Methods
}
