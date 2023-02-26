namespace ViewModels.Pages.Features.Cms;

public class PageViewModel : object
{
	#region Constructor
	public PageViewModel() : base()
	{
		Title = Resources.DataDictionary.DefaultNullValue;
		LayoutName = Resources.DataDictionary.DefaultNullValue;
	}
	#endregion /Constructor

	#region Properties

	#region public string Title { get; set; }
	/// <summary>
	/// عنوان صفحه
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.Title))]
	public string Title { get; set; }
	#endregion /public string Title { get; set; }

	#region public string? Body { get; set; }
	/// <summary>
	/// متن اصلی
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.Body))]
	public string? Body { get; set; }
	#endregion /public string? Body { get; set; }

	#region public string LayoutName { get; set; }
	/// <summary>
	/// نام ساختار صفحه
	/// </summary>
	public string LayoutName { get; set; }
	#endregion /public string LayoutName { get; set; }

	#region public string? Description { get; set; }
	/// <summary>
	/// توضیحات
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.Description))]
	public string? Description { get; set; }
	#endregion /public string? Description { get; set; }

	#region System.DateTimeOffset UpdateDateTime { get; set; }
	/// <summary>
	/// زمان ویرایش
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.UpdateDateTime))]
	public System.DateTimeOffset UpdateDateTime { get; set; }
	#endregion /System.DateTimeOffset UpdateDateTime { get; set; }

	#endregion /Properties
}
