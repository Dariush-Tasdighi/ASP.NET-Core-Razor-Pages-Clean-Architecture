using Dtat;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Server.Pages.Features.Common.Admin.LocalizedMailSettings;

[Microsoft.AspNetCore.Authorization
	.Authorize(Roles = Constants.Role.Administrator)]
public class UpdateModel :
	Infrastructure.BasePageModelWithDatabaseContext
{
	#region Constructor
	public UpdateModel
		(Persistence.DatabaseContext databaseContext) :
		base(databaseContext: databaseContext)
	{
		ViewModel = new();
	}
	#endregion /Constructor

	#region Properties

	[Microsoft.AspNetCore.Mvc.BindProperty]
	public ViewModels.Pages.Features.Common.Admin.LocalizedMailSettings.UpdateViewModel ViewModel { get; set; }

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

		// **************************************************
		var foundedItem =
			await
			DatabaseContext.LocalizedMailSettings
			.Where(current => current.CultureId == currentCulture.Id)
			.FirstOrDefaultAsync();

		if (foundedItem is null)
		{
			foundedItem = new Domain.Features.Common
				.LocalizedMailSetting(cultureId: currentCulture.Id);

			await DatabaseContext.AddAsync(entity: foundedItem);

			await DatabaseContext.SaveChangesAsync();
		}
		// **************************************************

		ViewModel =
			new ViewModels.Pages.Features.Common
			.Admin.LocalizedMailSettings.UpdateViewModel()
			{
				Id = foundedItem.Id,

				Enabled = foundedItem.Enabled,

				SmtpPassword = foundedItem.SmtpPassword,
				SmtpUsername = foundedItem.SmtpUsername,

				SmtpClientTimeout = foundedItem.SmtpClientTimeout,
				SmtpClientPortNumber = foundedItem.SmtpClientPortNumber,
				SmtpClientSslEnabled = foundedItem.SmtpClientSslEnabled,
				SmtpClientHostAddress = foundedItem.SmtpClientHostAddress,

				BccAddresses = foundedItem.BccAddresses,
				SenderDisplayName = foundedItem.SenderDisplayName,
				SenderEmailAddress = foundedItem.SenderEmailAddress,
				SupportDisplayName = foundedItem.SupportDisplayName,
				SupportEmailAddress = foundedItem.SupportEmailAddress,
				EmailSubjectTemplate = foundedItem.EmailSubjectTemplate,
				UseDefaultCredentials = foundedItem.UseDefaultCredentials,
			};

		return Page();
	}
	#endregion /OnGetAsync()

	#region OnPostAsync()
	public async System.Threading.Tasks.Task
		<Microsoft.AspNetCore.Mvc.IActionResult> OnPostAsync()
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

		if (ModelState.IsValid == false)
		{
			return Page();
		}

		// **************************************************
		var foundedItem =
			await
			DatabaseContext.LocalizedMailSettings

			.Where(current => current.CultureId == currentCulture.Id)

			.FirstOrDefaultAsync();

		if (foundedItem is null)
		{
			return RedirectToPage(pageName:
				Constants.CommonRouting.NotFound);
		}
		// **************************************************

		// **************************************************
		foundedItem.SetUpdateDateTime();

		foundedItem.SmtpClientTimeout = ViewModel.SmtpClientTimeout;
		foundedItem.SmtpClientPortNumber = ViewModel.SmtpClientPortNumber;

		foundedItem.Enabled = ViewModel.Enabled;
		foundedItem.SmtpClientSslEnabled = ViewModel.SmtpClientSslEnabled;
		foundedItem.UseDefaultCredentials = ViewModel.UseDefaultCredentials;

		foundedItem.BccAddresses = ViewModel.BccAddresses.Fix();
		foundedItem.SmtpPassword = ViewModel.SmtpPassword.Fix();
		foundedItem.SmtpUsername = ViewModel.SmtpUsername.Fix();
		foundedItem.SenderDisplayName = ViewModel.SenderDisplayName.Fix();
		foundedItem.SenderEmailAddress = ViewModel.SenderEmailAddress.Fix();
		foundedItem.SupportDisplayName = ViewModel.SupportDisplayName.Fix();
		foundedItem.SupportEmailAddress = ViewModel.SupportEmailAddress.Fix();
		foundedItem.EmailSubjectTemplate = ViewModel.EmailSubjectTemplate.Fix();
		foundedItem.SmtpClientHostAddress = ViewModel.SmtpClientHostAddress.Fix();
		// **************************************************

		await DatabaseContext.SaveChangesAsync();

		// **************************************************
		var successMessage = string.Format
			(format: Resources.Messages.Successes.Updated,
			arg0: Resources.DataDictionary.Data);

		AddToastSuccess(message: successMessage);
		// **************************************************

		return Page();
	}
	#endregion /OnPostAsync()

	#endregion /Methods
}
