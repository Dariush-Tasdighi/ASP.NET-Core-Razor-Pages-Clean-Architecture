namespace Domain.Features.Common;

public class LocalizedMailSetting :
	Seedwork.LocalizedEntity,
	Dtat.Net.Mail.Abstractions.IMailSetting,
	Dtat.Seedwork.Abstractions.IEntityHasUpdateDateTime
{
	#region Constructor
	public LocalizedMailSetting
		(System.Guid cultureId) : base(cultureId: cultureId)
	{
		UpdateDateTime = InsertDateTime;
	}
	#endregion /Constructor

	#region Properties

	public bool Enabled { get; set; }



	public int SmtpClientTimeout { get; set; }

	public int SmtpClientPortNumber { get; set; }

	public bool SmtpClientSslEnabled { get; set; }

	public string? SmtpClientHostAddress { get; set; }



	public string? SmtpUsername { get; set; }

	public string? SmtpPassword { get; set; }

	public bool UseDefaultCredentials { get; set; }



	public string? SenderDisplayName { get; set; }

	public string? SenderEmailAddress { get; set; }



	public string? SupportDisplayName { get; set; }

	public string? SupportEmailAddress { get; set; }



	public string? BccAddresses { get; set; }

	public string? EmailSubjectTemplate { get; set; }



	#region public System.DateTimeOffset UpdateDateTime { get; private set; }
	/// <summary>
	/// زمان ویرایش
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.UpdateDateTime))]

	[System.ComponentModel.DataAnnotations.Schema.DatabaseGenerated
		(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None)]
	public System.DateTimeOffset UpdateDateTime { get; private set; }
	#endregion /public System.DateTimeOffset UpdateDateTime { get; private set; }

	#endregion /Properties

	#region Methods

	#region SetUpdateDateTime()
	public void SetUpdateDateTime()
	{
		UpdateDateTime = Dtat.DateTime.Now;
	}
	#endregion /SetUpdateDateTime()

	#endregion /Methods
}
