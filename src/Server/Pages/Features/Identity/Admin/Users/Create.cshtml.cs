using Dtat;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace Server.Pages.Features.Identity.Admin.Users;

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

	public Microsoft.AspNetCore.Mvc.Rendering.SelectList? RolesSelectList { get; set; }

	public Microsoft.AspNetCore.Mvc.Rendering.SelectList? GendersSelectList { get; set; }

	[Microsoft.AspNetCore.Mvc.BindProperty]
	public ViewModels.Pages.Features.Identity.Admin.Users.CreateViewModel ViewModel { get; set; }

	#endregion /Properties

	#region Methods

	#region OnGetAsync()
	public async System.Threading.Tasks.Task OnGetAsync()
	{
		RolesSelectList =
			await
			Infrastructure.SelectLists.GetRolesAsync
			(databaseContext: DatabaseContext, selectedValue: null);

		GendersSelectList =
			await
			Infrastructure.SelectLists.GetGendersForAdminAsync
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
		var emailAddress =
			ViewModel.EmailAddress.Fix()!.ToLower();

		var isEmailAddressFound =
			await
			DatabaseContext.Users

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
		var password =
			ViewModel.Password.Fix()!;

		var user =
			new Domain.Features.Identity.User(emailAddress: emailAddress,
			roleId: ViewModel.RoleId, genderId: ViewModel.GenderId)
			{
				Username = username,
				Password = Dtat.Security
					.Hashing.GetSha256(text: password),

				Ordering = ViewModel.Ordering,

				NationalCode = nationalCode,
				CellPhoneNumber = cellPhoneNumber,

				IsActive = ViewModel.IsActive,
				IsVerified = ViewModel.IsVerified,
				IsProfilePublic = ViewModel.IsProfilePublic,
				IsEmailAddressVerified = ViewModel.IsEmailAddressVerified,
				IsNationalCodeVerified = ViewModel.IsNationalCodeVerified,
				IsVisibleInContactUsPage = ViewModel.IsVisibleInContactUsPage,
				IsCellPhoneNumberVerified = ViewModel.IsCellPhoneNumberVerified,

				AdminDescription = ViewModel.AdminDescription.Fix(),
			};

		if (ViewModel.IsDeleted)
		{
			user.Delete();
		}
		else
		{
			user.Undelete();
		}

		await DatabaseContext.AddAsync(entity: user);

		var localizedUser =
			new Domain.Features.Identity.LocalizedUser
			(cultureId: currentCulture.Id, userId: user.Id,
			firstName: ViewModel.FirstName.Fix()!, lastName: ViewModel.LastName.Fix()!)
			{
				Hits = ViewModel.Hits,
				Description = ViewModel.Description,
				TitleInContactUsPage = ViewModel.TitleInContactUsPage,
			};

		await DatabaseContext.AddAsync(entity: localizedUser);

		await DatabaseContext.SaveChangesAsync();
		// **************************************************

		// **************************************************
		var successMessage = string.Format
			(Resources.Messages.Successes.Created,
			Resources.DataDictionary.User);

		AddToastSuccess(message: successMessage);
		// **************************************************

		return RedirectToPage(pageName:
			Constants.CommonRouting.CurrentIndex);
	}
	#endregion /OnPostAsync()

	#endregion /Methods
}
