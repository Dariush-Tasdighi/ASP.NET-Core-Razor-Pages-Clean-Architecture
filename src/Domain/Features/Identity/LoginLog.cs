namespace Domain.Features.Identity;

public class LoginLog : Seedwork.Entity
{
	#region Constructor
	public LoginLog(System.Guid userId, string userIP) : base()
	{
		UserIP = userIP;
		UserId = userId;
	}
	#endregion /Constructor

	#region Properties

	#region public System.Guid UserId { get; set; }
	/// <summary>
	/// کاربر
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.User))]

	[System.ComponentModel.DataAnnotations.Required
		(AllowEmptyStrings = false,
		ErrorMessageResourceType = typeof(Resources.Messages.Validations),
		ErrorMessageResourceName = nameof(Resources.Messages.Validations.Required))]
	public System.Guid UserId { get; set; }
	#endregion /public System.Guid UserId { get; set; }

	#region public virtual User? User { get; set; }
	/// <summary>
	/// کاربر
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.User))]

	[System.ComponentModel.DataAnnotations.Required
		(AllowEmptyStrings = false,
		ErrorMessageResourceType = typeof(Resources.Messages.Validations),
		ErrorMessageResourceName = nameof(Resources.Messages.Validations.Required))]
	public virtual User? User { get; set; }
	#endregion /public virtual User? User { get; set; }



	#region public string UserIP { get; set; }
	/// <summary>
	/// آی‌پی کاربر
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.IP))]

	[System.ComponentModel.DataAnnotations.Required
		(AllowEmptyStrings = false,
		ErrorMessageResourceType = typeof(Resources.Messages.Validations),
		ErrorMessageResourceName = nameof(Resources.Messages.Validations.Required))]

	[System.ComponentModel.DataAnnotations.MaxLength
		(length: Constants.MaxLength.IP,
		ErrorMessageResourceType = typeof(Resources.Messages.Validations),
		ErrorMessageResourceName = nameof(Resources.Messages.Validations.MaxLength))]

	[System.ComponentModel.DataAnnotations.RegularExpression
		(pattern: Constants.RegularExpression.IP,
		ErrorMessageResourceType = typeof(Resources.Messages.Validations),
		ErrorMessageResourceName = nameof(Resources.Messages.Validations.IP))]
	public string UserIP { get; set; }
	#endregion /public string UserIP { get; set; }



	#region public System.DateTimeOffset? LogoutDateTime { get; set; }
	/// <summary>
	/// زمان خروج از سامانه
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.LogoutDateTime))]

	[System.ComponentModel.DataAnnotations.Schema.DatabaseGenerated
		(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None)]
	public System.DateTimeOffset? LogoutDateTime { get; set; }
	#endregion /public System.DateTimeOffset? LogoutDateTime { get; set; }

	#endregion /Properties
}
