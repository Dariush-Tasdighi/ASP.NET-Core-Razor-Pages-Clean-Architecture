using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Server.Pages.Features.Cms.Admin.Slides;

[Microsoft.AspNetCore.Authorization
	.Authorize(Roles = Constants.Role.Administrator)]
public class DeleteModel :
	Infrastructure.BasePageModelWithDatabaseContext
{
	#region Constructor
	public DeleteModel
		(Persistence.DatabaseContext databaseContext) :
		base(databaseContext: databaseContext)
	{
		ViewModel = new();
	}
	#endregion /Constructor

	#region Properties

	[Microsoft.AspNetCore.Mvc.BindProperty]
	public ViewModels.Pages.Features.Cms.Admin.Slides.DetailsOrDeleteViewModel ViewModel { get; private set; }

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
				.Features.Cms.Admin.Slides.DetailsOrDeleteViewModel
			{
				Id = current.Id,

				Interval = current.Interval,
				Ordering = current.Ordering,

				Title = current.Title,
				Caption = current.Caption,
				ImageUrl = current.ImageUrl,
				Description = current.Description,
				NavigationUrl = current.NavigationUrl,

				IsActive = current.IsActive,
				OpenUrlInNewWindow = current.OpenUrlInNewWindow,

				InsertDateTime = current.InsertDateTime,
				UpdateDateTime = current.UpdateDateTime,
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
		<Microsoft.AspNetCore.Mvc.IActionResult> OnPostAsync(System.Guid? id)
	{
		if (id is null)
		{
			return RedirectToPage(pageName:
				Constants.CommonRouting.BadRequest);
		}

		// **************************************************
		var foundedItem =
			await
			DatabaseContext.Slides
			.Where(current => current.Id == id.Value)
			.FirstOrDefaultAsync();

		if (foundedItem is null)
		{
			return RedirectToPage(pageName:
				Constants.CommonRouting.NotFound);
		}
		// **************************************************

		// **************************************************
		var hasAnyChildren =
			await
			DatabaseContext.Posts
			.Where(current => current.CategoryId == id.Value)
			.AnyAsync();

		if (hasAnyChildren)
		{
			// **************************************************
			var errorMessage = string.Format
				(format: Resources.Messages.Errors.CascadeDelete,
				arg0: Resources.DataDictionary.Slide);

			AddToastError(message: errorMessage);
			// **************************************************

			return RedirectToPage(pageName:
				Constants.CommonRouting.CurrentIndex);
		}
		// **************************************************

		// **************************************************
		var entityEntry =
			DatabaseContext.Remove(entity: foundedItem);

		var affectedRows =
			await
			DatabaseContext.SaveChangesAsync();
		// **************************************************

		// **************************************************
		var successMessage = string.Format
			(format: Resources.Messages.Successes.Deleted,
			arg0: Resources.DataDictionary.Slide);

		AddToastSuccess(message: successMessage);
		// **************************************************

		return RedirectToPage(pageName:
			Constants.CommonRouting.CurrentIndex);
	}
	#endregion /OnPostAsync()

	#endregion /Methods
}
