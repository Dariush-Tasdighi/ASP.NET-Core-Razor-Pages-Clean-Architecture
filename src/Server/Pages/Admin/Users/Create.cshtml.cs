using System.Linq;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Dtat;

namespace Server.Pages.Admin.Users;

[Microsoft.AspNetCore.Authorization
	.Authorize(Roles = Constants.Role.Administrator)]
public class CreateModel : Infrastructure.BasePageModelWithDatabaseContext
{
	#region Constructor
	public CreateModel(Persistence.DatabaseContext databaseContext,
		Microsoft.Extensions.Logging.ILogger<CreateModel> logger) :
		base(databaseContext: databaseContext)
	{
		Logger = logger;
		ViewModel = new();
	}
	#endregion /Constructor

	#region Properties
	private Microsoft.Extensions.Logging.ILogger<CreateModel> Logger { get; }

	[Microsoft.AspNetCore.Mvc.BindProperty]
	public ViewModels.Pages.Admin.Users.CreateViewModel ViewModel { get; set; }

	public Microsoft.AspNetCore.Mvc.Rendering.SelectList? RolesSelectList { get; set; }
	#endregion /Properties

	#region Methods

	#region OnGetAsync
	public async System.Threading.Tasks.Task
		<Microsoft.AspNetCore.Mvc.IActionResult> OnGetAsync()
	{
		try
		{
			RolesSelectList =
				await
				Infrastructure.SelectLists.GetRolesAsync
				(databaseContext: DatabaseContext, selectedValue: null);

			return Page();
		}
		catch (System.Exception ex)
		{
			Logger.LogError
				(message: Constants.Logger.ErrorMessage, args: ex.Message);

			AddToastError
				(message: Resources.Messages.Errors.UnexpectedError);

			return RedirectToPage(pageName: "Index");
		}
		finally
		{
			await DisposeDatabaseContextAsync();
		}
	}
	#endregion /OnGetAsync

	#region OnPostAsync
	public async System.Threading.Tasks.Task
		<Microsoft.AspNetCore.Mvc.IActionResult> OnPostAsync()
	{
		try
		{
			RolesSelectList =
				await
				Infrastructure.SelectLists.GetRolesAsync
				(databaseContext: DatabaseContext, selectedValue: ViewModel.RoleId);

			if (ModelState.IsValid == false)
			{
				return Page();
			}

			// **************************************************
			var fixedEmailAddress =
				ViewModel.EmailAddress.Fix()!;

			//var fixedEmailAddress =
			//	ViewModel.EmailAddress.Fix();

			var foundedAny =
				await
				DatabaseContext.Users
				.Where(current => current.EmailAddress.ToLower() == fixedEmailAddress.ToLower())
				.AnyAsync();

			if (foundedAny)
			{
				var key =
					$"{nameof(ViewModel)}.{nameof(ViewModel.EmailAddress)}";

				var errorMessage = string.Format
					(format: Resources.Messages.Errors.AlreadyExists,
					arg0: Resources.DataDictionary.EmailAddress);

				ModelState.AddModelError
					(key: key, errorMessage: errorMessage);
			}
			// **************************************************

			// **************************************************
			var fixedUsername =
				ViewModel.Username.Fix();

			if (fixedUsername is not null)
			{
				foundedAny =
					await
					DatabaseContext.Users

					.Where(current => current.Username != null
						&& current.Username.ToLower() == fixedUsername.ToLower())

					.AnyAsync();

				if (foundedAny)
				{
					var key =
						$"{nameof(ViewModel)}.{nameof(ViewModel.Username)}";

					var errorMessage = string.Format
						(format: Resources.Messages.Errors.AlreadyExists,
						arg0: Resources.DataDictionary.Username);

					ModelState.AddModelError
						(key: key, errorMessage: errorMessage);
				}
			}
			// **************************************************

			// **************************************************
			var fixedCellPhoneNumber =
				ViewModel.CellPhoneNumber.Fix();

			if (fixedCellPhoneNumber is not null)
			{
				foundedAny =
					await
					DatabaseContext.Users
					.Where(current => current.CellPhoneNumber == fixedCellPhoneNumber)
					.AnyAsync();

				if (foundedAny)
				{
					var key =
						$"{nameof(ViewModel)}.{nameof(ViewModel.CellPhoneNumber)}";

					var errorMessage = string.Format
						(format: Resources.Messages.Errors.AlreadyExists,
						arg0: Resources.DataDictionary.CellPhoneNumber);

					ModelState.AddModelError
						(key: key, errorMessage: errorMessage);
				}
			}
			// **************************************************

			if (ModelState.IsValid == false)
			{
				return Page();
			}

			// **************************************************
			var fixedLastName =
				ViewModel.LastName.Fix();

			var fixedFirstName =
				ViewModel.FirstName.Fix();

			var fixedDescription =
				ViewModel.Description.Fix();

			var fixedAdminDescription =
				ViewModel.AdminDescription.Fix();

			var fixedTitleInContactUsPage =
				ViewModel.TitleInContactUsPage.Fix();

			var hashOfPassword =
				Dtat.Security.Hashing.GetSha256
				(text: ViewModel.Password);

			var newEntity =
				new Domain.Features.Identity.User(emailAddress: fixedEmailAddress, roleId: ViewModel.RoleId)
				{
					//Id
					//Role
					//LoginLogs
					//CreatedPages
					//EmailAddress
					//InsertDateTime
					//UpdateDateTime
					//EmailAddressVerificationKey
					//CellPhoneNumberVerificationKey

					RoleId = ViewModel.RoleId,

					Ordering = ViewModel.Ordering,

					IsSystemic = false,
					IsUndeletable = false,

					IsActive = ViewModel.IsActive,
					IsProfilePublic = ViewModel.IsProfilePublic,
					IsEmailAddressVerified = ViewModel.IsEmailAddressVerified,
					IsVisibleInContactUsPage = ViewModel.IsVisibleInContactUsPage,
					IsCellPhoneNumberVerified = ViewModel.IsCellPhoneNumberVerified,

					Password = hashOfPassword,

					Username = fixedUsername,
					//LastName = fixedLastName,
					//FirstName = fixedFirstName,
					//Description = fixedDescription,
					CellPhoneNumber = fixedCellPhoneNumber,
					AdminDescription = fixedAdminDescription,
					//TitleInContactUsPage = fixedTitleInContactUsPage,
				};

			var entityEntry =
				await
				DatabaseContext.AddAsync(entity: newEntity);

			var affectedRows =
				await
				DatabaseContext.SaveChangesAsync();
			// **************************************************

			// **************************************************
			var successMessage = string.Format
				(format: Resources.Messages.Successes.Created,
				arg0: Resources.DataDictionary.User);

			AddToastSuccess(message: successMessage);
			// **************************************************

			return RedirectToPage(pageName: "Index");
		}
		catch (System.Exception ex)
		{
			Logger.LogError
				(message: Constants.Logger.ErrorMessage, args: ex.Message);

			AddToastError
				(message: Resources.Messages.Errors.UnexpectedError);

			return RedirectToPage(pageName: "Index");
		}
		finally
		{
			//await SetAccessibleRoleAsync()

			await DisposeDatabaseContextAsync();
		}
	}
	#endregion /OnPostAsync

	#endregion /Methods
}
