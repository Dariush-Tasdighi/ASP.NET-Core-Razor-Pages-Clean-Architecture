namespace Domain.Features.Cms;

public class MenuItem :
	Seedwork.LocalizedEntity,
	Dtat.Seedwork.Abstractions.IEntityHasUpdateDateTime
{
	#region Constructor
	public MenuItem(System.Guid cultureId, string title) : base(cultureId: cultureId)
	{
		Title = title;

		UpdateDateTime = InsertDateTime;

		Children =
			new System.Collections.Generic.List<MenuItem>();
	}
	#endregion /Constructor

	#region Properties

	#region public System.Guid? ParentId { get; set; }
	[System.ComponentModel.DataAnnotations.Display
		(Name = nameof(Resources.DataDictionary.MenuItem),
		ResourceType = typeof(Resources.DataDictionary))]
	public System.Guid? ParentId { get; set; }
	#endregion /public System.Guid? ParentId { get; set; }

	#region public virtual MenuItem? Parent { get; set; }
	[System.ComponentModel.DataAnnotations.Display
		(Name = nameof(Resources.DataDictionary.MenuItem),
		ResourceType = typeof(Resources.DataDictionary))]
	public virtual MenuItem? Parent { get; set; }
	#endregion /public virtual MenuItem? Parent { get; set; }



	#region public bool IsVisible { get; set; }
	/// <summary>
	/// وضعیت - فعال/غیرفعال
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.IsVisible))]
	public bool IsVisible { get; set; }
	#endregion /public bool IsVisible { get; set; }

	#region public bool IsDisabled { get; set; }
	/// <summary>
	/// نمایش داده می‌شود ولی غیرفعال دیده می‌شود
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.IsDisabled))]
	public bool IsDisabled { get; set; }
	#endregion /public bool IsDisabled { get; set; }

	#region public bool OpenUrlInNewWindow { get; set; }
	/// <summary>
	/// وضعیت
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.OpenUrlInNewWindow))]
	public bool OpenUrlInNewWindow { get; set; }
	#endregion /public bool OpenUrlInNewWindow { get; set; }



	#region public string Title { get; set; }
	/// <summary>
	/// عنوان
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(Name = nameof(Resources.DataDictionary.Title),
		ResourceType = typeof(Resources.DataDictionary))]

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

	#region public string? Description { get; set; }
	/// <summary>
	/// توضیحات
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.Description))]
	public string? Description { get; set; }
	#endregion /public string? Description { get; set; }

	#region public string? NavigationUrl { get; set; }
	/// <summary>
	/// لینک مقصد
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.NavigationUrl))]
	public string? NavigationUrl { get; set; }
	#endregion /public string? NavigationUrl { get; set; }



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
		UpdateDateTime =
			Dtat.DateTime.Now;
	}
	#endregion /SetUpdateDateTime()

	#endregion /Methods

	#region Collections

	public virtual System.Collections.Generic.IList<MenuItem> Children { get; set; }

	#endregion /Collections
}
