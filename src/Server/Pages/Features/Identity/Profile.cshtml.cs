using Dtat;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Server.Pages.Features.Identity;

public class ProfileModel :
	Infrastructure.BasePageModelWithDatabaseContext
{
	#region Constructor
	public ProfileModel
		(Persistence.DatabaseContext databaseContext) :
		base(databaseContext: databaseContext)
	{
		ViewModel = new();
	}
	#endregion /Constructor

	#region Properties
	public ViewModels.Pages.Features.Identity.ProfileViewModel ViewModel { get; set; }
	#endregion /Properties

	#region Methods

	#region OnGetAsync()
	public async System.Threading.Tasks.Task
		<Microsoft.AspNetCore.Mvc.IActionResult> OnGetAsync
		(string? culture = null, string? username = null)
	{
		// **************************************************
		culture =
			culture.Fix();

		if (culture is null)
		{
			return RedirectToPage(pageName:
				Constants.CommonRouting.BadRequest);
		}

		culture = culture.Replace
			(oldValue: " ", newValue: string.Empty);
		// **************************************************

		// **************************************************
		username =
			username.Fix();

		if (username is null)
		{
			return RedirectToPage(pageName:
				Constants.CommonRouting.BadRequest);
		}

		username = username.Replace
			(oldValue: " ", newValue: string.Empty);
		// **************************************************

		// **************************************************
		// دارد SEO‌ روش و نگاه ذیل مشکل
		//var currentUICulture =
		//	System.Globalization.CultureInfo.CurrentUICulture;

		var foundedCulture =
			await
			DatabaseContext.Cultures
			.Where(current => current.CultureName.ToLower() == culture.ToLower())
			.FirstOrDefaultAsync();

		if (foundedCulture is null)
		{
			return RedirectToPage(pageName:
				Constants.CommonRouting.InternalServerError);
		}

		if (foundedCulture.IsActive == false)
		{
			return RedirectToPage(pageName:
				Constants.CommonRouting.NotFound);
		}
		// **************************************************

		// **************************************************
		var foundedUserProfile =
			await
			DatabaseContext.UserProfiles

			.Include(current => current.User)

			.Where(current => current.Culture != null
				&& current.Culture.Id == foundedCulture.Id)

			.Where(current => current.User != null
				&& current.User.Username != null
				&& current.User.Username.ToLower() == username.ToLower())

			.FirstOrDefaultAsync();

		if (foundedUserProfile is null)
		{
			return RedirectToPage(pageName:
				Constants.CommonRouting.NotFound);
		}

		if (foundedUserProfile.User is null)
		{
			return RedirectToPage(pageName:
				Constants.CommonRouting.NotFound);
		}

		if (foundedUserProfile.User.IsActive == false)
		{
			return RedirectToPage(pageName:
				Constants.CommonRouting.NotFound);
		}

		if (foundedUserProfile.User.IsProfilePublic == false)
		{
			return RedirectToPage(pageName:
				Constants.CommonRouting.NotFound);
		}
		// **************************************************

		// **************************************************
		foundedUserProfile.Hits++;

		await DatabaseContext.SaveChangesAsync();
		// **************************************************

		// **************************************************
		ViewModel =
			new ViewModels.Pages.Features.Identity.ProfileViewModel
			{
				Hits = foundedUserProfile.Hits,
				LastName = foundedUserProfile.LastName,
				FirstName = foundedUserProfile.FirstName,
				Description = foundedUserProfile.Description,
				UpdateDateTime = foundedUserProfile.UpdateDateTime,

				EmailAddress = foundedUserProfile.User.EmailAddress,
				CellPhoneNumber = foundedUserProfile.User.CellPhoneNumber,
			};
		// **************************************************

		return Page();
	}
	#endregion /OnGetAsync()

	#endregion /Methods
}
