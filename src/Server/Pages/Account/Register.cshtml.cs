using Dtat;
using System.Linq;
using Microsoft.Extensions.Logging;
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

	public void OnGet()
	{
	}

	public async System.Threading.Tasks.Task
		<Microsoft.AspNetCore.Mvc.IActionResult> OnPostAsync()
	{
		if (ModelState.IsValid == false)
		{
			return Page();
		}

		// **************************************************
		// در این صفحه خاص نیازی به دستورات ذیل نمی‌باشد
		// **************************************************
		var fixedUsername =
			ViewModel.Username.Fix()!
			.ToLower();

		//var fixedUsername =
		//	ViewModel.Username.Fix()
		//	.ToLower();

		var fixedEmailAddress =
			ViewModel.EmailAddress.Fix()!
			.ToLower();

		//var fixedEmailAddress =
		//	ViewModel.EmailAddress.Fix()
		//	.ToLower();

		var fixedPassword =
			ViewModel.Password.Fix()!;

		//var fixedPassword =
		//	ViewModel.Password.Fix();

		var fixedConfirmPassword =
			ViewModel.ConfirmPassword.Fix();
		// **************************************************

		// **************************************************
		var isUsernameFound =
			await
			DatabaseContext.Users
			.Where(current => current.Username != null
				&& current.Username.ToLower() == fixedUsername)
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
		var isEmailAddressFound =
			await
			DatabaseContext.Users
			.Where(current => current.EmailAddress.ToLower() == fixedEmailAddress)
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
		var simpleUserRole =
			await
			DatabaseContext.Roles
			.Where(current => current.Code ==
				Domain.Features.Identity.Enums.RoleEnum.SimpleUser)
			.FirstOrDefaultAsync();

		if (simpleUserRole is null)
		{
			AddPageError(message: Resources
				.Messages.Errors.UnexpectedError);

			return Page();
		}

		if (simpleUserRole.IsActive == false)
		{
			AddPageError(message: Resources
				.Messages.Errors.UnexpectedError);

			return Page();
		}
		// **************************************************

		// **************************************************
		var user =
			new Domain.Features.Identity.User
			(emailAddress: fixedEmailAddress,
			roleId: simpleUserRole.Id)
			{
				Username = fixedUsername,
				Password = Dtat.Security
				.Hashing.GetSha256(text: fixedPassword),
			};

		await DatabaseContext.AddAsync(entity: user);

		await DatabaseContext.SaveChangesAsync();
		// **************************************************

		AddToastSuccess(message: Resources
			.Messages.Successes.RegistrationDone);

		// **************************************************
		// TODO: Send Verification Key To User Email Address
		// **************************************************

		return RedirectToPage
			(pageName: "/Account/Login");
	}

	#endregion /Methods
}
