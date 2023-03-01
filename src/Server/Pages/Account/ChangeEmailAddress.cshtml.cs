using ViewModels.Pages.Account;

namespace Server.Pages.Account;

[Microsoft.AspNetCore.Authorization.Authorize]
public class ChangeEmailAddressModel : Infrastructure.BasePageModel
{
	public ChangeEmailAddressModel() : base()
	{
		ViewModel = new();
	}

	[Microsoft.AspNetCore.Mvc.BindProperty]
	public ChangeEmailAddressViewModel ViewModel { get; set; }

	public void OnGet()
	{
	}
}
