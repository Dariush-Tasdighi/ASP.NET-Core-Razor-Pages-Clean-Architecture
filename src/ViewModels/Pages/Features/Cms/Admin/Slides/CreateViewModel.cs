namespace ViewModels.Pages.Features.Cms.Admin.Slides;

public class CreateViewModel : object
{
	#region Constructor
	public CreateViewModel() : base()
	{
		Interval = 5_000;
		Ordering = 10_000;
	}
	#endregion /Constructor

	#region Properties

	#region public bool IsActive { get; set; }
	/// <summary>
	/// وضعیت صفحه
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.IsActive))]
	public bool IsActive { get; set; }
	#endregion /public bool IsActive { get; set; }

	#region public int Ordering { get; set; }
	/// <summary>
	/// چیدمان
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.Ordering))]

	[System.ComponentModel.DataAnnotations.Range
		(minimum: 1, maximum: 100_000,
		ErrorMessageResourceType = typeof(Resources.Messages.Validations),
		ErrorMessageResourceName = nameof(Resources.Messages.Validations.Range))]
	public int Ordering { get; set; }
	#endregion /public int Ordering { get; set; }

	#region public string? Title { get; set; }
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
	public string? Title { get; set; }
	//public string Title { get; set; }
	#endregion /public string? Title { get; set; }

	#region public int Interval { get; set; }
	/// <summary>
	/// فاصله زمانی نمایش
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.Interval))]

	[System.ComponentModel.DataAnnotations.Range
		(minimum: 100, maximum: 60_000,
		ErrorMessageResourceType = typeof(Resources.Messages.Validations),
		ErrorMessageResourceName = nameof(Resources.Messages.Validations.Range))]
	public int Interval { get; set; }
	#endregion /public int Interval { get; set; }

	#region public string? ImageUrl { get; set; }
	/// <summary>
	/// لینک تصویر اسلاید
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.ImageUrl))]

	[System.ComponentModel.DataAnnotations.Required
		(AllowEmptyStrings = false,
		ErrorMessageResourceType = typeof(Resources.Messages.Validations),
		ErrorMessageResourceName = nameof(Resources.Messages.Validations.Required))]
	public string? ImageUrl { get; set; }
	//public string ImageUrl { get; set; }
	#endregion /public string? ImageUrl { get; set; }

	#region public string? NavigationUrl { get; set; }
	/// <summary>
	/// لینک مقصد اسلاید
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.NavigationUrl))]
	public string? NavigationUrl { get; set; }
	#endregion /public string? NavigationUrl { get; set; }

	#region public bool OpenUrlInNewWindow { get; set; }
	/// <summary>
	/// وضعیت
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.OpenUrlInNewWindow))]
	public bool OpenUrlInNewWindow { get; set; }
	#endregion /public bool OpenUrlInNewWindow { get; set; }

	#region public System.DateTimeOffset? PublishStartDateTime { get; set; }
	/// <summary>
	/// تاریخ و زمان شروع انتشار
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.PublishStartDateTime))]
	public System.DateTimeOffset? PublishStartDateTime { get; set; }
	#endregion /public System.DateTimeOffset? PublishStartDateTime { get; set; }

	#region public System.DateTimeOffset? PublishFinishDateTime { get; set; }
	/// <summary>
	/// تاریخ و زمان پایان انتشار
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.PublishFinishDateTime))]
	public System.DateTimeOffset? PublishFinishDateTime { get; set; }
	#endregion /public System.DateTimeOffset? PublishFinishDateTime { get; set; }

	#region public string? Caption { get; set; }
	/// <summary>
	/// توضیحات
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.Caption))]
	public string? Caption { get; set; }
	#endregion /public string? Caption { get; set; }

	#region public string? Description { get; set; }
	/// <summary>
	/// توضیحات
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.Description))]

	[System.ComponentModel.DataAnnotations.MaxLength
		(length: Constants.MaxLength.MetaDescription,
		ErrorMessageResourceType = typeof(Resources.Messages.Validations),
		ErrorMessageResourceName = nameof(Resources.Messages.Validations.MaxLength))]
	public string? Description { get; set; }
	#endregion /public string? Description { get; set; }

	#endregion /Properties
}
