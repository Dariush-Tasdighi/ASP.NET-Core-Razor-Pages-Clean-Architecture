using Dtat;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Server.Pages.Account;

public class RegisterModel :
	Infrastructure.BasePageModelWithDatabaseContext
{
	#region Constructor
	public RegisterModel
		(Persistence.DatabaseContext databaseContext) :
		base(databaseContext: databaseContext)
	{
		ViewModel = new();
	}
	#endregion /Constructor

	#region Properties

	[Microsoft.AspNetCore.Mvc.BindProperty]
	public ViewModels.Pages.Account.RegisterViewModel ViewModel { get; set; }

	#endregion /Properties

	#region Methods

	public async System.Threading.Tasks.Task
		<Microsoft.AspNetCore.Mvc.IActionResult> OnGetAsync()
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

		if (currentCulture.IsActive == false)
		{
			return RedirectToPage(pageName:
				Constants.CommonRouting.NotFound);
		}
		// **************************************************

		// **************************************************
		var simpleUserRole =
			await
			DatabaseContext.Roles

			.Where(current => current.Culture != null
				&& current.Culture.Lcid == currentUICultureLcid)

			.Where(current => current.Code ==
				Domain.Features.Identity.Enums.RoleEnum.SimpleUser)

			.FirstOrDefaultAsync();

		if (simpleUserRole is null)
		{
			return RedirectToPage(pageName:
				Constants.CommonRouting.NotFound);
		}

		if (simpleUserRole.IsActive == false)
		{
			return RedirectToPage(pageName:
				Constants.CommonRouting.NotFound);
		}
		// **************************************************

		return Page();
	}

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
				Constants.CommonRouting.NotFound);
		}

		if (currentCulture.IsActive == false)
		{
			return RedirectToPage(pageName:
				Constants.CommonRouting.NotFound);
		}
		// **************************************************

		// **************************************************
		var simpleUserRole =
			await
			DatabaseContext.Roles

			.Where(current => current.Culture != null
				&& current.Culture.Lcid == currentUICultureLcid)

			.Where(current => current.Code ==
				Domain.Features.Identity.Enums.RoleEnum.SimpleUser)

			.FirstOrDefaultAsync();

		if (simpleUserRole is null)
		{
			return RedirectToPage(pageName:
				Constants.CommonRouting.NotFound);
		}

		if (simpleUserRole.IsActive == false)
		{
			return RedirectToPage(pageName:
				Constants.CommonRouting.NotFound);
		}
		// **************************************************

		// **************************************************
		var username =
			ViewModel.Username.Fix()!.ToLower();

		var isUsernameFound =
			await
			DatabaseContext.Users
			.Where(current => current.Username != null
				&& current.Username.ToLower() == username)
			.AnyAsync()
			;

		if (isUsernameFound)
		{
			var errorMessage = string.Format
				(format: Resources.Messages.Errors.AlreadyExists,
				arg0: Resources.DataDictionary.Username);

			AddPageError(message: errorMessage);
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

		if (isUsernameFound || isEmailAddressFound)
		{
			return Page();
		}

		// **************************************************
		var password =
			ViewModel.Password.Fix()!;

		var user =
			new Domain.Features.Identity.User
			(emailAddress: emailAddress, roleId: simpleUserRole.Id)
			{
				Username = username,
				Password = Dtat.Security
					.Hashing.GetSha256(text: password),
			};

		var entityEntry =
			await
			DatabaseContext.AddAsync(entity: user);

		var affectedRows =
			await
			DatabaseContext.SaveChangesAsync();
		// **************************************************

		AddToastSuccess(message: Resources
			.Messages.Successes.RegistrationDone);

		// **************************************************
		// TODO: Send Verification Key To User Email Address
		// **************************************************

		return RedirectToPage
			(pageName: Constants.CommonRouting.Login);
	}

	#endregion /Methods
}
