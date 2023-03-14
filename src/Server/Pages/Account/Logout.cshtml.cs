using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication;

namespace Server.Pages.Account;

[Microsoft.AspNetCore.Authorization.Authorize]
public class LogoutModel :
	Infrastructure.BasePageModelWithDatabaseContext
{
	public LogoutModel(Persistence.DatabaseContext databaseContext,
        Services.Features.Identity.AuthenticatedUserService authenticatedUserService) : base(databaseContext: databaseContext)
	{
		AuthenticatedUserService = authenticatedUserService;
	}

	private Services.Features.Identity.AuthenticatedUserService AuthenticatedUserService { get; }

	public async System.Threading.Tasks.Task
		<Microsoft.AspNetCore.Mvc.IActionResult> OnGet()
	{
		// using Microsoft.AspNetCore.Authentication;
		await HttpContext.SignOutAsync
			(scheme: Infrastructure.Security.Constants.DefaultScheme);

		// **************************************************
		var sessionId =
			AuthenticatedUserService.SessionId;

		if(sessionId is not null)
		{
			var loginLog =
				await
				DatabaseContext.LoginLogs
				.Where(current => current.Id == sessionId.Value)
				.FirstOrDefaultAsync();

			if(loginLog is not null)
			{
				loginLog.LogoutDateTime = Dtat.DateTime.Now;

				await DatabaseContext.SaveChangesAsync();
			}
		}
		// **************************************************

		return RedirectToPage(pageName:
			Constants.CommonRouting.RootIndex);
	}
}
