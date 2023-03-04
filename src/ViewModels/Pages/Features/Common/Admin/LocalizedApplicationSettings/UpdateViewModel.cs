namespace ViewModels.Pages.Features.Common.Admin.LocalizedApplicationSettings;

public class UpdateViewModel : object
{
	#region Constructor
	public UpdateViewModel() : base()
	{
	}
	#endregion /Constructor

	#region Properties

	#region public System.Guid Id { get; set; }
	/// <summary>
	/// شناسه
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.Id))]
	public System.Guid Id { get; set; }
	#endregion /public System.Guid Id { get; set; }



	#region public int Hits { get; set; }
	/// <summary>
	/// تعداد دفعات بازدید در زبان
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.Hits))]
	public int Hits { get; set; }
	#endregion /public int Hits { get; set; }



	#region public string? NavbarBrandText { get; set; }
	/// <summary>
	/// Navbar Brand Text
	/// </summary>
	//[System.ComponentModel.DataAnnotations.Display
	//	(ResourceType = typeof(Resources.DataDictionary),
	//	Name = nameof(Resources.DataDictionary.NavbarBrandText))]
	public string? NavbarBrandText { get; set; }
	#endregion /public string? NavbarBrandText { get; set; }

	#region public string? NavbarBrandImageUrl { get; set; }
	/// <summary>
	/// Navbar Brand Image Url
	/// </summary>
	//[System.ComponentModel.DataAnnotations.Display
	//	(ResourceType = typeof(Resources.DataDictionary),
	//	Name = nameof(Resources.DataDictionary.NavbarBrandText))]
	public string? NavbarBrandImageUrl { get; set; }
	#endregion /public string? NavbarBrandImageUrl { get; set; }



	#region public string? HomePageTitle { get; set; }
	/// <summary>
	/// عنوان صفحه اولیه
	/// </summary>
	//[System.ComponentModel.DataAnnotations.Display
	//	(ResourceType = typeof(Resources.DataDictionary),
	//	Name = nameof(Resources.DataDictionary.HomePageTitle))]

	[System.ComponentModel.DataAnnotations.MaxLength
		(length: Constants.MaxLength.MetaTitle,
		ErrorMessageResourceType = typeof(Resources.Messages.Validations),
		ErrorMessageResourceName = nameof(Resources.Messages.Validations.MaxLength))]
	public string? HomePageTitle { get; set; }
	#endregion /public string? HomePageTitle { get; set; }

	#region public string? HomePageAuthor { get; set; }
	/// <summary>
	/// نام و نام خانوادگی مالک پایگاه برای صفحه اولیه
	/// </summary>
	//[System.ComponentModel.DataAnnotations.Display
	//	(ResourceType = typeof(Resources.DataDictionary),
	//	Name = nameof(Resources.DataDictionary.HomePageAuthor))]
	public string? HomePageAuthor { get; set; }
	#endregion /public string? HomePageAuthor { get; set; }

	#region public string? HomePageImageUrl { get; set; }
	/// <summary>
	/// آدرس تصویر صفحه اولیه
	/// </summary>
	//[System.ComponentModel.DataAnnotations.Display
	//	(ResourceType = typeof(Resources.DataDictionary),
	//	Name = nameof(Resources.DataDictionary.HomePageImageUrl))]
	public string? HomePageImageUrl { get; set; }
	#endregion /public string? HomePageImageUrl { get; set; }

	#region public string? HomePageDescription { get; set; }
	/// <summary>
	/// توضیحات صفحه اولیه
	/// </summary>
	//[System.ComponentModel.DataAnnotations.Display
	//	(ResourceType = typeof(Resources.DataDictionary),
	//	Name = nameof(Resources.DataDictionary.HomePageDescription))]

	[System.ComponentModel.DataAnnotations.MaxLength
		(length: Constants.MaxLength.MetaDescription,
		ErrorMessageResourceType = typeof(Resources.Messages.Validations),
		ErrorMessageResourceName = nameof(Resources.Messages.Validations.MaxLength))]
	public string? HomePageDescription { get; set; }
	#endregion /public string? HomePageDescription { get; set; }

	#endregion /Properties
}
