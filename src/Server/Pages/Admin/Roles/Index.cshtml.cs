using System.Linq;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace Server.Pages.Admin.Roles;

//[Microsoft.AspNetCore.Authorization.Authorize]

[Microsoft.AspNetCore.Authorization
	.Authorize(Roles = Constants.Role.Administrator)]
public class IndexModel :
	Infrastructure.BasePageModelWithDatabaseContext
{
	public IndexModel
		(Persistence.DatabaseContext databaseContext,
		Microsoft.Extensions.Logging.ILogger<IndexModel> logger) :
		base(databaseContext: databaseContext)
	{
		Logger = logger;
		//_logger = logger;

		ViewModel =
			new System.Collections.Generic.List
			<ViewModels.Pages.Admin.Roles.IndexItemViewModel>();
	}

	// **********
	private Microsoft.Extensions.Logging.ILogger<IndexModel> Logger { get; }
	//private readonly Microsoft.Extensions.Logging.ILogger<IndexModel> _logger;
	// **********

	// **********
	//public System.Collections.Generic.IList<Domain.Role> ViewModel { get; private set; }

	public System.Collections.Generic.IList
		<ViewModels.Pages.Admin.Roles.IndexItemViewModel> ViewModel
	{ get; private set; }
	// **********

	public async System.Threading.Tasks.Task
		<Microsoft.AspNetCore.Mvc.IActionResult> OnGetAsync()
	{
		try
		{
			//ViewModel =
			//	DatabaseContext.Roles
			//	// ToList() -> using System.Linq;
			//	.ToList()
			//	;

			// SELECT * FROM Roles

			//ViewModel =
			//	await
			//	DatabaseContext.Roles
			//	// ToListAsync() -> using Microsoft.EntityFrameworkCore;
			//	.ToListAsync()
			//	;

			ViewModel =
				await
				DatabaseContext.Roles
				.OrderBy(current => current.Ordering)
				.ThenBy(current => current.Name)
				.Select(current => new ViewModels.Pages.Admin.Roles.IndexItemViewModel
				{
					Id = current.Id,
					Name = current.Name,
					IsActive = current.IsActive,
					Ordering = current.Ordering,
					UserCount = current.Users.Count,
					InsertDateTime = current.InsertDateTime,
					UpdateDateTime = current.UpdateDateTime,
				})
				.ToListAsync()
				;
		}
		catch (System.Exception ex)
		{
			//Logger.Log
			//	(logLevel: LogLevel.Error, message: ex.Message);

			// LogCritical() -> using Microsoft.Extensions.Logging;
			// LogError() -> using Microsoft.Extensions.Logging;
			// LogWarning() -> using Microsoft.Extensions.Logging;
			// LogInformation() -> using Microsoft.Extensions.Logging;
			// LogDebug() -> using Microsoft.Extensions.Logging;
			// LogTrace() -> using Microsoft.Extensions.Logging;

			//Logger.LogCritical();
			//Logger.LogError();
			//Logger.LogWarning();
			//Logger.LogInformation();
			//Logger.LogDebug();
			//Logger.LogTrace();

			//Logger.LogError
			//	(message: ex.Message);

			Logger.LogError
				(message: Constants.Logger.ErrorMessage, args: ex.Message);

			AddPageError
				(message: Resources.Messages.Errors.UnexpectedError);
		}
		finally
		{
			await DisposeDatabaseContextAsync();
		}

		return Page();
	}
}
