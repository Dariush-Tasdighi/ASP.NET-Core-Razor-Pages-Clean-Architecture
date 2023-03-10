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
		HttpContextAccessor = httpContextAccessor;
		LocalizedMailSettingService = localizedMailSettingService;
	}
	#endregion /Constructor

	#region Properties

	[Microsoft.AspNetCore.Mvc.BindProperty]
	public System.Exception? ViewModel { get; set; }

	private Microsoft.AspNetCore.Http.IHttpContextAccessor HttpContextAccessor { get; }
	private Services.Features.Common.LocalizedMailSettingService LocalizedMailSettingService { get; }

	#endregion /Properties

	#region Methods

	#region OnGetAsync()
	public async System.Threading.Tasks.Task
		<Microsoft.AspNetCore.Mvc.IActionResult> OnGetAsync()
	{
		var localizedMailSetting =
			await
			LocalizedMailSettingService.GetInstanceAsync();

		try
		{
			// **************************************************
			var subject =
				"Test Subject";

			var domainName =
				HttpContextAccessor.HttpContext?.Request.Host.Value;

			var body =
				$"<h3>Welcome to our site!</h3><p>Domain: {domainName}</p>";

			//var recipient = new System.Net.Mail.MailAddress
			//	(address: "DariushT@GMail.com", displayName: "آقای داریوش تصدیقی");

			var recipient = new System.Net.Mail.MailAddress
				(address: "Dariush.Tasdighi@Hotmail.com", displayName: "آقای داریوش تصدیقی");

			await Dtat.Net.Mail.Utility.SendAsync
				(recipient: recipient, subject: subject,
				body: body, mailSetting: localizedMailSetting);
			// **************************************************

			// **************************************************
			//var subject =
			//	"test mail";

			//var body =
			//	$"hello please test my mail server";

			//var recipient = new System.Net.Mail.MailAddress
			//	(address: "test-fj0awttbi@srv1.mail-tester.com");

			//await Dtat.Net.Mail.Utility.SendAsync
			//	(recipient: recipient, subject: subject,
			//	body: body, mailSetting: localizedMailSetting);
			// **************************************************
		}
		catch (System.Exception ex)
		{
			ViewModel = ex;
		}

		return Page();
	}
	#endregion /OnGetAsync()

	#endregion /Methods
}
