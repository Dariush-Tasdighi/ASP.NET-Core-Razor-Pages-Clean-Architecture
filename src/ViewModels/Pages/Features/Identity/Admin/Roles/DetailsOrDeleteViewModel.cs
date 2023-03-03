namespace ViewModels.Pages.Features.Identity.Admin.Roles;

public class DetailsOrDeleteViewModel : UpdateViewModel
{
	#region Constructor
	public DetailsOrDeleteViewModel() : base()
	{
		Name = Resources.DataDictionary.DefaultNullValue;
	}
	#endregion /Constructor

	#region Properties

	#region public string Name { get; set; }
	/// <summary>
	/// نام
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.Name))]
	public string Name { get; set; }
	#endregion /public string Name { get; set; }

	#region public int UserCount { get; set; }
	/// <summary>
	/// تعداد کاربران
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

	#region public Domain.Features.Identity.Enums.RoleEnum Code { get; set; }
	/// <summary>
	/// کد
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.Code))]
	public Domain.Features.Identity.Enums.RoleEnum Code { get; set; }
	#endregion /public Domain.Features.Identity.Enums.RoleEnum Code { get; set; }

	#endregion Properties
}
