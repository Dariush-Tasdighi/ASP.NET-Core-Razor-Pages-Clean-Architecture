using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Services.Features.Identity;

public class UserService :
	Infrastructure.BaseServiceWithDatabaseContext
{
	#region Constructor
	public UserService
		(Persistence.DatabaseContext databaseContext,
		Services.Features.Common.LocalizedMailSettingService localizedMailSettingService,
		Microsoft.AspNetCore.Http.IHttpContextAccessor httpContextAccessor) :
		base(databaseContext: databaseContext)
	{
		HttpContextAccessor = httpContextAccessor;
		LocalizedMailSettingService = localizedMailSettingService;
	}
	#endregion /Constructor

	#region Properties

	[Microsoft.AspNetCore.Mvc.BindProperty]
	private Microsoft.AspNetCore.Http.IHttpContextAccessor HttpContextAccessor { get; }
	private Services.Features.Common.LocalizedMailSettingService LocalizedMailSettingService { get; }

	#endregion /Properties

	public async System.Threading.Tasks.Task
		SendUserEmailVerificationKeyAsync(string emailAddress)
	{
		var foundedUser =
			await
			DatabaseContext.Users
			.Where(current => current.EmailAddress.ToLower() == emailAddress.ToLower())
			.FirstOrDefaultAsync();

		if (foundedUser == null)
		{
			// TODO
			var errorMessage =
				"";

			throw new System.Exception(message: errorMessage);
		}

		var localizedMailSetting =
			await
			LocalizedMailSettingService.GetInstanceAsync();

		var subject =
			"Verify Your Email Address!";

		var domainName =
			HttpContextAccessor.HttpContext?.Request.Host.Value;

		var schema =
			HttpContextAccessor.HttpContext?.Request.Scheme;

		var siteUrl = $"{schema}://{domainName}";

		var link = $"{siteUrl}?key={foundedUser.EmailAddressVerificationKey}";

		var body =
			$"<h3>Welcome to our site!</h3>" +
			$"<p>Please click on below link:</p>" +
			$"<p><a href='{link}'>Click Here for Email Address Verification</a></p>" +
			$"<hr />" +
			$"<p>Our Site: {siteUrl}</p>";

		var recipient = new System.Net.Mail.MailAddress
			(address: foundedUser.EmailAddress, displayName: foundedUser.EmailAddress);

		Dtat.Net.Mail.Utility.Send
			(recipient: recipient, subject: subject,
			body: body, mailSetting: localizedMailSetting);
	}

	public async System.Threading.Tasks.Task
		NotifyAllActiveManagersAfterUserRegistrationAsync(Domain.Features.Identity.User newUser)
	{
		var localizedMailSetting =
			await
			LocalizedMailSettingService.GetInstanceAsync();

		var users =
			await
			DatabaseContext.Users

			.Where(current => current.IsActive)
			.Where(current => current.IsDeleted == false)
			.Where(current => current.Role != null && (
				current.Role.Code == Domain.Features.Identity.Enums.RoleEnum.Programmer ||
				current.Role.Code == Domain.Features.Identity.Enums.RoleEnum.Supervisor ||
				current.Role.Code == Domain.Features.Identity.Enums.RoleEnum.Administrator ||
				current.Role.Code == Domain.Features.Identity.Enums.RoleEnum.ApplicationOwner))

			.ToListAsync()
			;

		foreach (var user in users)
		{
			var subject =
				"Verify Your Email Address!";

			var domainName =
				HttpContextAccessor.HttpContext?.Request.Host.Value;

			var schema =
				HttpContextAccessor.HttpContext?.Request.Scheme;

			var siteUrl = $"{schema}://{domainName}";

			var body =
				$"<h3>New User Registered!</h3>" +
				$"<p>Username: {newUser.Username}</p>" +
				$"<p>Email Address: {newUser.EmailAddress}</p>" +
				$"<hr />" +
				$"<p><a href='{siteUrl}'>{siteUrl}</a></p>";

			var recipient = new System.Net.Mail.MailAddress
				(address: user.EmailAddress, displayName: user.EmailAddress);

			Dtat.Net.Mail.Utility.Send
				(recipient: recipient, subject: subject,
				body: body, mailSetting: localizedMailSetting);
		}
	}
}
