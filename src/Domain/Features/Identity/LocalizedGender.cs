namespace Domain.Features.Identity;

public class LocalizedGender : Seedwork.LocalizedEntity
{
	#region Constructor
	public LocalizedGender(System.Guid cultureId,
		System.Guid genderId, string title) : base(cultureId: cultureId)
	{
		Title = title;
		GenderId = genderId;
	}
	#endregion /Constructor

	#region Properties

	#region public System.Guid GenderId { get; set; }
	/// <summary>
	/// جنیست
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.Gender))]

	[System.ComponentModel.DataAnnotations.Required
		(AllowEmptyStrings = false,
		ErrorMessageResourceType = typeof(Resources.Messages.Validations),
		ErrorMessageResourceName = nameof(Resources.Messages.Validations.Required))]
	public System.Guid GenderId { get; set; }
	#endregion /public System.Guid GenderId { get; set; }

	#region public virtual Gender? Gender { get; set; }
	/// <summary>
	/// جنیست
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.User))]

	[System.ComponentModel.DataAnnotations.Required
		(AllowEmptyStrings = false,
		ErrorMessageResourceType = typeof(Resources.Messages.Validations),
		ErrorMessageResourceName = nameof(Resources.Messages.Validations.Required))]
	public virtual Gender? Gender { get; set; }
	#endregion /public virtual Gender? Gender { get; set; }

	#region public string Title { get; set; }
	/// <summary>
	/// عنوان
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.Title))]

	[System.ComponentModel.DataAnnotations.Required
		(AllowEmptyStrings = false,
		ErrorMessageResourceType = typeof(Resources.Messages.Validations),
		ErrorMessageResourceName = nameof(Resources.Messages.Validations.Required))]

	[System.ComponentModel.DataAnnotations.MaxLength
		(length: Constants.MaxLength.Title,
		ErrorMessageResourceType = typeof(Resources.Messages.Validations),
		ErrorMessageResourceName = nameof(Resources.Messages.Validations.MaxLength))]
	public string Title { get; set; }
	#endregion /public string Title { get; set; }

	#region public string? Prefix { get; set; }
	/// <summary>
	/// پیشوند - خانم / آقای
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.Prefix))]

	[System.ComponentModel.DataAnnotations.MaxLength
		(length: Constants.MaxLength.Prefix,
		ErrorMessageResourceType = typeof(Resources.Messages.Validations),
		ErrorMessageResourceName = nameof(Resources.Messages.Validations.MaxLength))]
	public string? Prefix { get; set; }
	#endregion /public string? Prefix { get; set; }

	#region public string? Description { get; set; }
	/// <summary>
	/// توضیحات
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.Description))]
	public string? Description { get; set; }
	#endregion /public string? Description { get; set; }

	#endregion /Properties
}
