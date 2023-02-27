using System.Linq;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace Server.Pages.Admin.Users;

[Microsoft.AspNetCore.Authorization
	.Authorize(Roles = Constants.Role.Administrator)]
public class DetailsModel :
	Infrastructure.BasePageModelWithDatabaseContext
{
	#region Constructor
	public DetailsModel
		(Persistence.DatabaseContext databaseContext,
		Microsoft.Extensions.Logging.ILogger<DetailsModel> logger) :
		base(databaseContext: databaseContext)
	{
		Logger = logger;
		ViewModel = new();
	}
	#endregion /Constructor

	#region Properties
	private Microsoft.Extensions.Logging.ILogger<DetailsModel> Logger { get; }

	public ViewModels.Pages.Admin.Users.DetailsOrDeleteViewModel ViewModel { get; private set; }
	#endregion /Properties

	#region OnGetAsync
	public async System.Threading.Tasks.Task
		<Microsoft.AspNetCore.Mvc.IActionResult> OnGetAsync(System.Guid? id)
	{
		try
		{
			if (id is null)
			{
				AddToastError
					(message: Resources.Messages.Errors.IdIsNull);

				return RedirectToPage(pageName:
					Constants.CommonRouting.CurrentIndex);
			}

			var result =
				await
				DatabaseContext.Users
				.Where(current => current.Id == id.Value)
				.Select(current => new ViewModels.Pages.Admin.Users.DetailsOrDeleteViewModel()
				{
					Id = current.Id,

					IsActive = current.IsActive,
					IsSystemic = current.IsSystemic,
					IsUndeletable = current.IsUndeletable,
					IsProfilePublic = current.IsProfilePublic,
					IsEmailAddressVerified = current.IsEmailAddressVerified,
					IsVisibleInContactUsPage = current.IsVisibleInContactUsPage,
					IsCellPhoneNumberVerified = current.IsCellPhoneNumberVerified,

					RoleId = current.RoleId,

					RoleName = current.Role!.Name,
					//RoleName = current.Role.Name,

					IsRoleActive = current.Role.IsActive,

					Ordering = current.Ordering,

					//LastName = current.LastName,
					Username = current.Username,
					//FirstName = current.FirstName,
					//Description = current.Description,
					EmailAddress = current.EmailAddress,
					CellPhoneNumber = current.CellPhoneNumber,
					AdminDescription = current.AdminDescription,
					//TitleInContactUsPage = current.TitleInContactUsPage,

					InsertDateTime = current.InsertDateTime,
					UpdateDateTime = current.UpdateDateTime,
					LastLoginDateTime = current.LastLoginDateTime,
				})
				.FirstOrDefaultAsync();

			if (result is null)
			{
				AddToastError
					(message: Resources.Messages.Errors.ThereIsNotAnyDataWithThisId);

				return RedirectToPage(pageName:
					Constants.CommonRouting.CurrentIndex);
			}

			ViewModel = result;

			return Page();
		}
		catch (System.Exception ex)
		{
			Logger.LogError
				(message: Constants.Logger.ErrorMessage, args: ex.Message);

			AddToastError
				(message: Resources.Messages.Errors.UnexpectedError);

			return RedirectToPage(pageName:
				Constants.CommonRouting.CurrentIndex);
		}
		finally
		{
			await DisposeDatabaseContextAsync();
		}
	}
	#endregion /OnGetAsync
}
