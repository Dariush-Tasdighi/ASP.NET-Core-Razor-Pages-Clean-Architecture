using System.Xml.Linq;

namespace Domain.Features.Identity;

public class Gender :
	Seedwork.LocalizedEntity,
	Dtat.Seedwork.Abstractions.IEntityHasIsActive,
	Dtat.Seedwork.Abstractions.IEntityHasUpdateDateTime
{
	#region Constructor
	public Gender(System.Guid cultureId,
		Enums.GenderEnum code, string title) : base(cultureId: cultureId)
	{
		Code = code;
		Title = title;

		UpdateDateTime = InsertDateTime;

		UserProfiles =
			new System.Collections.Generic.List<UserProfile>();
	}
	#endregion /Constructor

	#region Properties

	#region public bool IsActive { get; set; }
	/// <summary>
	/// وضعیت - فعال/غیرفعال
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.IsActive))]
	public bool IsActive { get; set; }
	#endregion /public bool IsActive { get; set; }

	#region public Enums.GenderEnum Code { get; set; }
	/// <summary>
	/// کد
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.Code))]
	public Enums.GenderEnum Code { get; set; }
	#endregion /public Enums.GenderEnum Code { get; set; }

	#region public string Title { get; set; }
	/// <summary>
	/// عنوان - خانم / آقا
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

	#region Read Only Properties

	public string DisplayName
	{
		get
		{
			var status =
				Resources.DataDictionary.Inactive;

			if (IsActive)
			{
				status =
					Resources.DataDictionary.Active;
			}
			var result =
				$"{Title} ({status})";

			return result;
		}
	}

	#endregion /Read Only Properties

	#region Methods

	#region SetUpdateDateTime()
	public void SetUpdateDateTime()
	{
		UpdateDateTime = Dtat.DateTime.Now;
	}
	#endregion /SetUpdateDateTime()

	#endregion /Methods

	#region Collections

	public virtual System.Collections.Generic.IList<UserProfile> UserProfiles { get; private set; }

	#endregion /Collections
}
