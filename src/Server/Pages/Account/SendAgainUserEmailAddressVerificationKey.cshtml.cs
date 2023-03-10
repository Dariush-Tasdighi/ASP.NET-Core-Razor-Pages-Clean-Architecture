using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Server.Pages.Account;

public class SendAgainUserEmailAddressVerificationKeyModel :
	Infrastructure.BasePageModelWithDatabaseContext
{
	#region Constructor
	public SendAgainUserEmailAddressVerificationKeyModel
		(Persistence.DatabaseContext databaseContext,
		Services.Features.Identity.UserService userService) :
		base(databaseContext: databaseContext)
	{
		ViewModel = new();
		UserService = userService;
	}
	#endregion /Constructor

	#region Properties

	private Services.Features.Identity.UserService UserService { get; }

	[Microsoft.AspNetCore.Mvc.BindProperty]
	public ViewModels.Pages.Account.SendAgainUserEmailAddressVerificationKeyViewModel ViewModel { get; set; }

	#endregion /Properties

	#region Methods

	#region OnGetAsync()
	public async System.Threading.Tasks.Task OnGetAsync()
	{
		await System.Threading.Tasks.Task.CompletedTask;
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
		var userEmailAddress =
			ViewModel.EmailAddress!.ToLower();

		var foundedUser =
			await
			DatabaseContext.Users

			.Where(current => current.EmailAddress.ToLower() == userEmailAddress)

			.FirstOrDefaultAsync();

		if (foundedUser is null)
		{
			return RedirectToPage(pageName:
				Constants.CommonRouting.NotFound);
		}

		if (foundedUser.IsEmailAddressVerified)
		{
			var message =
				Resources.Messages.Errors
				.YourEmailAddressAlreadyVerified;

			AddToastError(message: message);

			return RedirectToPage(pageName:
				Constants.CommonRouting.Login);
		}
		// **************************************************

		// **************************************************
		try
		{
			await UserService
				.SendUserEmailVerificationKeyAsync(emailAddress: userEmailAddress);
		}
		catch { }
		// **************************************************

		// **************************************************
		var successMessage =
			Resources.Messages.Successes.PasswordChanged;

		AddPageSuccess(message: successMessage);
		// **************************************************

		return Page();
	}
	#endregion /OnPostAsync()

	#endregion /Methods
}
