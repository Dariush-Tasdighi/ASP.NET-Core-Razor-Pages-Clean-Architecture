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
		// **************************************************

		var foundedCulture =
			await
			DatabaseContext.Cultures
			.Where(current => current.CultureName.ToLower() == culture.ToLower())
			.FirstOrDefaultAsync();

		if (foundedCulture is null)
		{
			return RedirectToPage(pageName:
				Constants.CommonRouting.NotFound);
		}

		if (foundedCulture.IsActive == false)
		{
			return RedirectToPage(pageName:
				Constants.CommonRouting.NotFound);
		}
		// **************************************************

		// **************************************************
		var foundedLocalizedUser =
			await
			DatabaseContext.LocalizedUsers
			.Include(current => current.User)
			.ThenInclude(user => user!.Role)
			.ThenInclude(role => role!.LocalizedRoles)

			.Where(current => current.Culture != null
				&& current.Culture.Id == foundedCulture.Id)

			.Where(current => current.User != null
				&& current.User.Username != null
				&& current.User.Username.ToLower() == username.ToLower())

			.FirstOrDefaultAsync();

		if (foundedLocalizedUser is null)
		{
			return RedirectToPage(pageName:
				Constants.CommonRouting.NotFound);
		}

		if (foundedLocalizedUser.User is null)
		{
			return RedirectToPage(pageName:
				Constants.CommonRouting.NotFound);
		}

		if (foundedLocalizedUser.User.IsActive == false)
		{
			return RedirectToPage(pageName:
				Constants.CommonRouting.NotFound);
		}

		if (foundedLocalizedUser.User.IsProfilePublic == false)
		{
			return RedirectToPage(pageName:
				Constants.CommonRouting.NotFound);
		}
		// **************************************************

		// **************************************************
		foundedLocalizedUser.Hits++;

		await DatabaseContext.SaveChangesAsync();
		// **************************************************

		var roleTitle =
			foundedLocalizedUser.User?.Role?.LocalizedRoles
			.Where(current => current.CultureId == foundedCulture.Id)
			.FirstOrDefault()?.Title;

		// **************************************************
		ViewModel =
			new ViewModels.Pages.Features.Identity.ProfileViewModel
			{
				RoleTitle = roleTitle,
				Hits = foundedLocalizedUser.Hits,
				LastName = foundedLocalizedUser.LastName,
				FirstName = foundedLocalizedUser.FirstName,
				Description = foundedLocalizedUser.Description,

				InsertDateTime = foundedLocalizedUser.InsertDateTime,
				UpdateDateTime = foundedLocalizedUser.UpdateDateTime,

				EmailAddress = foundedLocalizedUser.User!.EmailAddress,
				CellPhoneNumber = foundedLocalizedUser.User!.CellPhoneNumber,
			};
		// **************************************************

		return Page();
	}
	#endregion /OnGetAsync()

	#endregion /Methods
}
