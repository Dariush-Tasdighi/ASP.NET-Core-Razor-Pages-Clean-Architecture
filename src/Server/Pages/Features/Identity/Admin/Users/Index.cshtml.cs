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
	public async System.Threading.Tasks.Task OnGetAsync()
	{
		var currentUICultureLcid = Domain.Features
			.Common.CultureEnumHelper.GetCurrentUICultureLcid();

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

				RoleId = current.Role != null ? current.Role.Id : default,
				RoleTitle = current.Role != null ? current.Role.Title : default,

#pragma warning disable CS8602

				Hits =
					current.Profiles
					.FirstOrDefault(other => other.Culture != null
						&& other.Culture.Lcid == currentUICultureLcid).Hits,

				LastName =
					current.Profiles
					.FirstOrDefault(other => other.Culture != null
						&& other.Culture.Lcid == currentUICultureLcid).LastName,

				FirstName =
					current.Profiles
					.FirstOrDefault(other => other.Culture != null
						&& other.Culture.Lcid == currentUICultureLcid).FirstName,

				GenderId =
					current.Profiles
					.FirstOrDefault(other => other.Culture != null
						&& other.Culture.Lcid == currentUICultureLcid).Gender.Id,

				GenderPrefix =
					current.Profiles
					.FirstOrDefault(other => other.Culture != null
						&& other.Culture.Lcid == currentUICultureLcid).Gender.Prefix,

#pragma warning restore CS8602

			})
			.ToListAsync()
			;
	}
	#endregion /OnGetAsync()

	#endregion /Methods
}
