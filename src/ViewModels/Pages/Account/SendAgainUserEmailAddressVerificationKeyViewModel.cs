namespace ViewModels.Pages.Account;

public class SendAgainUserEmailAddressVerificationKeyViewModel : object
{
	#region Constructor
	public SendAgainUserEmailAddressVerificationKeyViewModel() : base()
	{
	}
	#endregion /Constructor

	#region Properties

	#region public string? EmailAddress { get; set; }
	/// <summary>
	/// نشانی پست الکترونیکی
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.EmailAddress))]

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
	public string? EmailAddress { get; set; }
	//public string EmailAddress { get; set; }
	#endregion /public string? EmailAddress { get; set; }

	#endregion /Properties

}
