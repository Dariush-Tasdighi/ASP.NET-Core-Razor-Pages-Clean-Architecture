using Dtat;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Server.Pages.Features.Identity.Admin.Roles;

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
	public ViewModels.Pages.Features.Identity.Admin.Roles.UpdateViewModel ViewModel { get; set; }

	#endregion /Properties

	#region Methods

	#region OnGetAsync()
	public async System.Threading.Tasks.Task
		<Microsoft.AspNetCore.Mvc.IActionResult> OnGetAsync(System.Guid? id)
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

		if (id is null)
		{
			return RedirectToPage(pageName:
				Constants.CommonRouting.BadRequest);
		}

		var result =
			await
			DatabaseContext.Roles

			.Where(current => current.Id == id.Value)

			.Select(current => new ViewModels.Pages
				.Features.Identity.Admin.Roles.UpdateViewModel()
			{
				Id = current.Id,
				IsActive = current.IsActive,
				Ordering = current.Ordering,

#pragma warning disable CS8602

				Title = current.LocalizedRoles
					.FirstOrDefault(other => other.CultureId == currentCulture.Id).Title,

				Description = current.LocalizedRoles
					.FirstOrDefault(other => other.CultureId == currentCulture.Id).Description,

#pragma warning restore CS8602

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
			DatabaseContext.Roles
			.Where(current => current.Id == ViewModel.Id)
			.FirstOrDefaultAsync();

		if (foundedItem is null)
		{
			return RedirectToPage(pageName:
				Constants.CommonRouting.NotFound);
		}
		// **************************************************

		// **************************************************
		var title = ViewModel.Title.Fix()!;

		var foundedAny =
			await
			DatabaseContext.LocalizedRoles

			.Where(current => current.Id != ViewModel.Id)
			.Where(current => current.CultureId == currentCulture.Id)
			.Where(current => current.Title.ToLower() == title.ToLower())

			.AnyAsync();

		if (foundedAny)
		{
			// **************************************************
			var errorMessage = string.Format
				(format: Resources.Messages.Errors.AlreadyExists,
				arg0: Resources.DataDictionary.Title);

			AddPageError(message: errorMessage);
			// **************************************************

			return Page();
		}
		// **************************************************

		// **************************************************
		foundedItem.SetUpdateDateTime();

		// دقت کنید که دستور ذیل نباید نوشته شود
		//foundedItem.Name = ViewModel.Name.Fix()!;

		foundedItem.IsActive = ViewModel.IsActive;
		foundedItem.Ordering = ViewModel.Ordering;
		// **************************************************

		// **************************************************
		var localizedItem =
			await
			DatabaseContext.LocalizedRoles
			.Where(current => current.RoleId == foundedItem.Id)
			.Where(current => current.CultureId == currentCulture.Id)
			.FirstOrDefaultAsync();

		if (localizedItem is not null)
		{
			localizedItem.Title = title;
			localizedItem.Description = ViewModel.Description.Fix();
		}
		else
		{
			localizedItem =
				new Domain.Features.Identity.LocalizedRole
				(cultureId: currentCulture.Id, roleId: foundedItem.Id, title: title)
				{
					Description = ViewModel.Description.Fix(),
				};

			await DatabaseContext.AddAsync(entity: localizedItem);
		}
		// **************************************************

		var affectedRows =
			await
			DatabaseContext.SaveChangesAsync();

		// **************************************************
		var successMessage = string.Format
			(format: Resources.Messages.Successes.Updated,
			arg0: Resources.DataDictionary.Role);

		AddToastSuccess(message: successMessage);
		// **************************************************

		return RedirectToPage(pageName:
			Constants.CommonRouting.CurrentIndex);
	}
	#endregion /OnPostAsync()

	#endregion /Methods
}
