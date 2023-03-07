using Dtat;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Server.Pages.Account;

[Microsoft.AspNetCore.Authorization.Authorize]
public class UpdateProfileModel :
	Infrastructure.BasePageModelWithDatabaseContext
{
	#region Constructor
	public UpdateProfileModel
		(Persistence.DatabaseContext databaseContext) :
		base(databaseContext: databaseContext)
	{
		ViewModel = new();
	}
	#endregion /Constructor

	#region Properties

	[Microsoft.AspNetCore.Mvc.BindProperty]
	public ViewModels.Pages.Account.UpdateProfileViewModel ViewModel { get; set; }

	public Microsoft.AspNetCore.Mvc.Rendering.SelectList? GendersSelectList { get; set; }

	#endregion /Properties

	#region Methods

	#region OnGetAsync()
	public async System.Threading.Tasks.Task
		<Microsoft.AspNetCore.Mvc.IActionResult> OnGetAsync()
	{
		if (User is null ||
			User.Identity is null ||
			User.Identity.IsAuthenticated == false ||
			string.IsNullOrWhiteSpace(value: User.Identity.Name))
		{
			return RedirectToPage(pageName:
				Constants.CommonRouting.Logout);
		}

		var userEmailAddress = User.Identity.Name;

		var currentUICultureLcid = Domain.Features
			.Common.CultureEnumHelper.GetCurrentUICultureLcid();

		var foundedUser =
			await
			DatabaseContext.Users
			.Include(current => current.LocalizedUsers)
			.ThenInclude(current => current.Culture)
			.Where(current => current.EmailAddress.ToLower() == userEmailAddress.ToLower())
			.FirstOrDefaultAsync();

		if (foundedUser is null)
		{
			return RedirectToPage(pageName:
				Constants.CommonRouting.NotFound);
		}

		GendersSelectList =
			await
			Infrastructure.SelectLists.GetGendersForUserAsync
			(databaseContext: DatabaseContext, selectedValue: null);

		ViewModel.GenderId = foundedUser.GenderId;
		ViewModel.ImageUrl = foundedUser.ImageUrl;
		ViewModel.Username = foundedUser.Username;
		ViewModel.WideImageUrl = foundedUser.WideImageUrl;
		ViewModel.NationalCode = foundedUser.NationalCode;
		ViewModel.IsProfilePublic = foundedUser.IsProfilePublic;
		ViewModel.CellPhoneNumber = foundedUser.CellPhoneNumber;

		var foundedLocalizedUser =
			foundedUser.LocalizedUsers
			.Where(current => current.Culture is not null &&
				current.Culture.Lcid == currentUICultureLcid)
			.FirstOrDefault();

		if (foundedLocalizedUser is null)
		{
			return Page();
		}

		ViewModel.LastName = foundedLocalizedUser.LastName;
		ViewModel.FirstName = foundedLocalizedUser.FirstName;
		ViewModel.Description = foundedLocalizedUser.Description;

		return Page();
	}
	#endregion /OnGetAsync()

	#region OnPostAsync()
	public async
		System.Threading.Tasks.Task<Microsoft.AspNetCore.Mvc.IActionResult> OnPostAsync()
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
			GendersSelectList =
				await
				Infrastructure.SelectLists.GetGendersForUserAsync
				(databaseContext: DatabaseContext, selectedValue: null);

			return Page();
		}
		// **************************************************

		// **************************************************
		var userEmailAddress =
			User!.Identity!.Name!.ToLower();

		var foundedUser =
			await
			DatabaseContext.Users

			.Include(current => current.LocalizedUsers)
			.ThenInclude(current => current.Culture)

			.Where(current => current.EmailAddress.ToLower() == userEmailAddress)

			.FirstOrDefaultAsync();

		if (foundedUser is null)
		{
			return RedirectToPage(pageName:
				Constants.CommonRouting.Logout);
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

				.Where(current => current.Id != foundedUser.Id)

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

			username = username.ToLower();
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

				.Where(current => current.Id != foundedUser.Id)

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

				.Where(current => current.Id != foundedUser.Id)

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

		// **************************************************
		if (isUsernameFound || isCellPhoneNumberFound || isNationalCodeFound)
		{
			GendersSelectList =
				await
				Infrastructure.SelectLists.GetGendersForUserAsync
				(databaseContext: DatabaseContext, selectedValue: ViewModel.GenderId);

			return Page();
		}
		// **************************************************

		// **************************************************
		foundedUser.SetUpdateDateTime();

		foundedUser.Username = username;
		foundedUser.NationalCode = nationalCode;
		foundedUser.CellPhoneNumber = cellPhoneNumber;

		foundedUser.GenderId = ViewModel.GenderId;
		foundedUser.IsProfilePublic = ViewModel.IsProfilePublic;

		foundedUser.ImageUrl = ViewModel.ImageUrl.Fix();
		foundedUser.WideImageUrl = ViewModel.WideImageUrl.Fix();
		// **************************************************

		// **************************************************
		var foundedLocalizedUser =
			foundedUser.LocalizedUsers
			.Where(current => current.CultureId == currentCulture.Id)
			.FirstOrDefault();

		if (foundedLocalizedUser is not null)
		{
			foundedLocalizedUser.LastName = ViewModel.LastName.Fix()!;
			foundedLocalizedUser.FirstName = ViewModel.FirstName.Fix()!;
			foundedLocalizedUser.Description = ViewModel.Description.Fix();
		}
		else
		{
			var localizedUser =
				new Domain.Features.Identity.LocalizedUser
				(cultureId: currentCulture.Id, userId: foundedUser.Id,
				firstName: ViewModel.FirstName.Fix()!, lastName: ViewModel.LastName.Fix()!)
				{
					Description = ViewModel.Description.Fix(),
				};

			await DatabaseContext.AddAsync(entity: localizedUser);
		}

		await DatabaseContext.SaveChangesAsync();

		GendersSelectList =
			await
			Infrastructure.SelectLists.GetGendersForUserAsync
			(databaseContext: DatabaseContext, selectedValue: null);

		// **************************************************
		var successMessage =
			Resources.Messages.Successes.UpdateProfile;

		AddPageSuccess(message: successMessage);
		// **************************************************

		return Page();
	}
	#endregion /OnPostAsync()

	#endregion /Methods
}
