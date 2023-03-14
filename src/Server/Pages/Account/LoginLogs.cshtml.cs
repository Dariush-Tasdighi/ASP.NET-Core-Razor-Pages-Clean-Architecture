using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Server.Pages.Account;

[Microsoft.AspNetCore.Authorization
	.Authorize(Roles = Constants.Role.Administrator)]
public class LoginLogsModel :
	Infrastructure.BasePageModelWithDatabaseContext
{
	#region Constructor
	public LoginLogsModel
		(Persistence.DatabaseContext databaseContext) :
		base(databaseContext: databaseContext)
	{
		ViewModel = new();
	}
	#endregion /Constructor

	#region Properties

	public System.Collections.Generic.List<Domain.Features.Identity.LoginLog> ViewModel { get; private set; }

	#endregion /Properties

	#region Methods

	#region OnGetAsync()
	public async System.Threading.Tasks.Task OnGetAsync()
	{
		var currentUICultureLcid = Domain.Features
			.Common.CultureEnumHelper.GetCurrentUICultureLcid();

		ViewModel =
			await
			DatabaseContext.LoginLogs

			.OrderByDescending(current => current.InsertDateTime)

			.Skip(count: 0)
			.Take(count: 50)

			.ToListAsync()
			;
	}
	#endregion /OnGetAsync()

	#endregion /Methods
}
