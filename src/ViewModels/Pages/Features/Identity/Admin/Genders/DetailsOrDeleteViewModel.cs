namespace ViewModels.Pages.Features.Identity.Admin.Genders;

public class DetailsOrDeleteViewModel : UpdateViewModel
{
	#region Constructor
	public DetailsOrDeleteViewModel() : base()
	{
	}
	#endregion /Constructor

	#region Properties

	#region public int UserCount { get; set; }
	/// <summary>
	/// تعداد مطالب
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.UserCount))]
	public int UserCount { get; set; }
	#endregion /public int UserCount { get; set; }

	#region public System.DateTimeOffset InsertDateTime { get; set; }
	/// <summary>
	/// زمان ایجاد
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.InsertDateTime))]
	public System.DateTimeOffset InsertDateTime { get; set; }
	#endregion /public System.DateTimeOffset InsertDateTime { get; set; }

	#region public System.DateTimeOffset UpdateDateTime { get; set; }
	/// <summary>
	/// زمان ویرایش
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.UpdateDateTime))]
	public System.DateTimeOffset UpdateDateTime { get; set; }
	#endregion /public System.DateTimeOffset UpdateDateTime { get; set; }

	#region public Domain.Features.Identity.Enums.GenderEnum Code { get; set; }
	/// <summary>
	/// کد
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.Code))]
	public Domain.Features.Identity.Enums.GenderEnum Code { get; set; }
	#endregion /public Domain.Features.Identity.Enums.GenderEnum Code { get; set; }

	#endregion Properties
}
