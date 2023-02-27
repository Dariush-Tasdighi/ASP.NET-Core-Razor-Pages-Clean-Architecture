using Microsoft.AspNetCore.Authentication;

namespace Server.Pages.Account;

[Microsoft.AspNetCore.Authorization.Authorize]
public class LogoutModel : Infrastructure.BasePageModel
{
	public LogoutModel() : base()
	{
	}

	public async System.Threading.Tasks.Task
		<Microsoft.AspNetCore.Mvc.IActionResult> OnGet()
	{
		// using Microsoft.AspNetCore.Authentication;
		await HttpContext.SignOutAsync
			(scheme: Infrastructure.Security.Constants.DefaultScheme);

		return RedirectToPage(pageName:
			Constants.CommonRouting.RootIndex);
	}
}
