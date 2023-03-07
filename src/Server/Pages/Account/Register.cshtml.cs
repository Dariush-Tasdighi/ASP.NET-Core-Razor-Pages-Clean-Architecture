using Dtat;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Server.Pages.Account;

public class RegisterModel :
	Infrastructure.BasePageModelWithDatabaseContext
{
	#region Constructor
	public RegisterModel
		(Persistence.DatabaseContext databaseContext,
		Services.Features.Identity.UserService userService,
		Services.Features.Common.ApplicationSettingService applicationSettingService) :
		base(databaseContext: databaseContext)
	{
		ViewModel = new();

		UserService = userService;
		ApplicationSettingService = applicationSettingService;
	}
	#endregion /Constructor

	#region Properties

	[Microsoft.AspNetCore.Mvc.BindProperty]
	public ViewModels.Pages.Account.RegisterViewModel ViewModel { get; set; }

	private Services.Features.Identity.UserService UserService { get; }
	private Services.Features.Common.ApplicationSettingService ApplicationSettingService { get; }

	#endregion /Properties

	#region Methods

	public async System.Threading.Tasks.Task
		<Microsoft.AspNetCore.Mvc.IActionResult> OnGetAsync()
	{
		// **************************************************
		var applicationSetting =
			await
			ApplicationSettingService.GetInstanceAsync();

		if (applicationSetting.IsRegistrationEnabled == false)
		{
			return RedirectToPage(pageName:
				Constants.CommonRouting.NotFound);
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

			//.Where(current => current.Culture != null
			//	&& current.Culture.Lcid == currentUICultureLcid)

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
		var unspecifiedGender =
			await
			DatabaseContext.Genders

			.Where(current => current.Code ==
				Domain.Features.Identity.Enums.GenderEnum.Unspecified)

			.FirstOrDefaultAsync();

		if (unspecifiedGender is null)
		{
			return RedirectToPage(pageName:
				Constants.CommonRouting.NotFound);
		}

		// دستور ذیل نباید نوشته شود
		//if (unspecifiedGender.IsActive == false)
		//{
		//	return RedirectToPage(pageName:
		//		Constants.CommonRouting.NotFound);
		//}
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
			new Domain.Features.Identity.User(emailAddress: emailAddress,
			roleId: simpleUserRole.Id, genderId: unspecifiedGender.Id)
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

		try
		{
			await UserService
				.SendUserEmailVerificationKeyAsync(emailAddress: user.EmailAddress);

			await UserService
				.NotifyAllActiveManagersAfterUserRegistrationAsync(newUser: user);
		}
		catch { }

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
