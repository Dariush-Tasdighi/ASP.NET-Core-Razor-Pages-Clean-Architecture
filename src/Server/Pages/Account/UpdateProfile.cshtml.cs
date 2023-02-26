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

	[Microsoft.AspNetCore.Mvc.BindProperty]
	public ViewModels.Pages.Account.UpdateProfileViewModel ViewModel { get; set; }

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

		if (foundedUser == null)
		{
			return RedirectToPage("/Index");
		}

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

		ViewModel.LastName = foundedUserProfile.LastName;
		ViewModel.FirstName = foundedUserProfile.FirstName;
		ViewModel.Description = foundedUserProfile.Description;

		return Page();
	}

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
			return Page();
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
			return RedirectToPage("/Index");
		}

		foundedUser.NationalCode = ViewModel.NationalCode;
		foundedUser.IsProfilePublic = ViewModel.IsProfilePublic;

		var foundedUserProfile =
			foundedUser.Profiles
			.Where(current => current.Culture is not null &&
				current.Culture.Lcid == currentUICultureLcid)
			.FirstOrDefault();

		if (foundedUserProfile is not null)
		{
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
				return RedirectToPage("/Index");
			}

			var userProfile =
				new Domain.Features.Identity.UserProfile
				(userId: foundedUser.Id,
				cultureId: currentCulture.Id,
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

		// TODO پیغام می‌دهیم

		return Page();
	}
}
