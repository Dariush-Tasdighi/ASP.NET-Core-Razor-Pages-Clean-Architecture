namespace ViewModels.Pages.Features.Common.Admin.LocalizedMailSettings;

public class UpdateViewModel : object
{
	#region Constructor
	public UpdateViewModel() : base()
	{
		Enabled = true;
		SmtpClientPortNumber = 25;
		SmtpClientTimeout = 100_000;
	}
	#endregion /Constructor

	#region Properties

	#region public System.Guid Id { get; set; }
	/// <summary>
	/// شناسه
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.Id))]
	public System.Guid Id { get; set; }
	#endregion /public System.Guid Id { get; set; }



	[System.ComponentModel.DataAnnotations.Display
		(Name = "Enabled")]
	public bool Enabled { get; set; }



	[System.ComponentModel.DataAnnotations.Display
		(Name = "SMTP Client Timeout")]
	public int SmtpClientTimeout { get; set; }

	[System.ComponentModel.DataAnnotations.Display
		(Name = "SMTP Client Port Number")]
	public int SmtpClientPortNumber { get; set; }

	[System.ComponentModel.DataAnnotations.Display
		(Name = "SMTP Client SSL Enabled")]
	public bool SmtpClientSslEnabled { get; set; }

	[System.ComponentModel.DataAnnotations.Display
		(Name = "SMTP Client Host Address")]
	public string? SmtpClientHostAddress { get; set; }



	[System.ComponentModel.DataAnnotations.Display
		(Name = "SMTP Username")]
	public string? SmtpUsername { get; set; }

	[System.ComponentModel.DataAnnotations.Display
		(Name = "SMTP Password")]
	public string? SmtpPassword { get; set; }

	[System.ComponentModel.DataAnnotations.Display
		(Name = "Use Default Credentials")]
	public bool UseDefaultCredentials { get; set; }



	[System.ComponentModel.DataAnnotations.Display
		(Name = "Sender Display Name")]
	public string? SenderDisplayName { get; set; }

	[System.ComponentModel.DataAnnotations.Display
		(Name = "Sender Email Address")]
	public string? SenderEmailAddress { get; set; }



	[System.ComponentModel.DataAnnotations.Display
		(Name = "Support Display Name")]
	public string? SupportDisplayName { get; set; }

	[System.ComponentModel.DataAnnotations.Display
		(Name = "Support Email Address")]
	public string? SupportEmailAddress { get; set; }



	[System.ComponentModel.DataAnnotations.Display
		(Name = "BCC Addresses (,)")]
	public string? BccAddresses { get; set; }

	[System.ComponentModel.DataAnnotations.Display
		(Name = "Email Subject Template")]
	public string? EmailSubjectTemplate { get; set; }

	#endregion /Properties
}
