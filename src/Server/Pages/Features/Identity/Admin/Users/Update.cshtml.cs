using Dtat;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Server.Pages.Features.Identity.Admin.Users;

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

	public Microsoft.AspNetCore.Mvc.Rendering.SelectList? RolesSelectList { get; set; }

	public Microsoft.AspNetCore.Mvc.Rendering.SelectList? GendersSelectList { get; set; }

	[Microsoft.AspNetCore.Mvc.BindProperty]
	public ViewModels.Pages.Features.Identity.Admin.Users.UpdateViewModel ViewModel { get; set; }

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
			DatabaseContext.Users

			.Where(current => current.Id == id.Value)

			.Select(current => new ViewModels.Pages.Features.Identity.Admin.Users.UpdateViewModel
			{
				Id = current.Id,

				RoleId = current.RoleId,
				GenderId = current.GenderId,

				Ordering = current.Ordering,

				Username = current.Username,
				EmailAddress = current.EmailAddress,
				NationalCode = current.NationalCode,
				CellPhoneNumber = current.CellPhoneNumber,
				AdminDescription = current.AdminDescription,

				IsActive = current.IsActive,
				IsDeleted = current.IsDeleted,
				IsVerified = current.IsVerified,
				IsProfilePublic = current.IsProfilePublic,
				IsEmailAddressVerified = current.IsEmailAddressVerified,
				IsNationalCodeVerified = current.IsNationalCodeVerified,
				IsVisibleInContactUsPage = current.IsVisibleInContactUsPage,
				IsCellPhoneNumberVerified = current.IsCellPhoneNumberVerified,

#pragma warning disable CS8602

				Hits = current.LocalizedUsers.FirstOrDefault
					(current => current.CultureId == currentCulture.Id).Hits,

				LastName = current.LocalizedUsers.FirstOrDefault
					(current => current.CultureId == currentCulture.Id).LastName,

				FirstName = current.LocalizedUsers.FirstOrDefault
					(current => current.CultureId == currentCulture.Id).FirstName,

				Description = current.LocalizedUsers.FirstOrDefault
					(current => current.CultureId == currentCulture.Id).Description,

				TitleInContactUsPage = current.LocalizedUsers.FirstOrDefault
					(current => current.CultureId == currentCulture.Id).TitleInContactUsPage,

#pragma warning restore CS8602

			})
			.FirstOrDefaultAsync();

		if (result is null)
		{
			return RedirectToPage(pageName:
				Constants.CommonRouting.NotFound);
		}

		ViewModel = result;

		RolesSelectList =
			await
			Infrastructure.SelectLists.GetRolesAsync
			(databaseContext: DatabaseContext, selectedValue: null);

		GendersSelectList =
			await
			Infrastructure.SelectLists.GetGendersForAdminAsync
			(databaseContext: DatabaseContext, selectedValue: null);

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

		// **************************************************
		if (ModelState.IsValid == false)
		{
			RolesSelectList =
				await
				Infrastructure.SelectLists.GetRolesAsync
				(databaseContext: DatabaseContext, selectedValue: null);

			GendersSelectList =
				await
				Infrastructure.SelectLists.GetGendersForAdminAsync
				(databaseContext: DatabaseContext, selectedValue: null);

			return Page();
		}
		// **************************************************

		// **************************************************
		var foundedItem =
			await
			DatabaseContext.Users
			.Where(current => current.Id == ViewModel.Id)
			.FirstOrDefaultAsync();

		if (foundedItem is null)
		{
			return RedirectToPage(pageName:
				Constants.CommonRouting.NotFound);
		}
		// **************************************************

		// **************************************************
		var emailAddress =
			ViewModel.EmailAddress.Fix()!.ToLower();

		var isEmailAddressFound =
			await
			DatabaseContext.Users

			.Where(current => current.Id != ViewModel.Id)
			.Where(current => current.EmailAddress.ToLower() == emailAddress)

			.AnyAsync();

		if (isEmailAddressFound)
		{
			var errorMessage = string.Format
				(format: Resources.Messages.Errors.AlreadyExists,
				arg0: Resources.DataDictionary.EmailAddress);

			AddPageError(message: errorMessage);
		}
		// **************************************************

		// **************************************************
		var username =
			ViewModel.Username.Fix();

		var isUsernameFound = false;

		if (username is not null)
		{
			isUsernameFound =
				await
				DatabaseContext.Users

				.Where(current => current.Id != ViewModel.Id)
				.Where(current => current.Username != null
					&& current.Username.ToLower() == username.ToLower())

				.AnyAsync()
				;

			if (isUsernameFound)
			{
				var errorMessage = string.Format
					(format: Resources.Messages.Errors.AlreadyExists,
					arg0: Resources.DataDictionary.Username);

				AddPageError(message: errorMessage);
			}
		}
		// **************************************************

		// **************************************************
		var cellPhoneNumber =
			ViewModel.CellPhoneNumber.Fix();

		var isCellPhoneNumberFound = false;

		if (cellPhoneNumber is not null)
		{
			isCellPhoneNumberFound =
				await
				DatabaseContext.Users

				.Where(current => current.Id != ViewModel.Id)
				.Where(current => current.CellPhoneNumber != null
					&& current.CellPhoneNumber.ToLower() == cellPhoneNumber)

				.AnyAsync();

			if (isCellPhoneNumberFound)
			{
				var errorMessage = string.Format
					(format: Resources.Messages.Errors.AlreadyExists,
					arg0: Resources.DataDictionary.CellPhoneNumber);

				AddPageError(message: errorMessage);
			}
		}
		// **************************************************

		// **************************************************
		var nationalCode =
			ViewModel.NationalCode.Fix();

		var isNationalCodeFound = false;

		if (nationalCode is not null)
		{
			isNationalCodeFound =
				await
				DatabaseContext.Users

				.Where(current => current.Id != ViewModel.Id)
				.Where(current => current.NationalCode != null
					&& current.NationalCode.ToLower() == nationalCode)

				.AnyAsync();

			if (isNationalCodeFound)
			{
				var errorMessage = string.Format
					(format: Resources.Messages.Errors.AlreadyExists,
					arg0: Resources.DataDictionary.NationalCode);

				AddPageError(message: errorMessage);
			}
		}
		// **************************************************

		if (isUsernameFound || isEmailAddressFound ||
			isCellPhoneNumberFound || isNationalCodeFound)
		{
			RolesSelectList =
				await
				Infrastructure.SelectLists.GetRolesAsync
				(databaseContext: DatabaseContext, selectedValue: null);

			GendersSelectList =
				await
				Infrastructure.SelectLists.GetGendersForAdminAsync
				(databaseContext: DatabaseContext, selectedValue: null);

			return Page();
		}

		// **************************************************
		foundedItem.SetUpdateDateTime();

		foundedItem.Username = username;

		foundedItem.Ordering = ViewModel.Ordering;

		foundedItem.NationalCode = nationalCode;
		foundedItem.CellPhoneNumber = cellPhoneNumber;

		foundedItem.IsActive = ViewModel.IsActive;
		foundedItem.IsVerified = ViewModel.IsVerified;
		foundedItem.IsProfilePublic = ViewModel.IsProfilePublic;
		foundedItem.IsEmailAddressVerified = ViewModel.IsEmailAddressVerified;
		foundedItem.IsNationalCodeVerified = ViewModel.IsNationalCodeVerified;
		foundedItem.IsVisibleInContactUsPage = ViewModel.IsVisibleInContactUsPage;
		foundedItem.IsCellPhoneNumberVerified = ViewModel.IsCellPhoneNumberVerified;

		foundedItem.AdminDescription = ViewModel.AdminDescription.Fix();

		if (ViewModel.IsDeleted)
		{
			foundedItem.Delete();
		}
		else
		{
			foundedItem.Undelete();
		}
		// **************************************************

		// **************************************************
		var localizedItem =
			await
			DatabaseContext.LocalizedUsers
			.Where(current => current.UserId == foundedItem.Id)
			.Where(current => current.CultureId == currentCulture.Id)
			.FirstOrDefaultAsync();

		if (localizedItem is not null)
		{
			localizedItem.SetUpdateDateTime();

			localizedItem.Hits = ViewModel.Hits;
			localizedItem.LastName = ViewModel.LastName.Fix()!;
			localizedItem.FirstName = ViewModel.FirstName.Fix()!;
			localizedItem.Description = ViewModel.Description.Fix();
			localizedItem.TitleInContactUsPage = ViewModel.TitleInContactUsPage.Fix();
		}
		else
		{
			localizedItem =
				new Domain.Features.Identity.LocalizedUser
				(cultureId: currentCulture.Id, userId: foundedItem.Id,
				firstName: ViewModel.FirstName.Fix()!, ViewModel.LastName.Fix()!)
				{
					Hits = ViewModel.Hits,
					Description = ViewModel.Description.Fix(),
					TitleInContactUsPage = ViewModel.TitleInContactUsPage.Fix(),
				};

			await DatabaseContext.AddAsync(entity: localizedItem);
		}
		// **************************************************

		await DatabaseContext.SaveChangesAsync();

		// **************************************************
		var successMessage = string.Format
			(Resources.Messages.Successes.Updated,
			Resources.DataDictionary.User);

		AddToastSuccess(message: successMessage);
		// **************************************************

		return RedirectToPage(pageName:
			Constants.CommonRouting.CurrentIndex);
	}
	#endregion /OnPostAsync()

	#endregion /Methods
}
