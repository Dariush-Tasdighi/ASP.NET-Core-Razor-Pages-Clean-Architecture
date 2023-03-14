namespace Server.Pages.Features.Common.Admin.LocalizedMailSettings;

[Microsoft.AspNetCore.Authorization
	.Authorize(Roles = Constants.Role.Administrator)]
public class TestModel :
	Infrastructure.BasePageModelWithDatabaseContext
{
	#region Constructor
	public TestModel
		(Persistence.DatabaseContext databaseContext,
		Services.Features.Common.LocalizedMailSettingService localizedMailSettingService,
		Microsoft.AspNetCore.Http.IHttpContextAccessor httpContextAccessor) :
		base(databaseContext: databaseContext)
	{
		ViewModel = new();

		HttpContextAccessor = httpContextAccessor;
		LocalizedMailSettingService = localizedMailSettingService;
	}
	#endregion /Constructor

	#region Properties

	private Microsoft.AspNetCore.Http.IHttpContextAccessor HttpContextAccessor { get; }
	private Services.Features.Common.LocalizedMailSettingService LocalizedMailSettingService { get; }

	[Microsoft.AspNetCore.Mvc.BindProperty]
	public System.Exception? Exception { get; set; }

	[Microsoft.AspNetCore.Mvc.BindProperty]
	public ViewModels.Pages.Features.Common.Admin.LocalizedMailSettings.TestViewModel ViewModel { get; set; }

	#endregion /Properties

	#region Methods

	#region OnGetAsync()
	public async System.Threading.Tasks.Task OnGetAsync()
	{
		ViewModel.EmailBody = "Email Body Test";
		ViewModel.EmailSubject = "Email Subject Test";
		ViewModel.RecipientEmailAddress = "DariushT@GMail.com";
		ViewModel.RecipientDisplayName = "Mr. Dariush Tasdighi";

		await System.Threading.Tasks.Task.CompletedTask;
	}
	#endregion /OnGetAsync()

	#region OnPostAsync()
	public async System.Threading.Tasks.Task
	<Microsoft.AspNetCore.Mvc.IActionResult> OnPostAsync()
	{
		if (ModelState.IsValid == false)
		{
			return Page();
		}

		try
		{
			var localizedMailSetting =
				await
				LocalizedMailSettingService.GetInstanceAsync();

			// **************************************************
			var hostUrl =
				Infrastructure.HttpContextHelper.GetCurrentHostUrl
				(httpContext: HttpContextAccessor.HttpContext);

			var body =
				$"{ViewModel.EmailBody}<br /><p>Host: <a href='{hostUrl}'>{hostUrl}</a> - Version: {Infrastructure.Version.Value}</p>";

			var recipient =
				new System.Net.Mail.MailAddress
				(address: ViewModel.RecipientEmailAddress!,
				displayName: ViewModel.RecipientDisplayName);

			await Dtat.Net.Mail.Utility.SendAsync
				(recipient: recipient, subject: ViewModel.EmailSubject!,
				body: body, mailSetting: localizedMailSetting);
			// **************************************************

			// **************************************************
			var successMessage = string.Format
				(format: Resources.Messages.Successes.EmailSentSuccessfully);

			AddPageSuccess(message: successMessage);
			// **************************************************
		}
		catch (System.Exception ex)
		{
			var exception = ex;

			while (exception != null)
			{
				AddPageError
					(message: exception.Message);

				exception =
					exception.InnerException;
			}
		}

		return Page();
	}
	#endregion /OnPostAsync()

	#endregion /Methods
}
