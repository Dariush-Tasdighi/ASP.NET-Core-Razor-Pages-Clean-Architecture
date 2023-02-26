namespace Server.Pages.Account;

[Microsoft.AspNetCore.Authorization.Authorize]
public class ChangeEmailAddressModel : Infrastructure.BasePageModel
{
	public ChangeEmailAddressModel() : base()
	{
		ViewModel = new();
	}

	[Microsoft.AspNetCore.Mvc.BindProperty]
	public ViewModels.Pages.Account.Users.ChangeEmailAddressViewModel ViewModel { get; set; }

	public void OnGet()
	{
	}
}
