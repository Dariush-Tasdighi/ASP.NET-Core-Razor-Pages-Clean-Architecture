using Dtat;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Server.Pages.Account
{
	public class VerifyUserEmailAddressModel :
		Infrastructure.BasePageModelWithDatabaseContext
	{
		#region Constructor
		public VerifyUserEmailAddressModel
			(Persistence.DatabaseContext databaseContext) :
			base(databaseContext: databaseContext)
		{
		}
		#endregion /Constructor

		#region Methods

		#region OnGetAsync()
		public async System.Threading.Tasks.Task
			<Microsoft.AspNetCore.Mvc.IActionResult> OnGetAsync(string? key = null)
		{
			// **************************************************
			key = key.Fix();

			if (key is null)
			{
				return RedirectToPage(pageName:
					Constants.CommonRouting.BadRequest);
			}

			key = key.Replace
				(oldValue: " ", newValue: string.Empty);
			// **************************************************

			// **************************************************
			System.Guid keyGuid;

			try
			{
				keyGuid = new System.Guid(g: key);
			}
			catch
			{
				return RedirectToPage(pageName:
					Constants.CommonRouting.BadRequest);
			}
			// **************************************************

			string message;

			// **************************************************
			var foundedUser =
				await
				DatabaseContext.Users
				.Where(current => current.EmailAddressVerificationKey == keyGuid)
				.FirstOrDefaultAsync();

			if(foundedUser == null)
			{
				return RedirectToPage(pageName:
					Constants.CommonRouting.NotFound);
			}

			if(foundedUser.IsEmailAddressVerified)
			{
				message =
					Resources.Messages.Errors
					.YourEmailAddressAlreadyVerified;

				AddToastError(message: message);

				return RedirectToPage(pageName:
					Constants.CommonRouting.Login);
			}
			// **************************************************

			// **************************************************
			foundedUser.IsEmailAddressVerified = true;

			await DatabaseContext.SaveChangesAsync();

			message =
				Resources.Messages.Successes.YourEmailAddressVerified;

			AddToastSuccess(message: message);

			return RedirectToPage(pageName:
				Constants.CommonRouting.Login);
			// **************************************************
		}
	}
	#endregion /OnGetAsync()

	#endregion /Methods
}
