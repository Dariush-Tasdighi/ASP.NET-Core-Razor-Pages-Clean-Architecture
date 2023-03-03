using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Server.Pages.Features.Identity.Admin.Users;

[Microsoft.AspNetCore.Authorization
	.Authorize(Roles = Constants.Role.Administrator)]
public class IndexModel :
	Infrastructure.BasePageModelWithDatabaseContext
{
	#region Constructor
	public IndexModel
		(Persistence.DatabaseContext databaseContext) :
		base(databaseContext: databaseContext)
	{
		ViewModel = new();
	}
	#endregion /Constructor

	#region Properties

	public System.Collections.Generic.List<ViewModels.Pages.Features.Identity.Admin.Users.IndexItemViewModel> ViewModel { get; private set; }

	#endregion /Properties

	#region Methods

	#region OnGetAsync()
	public async System.Threading.Tasks.Task
		<Microsoft.AspNetCore.Mvc.IActionResult> OnGetAsync()
	{
		// **************************************************
		var currentUICultureLcid = Domain.Features
			.Common.CultureEnumHelper.GetCurrentUICultureLcid();

		var currentCulture =
			await
			DatabaseContext.Cultures
			.Where(current => current.Lcid == currentUICultureLcid)
			.FirstOrDefaultAsync();

		if (currentCulture is null)
		{
			return RedirectToPage(pageName:
				Constants.CommonRouting.NotFound);
		}
		// **************************************************

		ViewModel =
			await
			DatabaseContext.Users

			.OrderByDescending(current => current.UpdateDateTime)

			.Select(current => new ViewModels.Pages
				.Features.Identity.Admin.Users.IndexItemViewModel
			{
				Id = current.Id,

				Ordering = current.Ordering,

				Username = current.Username,
				EmailAddress = current.EmailAddress,
				NationalCode = current.NationalCode,
				CellPhoneNumber = current.CellPhoneNumber,

				DeleteDateTime = current.DeleteDateTime,
				InsertDateTime = current.InsertDateTime,
				UpdateDateTime = current.UpdateDateTime,
				LastLoginDateTime = current.LastLoginDateTime,

				IsActive = current.IsActive,
				IsDeleted = current.IsDeleted,
				IsVerified = current.IsVerified,
				IsUndeletable = current.IsUndeletable,
				IsEmailAddressVerified = current.IsEmailAddressVerified,
				IsNationalCodeVerified = current.IsNationalCodeVerified,
				IsVisibleInContactUsPage = current.IsVisibleInContactUsPage,
				IsCellPhoneNumberVerified = current.IsCellPhoneNumberVerified,

				RoleId = current.RoleId,
				GenderId = current.GenderId,

#pragma warning disable CS8602

				Hits =
					current.LocalizedUsers.FirstOrDefault
					(other => other.CultureId == currentCulture.Id).Hits,

				LastName =
					current.LocalizedUsers.FirstOrDefault
					(other => other.CultureId == currentCulture.Id).LastName,

				FirstName =
					current.LocalizedUsers.FirstOrDefault
					(other => other.CultureId == currentCulture.Id).FirstName,

				RoleTitle =
					current.Role.LocalizedRoles.FirstOrDefault
					(other => other.CultureId == currentCulture.Id).Title,

				GenderPrefix =
					current.Gender.LocalizedGenders.FirstOrDefault
					(other => other.CultureId == currentCulture.Id).Prefix,

#pragma warning restore CS8602

			})
			.ToListAsync()
			;

		return Page();
	}
	#endregion /OnGetAsync()

	#endregion /Methods
}
