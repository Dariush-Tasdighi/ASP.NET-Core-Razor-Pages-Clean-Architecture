using Dtat;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Server.Pages.Account;

[Microsoft.AspNetCore.Authorization.Authorize]
public class UpdateProfileModel :
	Infrastructure.BasePageModelWithDatabaseContext
{
	#region Constructor
	public UpdateProfileModel
		(Persistence.DatabaseContext databaseContext) :
		base(databaseContext: databaseContext)
	{
		ViewModel = new();
	}
	#endregion /Constructor

	#region Properties

	[Microsoft.AspNetCore.Mvc.BindProperty]
	public ViewModels.Pages.Account.UpdateProfileViewModel ViewModel { get; set; }

	public Microsoft.AspNetCore.Mvc.Rendering.SelectList? GendersSelectList { get; set; }

	#endregion /Properties

	#region Methods

	#region OnGetAsync()
	public async System.Threading.Tasks.Task
		<Microsoft.AspNetCore.Mvc.IActionResult> OnGetAsync()
	{
		if (User is null ||
			User.Identity is null ||
			User.Identity.IsAuthenticated == false ||
			string.IsNullOrWhiteSpace(value: User.Identity.Name))
		{
			return RedirectToPage(pageName:
				Constants.CommonRouting.Logout);
		}

		var userEmailAddress = User.Identity.Name;

		var currentUICultureLcid = Domain.Features
			.Common.CultureEnumHelper.GetCurrentUICultureLcid();

		var foundedUser =
			await
			DatabaseContext.Users
			.Include(current => current.Profiles)
			.ThenInclude(current => current.Culture)
			.Where(current => current.EmailAddress.ToLower() == userEmailAddress.ToLower())
			.FirstOrDefaultAsync();

		if (foundedUser is null)
		{
			return RedirectToPage(pageName:
				Constants.CommonRouting.NotFound);
		}

		GendersSelectList =
			await
			Infrastructure.SelectLists.GetGendersForUserAsync
			(databaseContext: DatabaseContext, selectedValue: null);

		ViewModel.NationalCode = foundedUser.NationalCode;
		ViewModel.IsProfilePublic = foundedUser.IsProfilePublic;

		var foundedUserProfile =
			foundedUser.Profiles
			.Where(current => current.Culture is not null &&
				current.Culture.Lcid == currentUICultureLcid)
			.FirstOrDefault();

		if (foundedUserProfile is null)
		{
			return Page();
		}

		ViewModel.GenderId = foundedUserProfile.GenderId;
		ViewModel.LastName = foundedUserProfile.LastName;
		ViewModel.FirstName = foundedUserProfile.FirstName;
		ViewModel.Description = foundedUserProfile.Description;

		return Page();
	}
	#endregion /OnGetAsync()

	#region OnPostAsync()
	public async
		System.Threading.Tasks.Task<Microsoft.AspNetCore.Mvc.IActionResult> OnPostAsync()
	{
		if (User is null ||
			User.Identity is null ||
			User.Identity.IsAuthenticated == false ||
			string.IsNullOrWhiteSpace(value: User.Identity.Name))
		{
			return RedirectToPage(pageName:
				Constants.CommonRouting.Logout);
		}

		if (ModelState.IsValid == false)
		{
			GendersSelectList =
				await
				Infrastructure.SelectLists.GetGendersForUserAsync
				(databaseContext: DatabaseContext, selectedValue: null);

			return Page();
		}

		var userEmailAddress =
			User.Identity.Name.ToLower();

		var currentUICultureLcid = Domain.Features
			.Common.CultureEnumHelper.GetCurrentUICultureLcid();

		var foundedUser =
			await
			DatabaseContext.Users

			.Include(current => current.Profiles)
			.ThenInclude(current => current.Culture)

			.Where(current => current.EmailAddress.ToLower() == userEmailAddress)

			.FirstOrDefaultAsync();

		if (foundedUser is null)
		{
			return RedirectToPage(pageName:
				Constants.CommonRouting.Logout);
		}

		foundedUser.NationalCode = ViewModel.NationalCode;
		foundedUser.IsProfilePublic = ViewModel.IsProfilePublic;

		var foundedUserProfile =
			foundedUser.Profiles
			.Where(current => current.Culture != null &&
				current.Culture.Lcid == currentUICultureLcid)
			.FirstOrDefault();

		if (foundedUserProfile is not null)
		{
			foundedUserProfile.GenderId = ViewModel.GenderId;
			foundedUserProfile.LastName = ViewModel.LastName.Fix()!;
			foundedUserProfile.FirstName = ViewModel.FirstName.Fix()!;

			//foundedUserProfile.LastName = ViewModel.LastName.Fix();
			//foundedUserProfile.FirstName = ViewModel.FirstName.Fix();

			foundedUserProfile.Description = ViewModel.Description.Fix();
		}
		else
		{
			var currentCulture =
				await
				DatabaseContext.Cultures
				.Where(current => current.Lcid == currentUICultureLcid)
				.FirstOrDefaultAsync();

			if (currentCulture is null)
			{
				return RedirectToPage(pageName:
					Constants.CommonRouting.InternalServerError);
			}

			var userProfile =
				new Domain.Features.Identity.UserProfile
				(cultureId: currentCulture.Id,
				userId: foundedUser.Id,
				genderId: ViewModel.GenderId,
				firstName: ViewModel.FirstName.Fix()!,
				lastName: ViewModel.LastName.Fix()!)

				//firstName: ViewModel.FirstName.Fix(),
				//lastName: ViewModel.LastName.Fix())
				{
					Description = ViewModel.Description.Fix(),
				};

			await DatabaseContext.AddAsync(entity: userProfile);
		}

		await DatabaseContext.SaveChangesAsync();

		GendersSelectList =
			await
			Infrastructure.SelectLists.GetGendersForUserAsync
			(databaseContext: DatabaseContext, selectedValue: null);

		// **************************************************
		var successMessage =
			Resources.Messages.Successes.UpdateProfile;

		AddPageSuccess(message: successMessage);
		// **************************************************

		return Page();
	}
	#endregion /OnPostAsync()

	#endregion /Methods
}
