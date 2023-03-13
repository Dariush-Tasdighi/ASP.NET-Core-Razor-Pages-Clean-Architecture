namespace ViewModels.Pages.Features.Common.Admin.LocalizedMailSettings;

public class TestViewModel : object
{
	#region Constructor
	public TestViewModel() : base()
	{

	}
	#endregion /Constructor

	#region Properties

	#region public string? EmailBody { get; set; }
	/// <summary>
	/// متن نامه
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.EmailBody))]

	[System.ComponentModel.DataAnnotations.Required
		(AllowEmptyStrings = false,
		ErrorMessageResourceType = typeof(Resources.Messages.Validations),
		ErrorMessageResourceName = nameof(Resources.Messages.Validations.Required))]
	public string? EmailBody { get; set; }
	//public string EmailBody { get; set; }
	#endregion /public string? EmailBody { get; set; }

	#region public string? EmailSubject { get; set; }
	/// <summary>
	/// موضوع نامه
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.EmailSubject))]

	[System.ComponentModel.DataAnnotations.Required
		(AllowEmptyStrings = false,
		ErrorMessageResourceType = typeof(Resources.Messages.Validations),
		ErrorMessageResourceName = nameof(Resources.Messages.Validations.Required))]
	public string? EmailSubject { get; set; }
	//public string EmailSubject { get; set; }
	#endregion /public string? EmailSubject { get; set; }

	#region public string? RecipientDisplayName { get; set; }
	/// <summary>
	/// نام و نام خانوادگی گیرنده
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.RecipientDisplayName))]
	public string? RecipientDisplayName { get; set; }
	#endregion /public string? RecipientDisplayName { get; set; }

	#region public string? RecipientEmailAddress { get; set; }
	/// <summary>
	/// نشانی پست الکترونیکی گیرنده
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.RecipientEmailAddress))]

	[System.ComponentModel.DataAnnotations.Required
		(AllowEmptyStrings = false,
		ErrorMessageResourceType = typeof(Resources.Messages.Validations),
		ErrorMessageResourceName = nameof(Resources.Messages.Validations.Required))]

	[System.ComponentModel.DataAnnotations.MaxLength
		(length: Constants.MaxLength.EmailAddress,
		ErrorMessageResourceType = typeof(Resources.Messages.Validations),
		ErrorMessageResourceName = nameof(Resources.Messages.Validations.MaxLength))]

	[System.ComponentModel.DataAnnotations.RegularExpression
		(pattern: Constants.RegularExpression.EmailAddress,
		ErrorMessageResourceType = typeof(Resources.Messages.Validations),
		ErrorMessageResourceName = nameof(Resources.Messages.Validations.EmailAddress))]
	public string? RecipientEmailAddress { get; set; }
	//public string RecipientEmailAddress { get; set; }
	#endregion /public string? RecipientEmailAddress { get; set; }

	#endregion /Properties
}
