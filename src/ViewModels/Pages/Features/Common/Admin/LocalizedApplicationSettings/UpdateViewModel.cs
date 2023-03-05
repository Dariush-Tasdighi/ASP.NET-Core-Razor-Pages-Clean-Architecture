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



	#region public string? Copyright { get; set; }
	/// <summary>
	/// کپی‌رایت
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.Copyright))]
	public string? Copyright { get; set; }
	#endregion /public string? Copyright { get; set; }

	#region public string? ApplicationVersioin { get; set; }
	/// <summary>
	/// نسخه سامانه
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.Version))]
	public string? ApplicationVersioin { get; set; }
	#endregion /public string? ApplicationVersioin { get; set; }



	#region public string? NavbarBrandText { get; set; }
	/// <summary>
	/// Navbar Brand Text
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(Name = "Navbar Brand Text")]
	public string? NavbarBrandText { get; set; }
	#endregion /public string? NavbarBrandText { get; set; }

	#region public string? NavbarBrandImageUrl { get; set; }
	/// <summary>
	/// Navbar Brand Image Url
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(Name = "Navbar Brand Image URL")]
	public string? NavbarBrandImageUrl { get; set; }
	#endregion /public string? NavbarBrandImageUrl { get; set; }



	#region public string? HomePageTitle { get; set; }
	/// <summary>
	/// عنوان صفحه اولیه
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(Name = "Home Page Title")]

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
	[System.ComponentModel.DataAnnotations.Display
		(Name = "Home Page Author")]
	public string? HomePageAuthor { get; set; }
	#endregion /public string? HomePageAuthor { get; set; }

	#region public string? HomePageImageUrl { get; set; }
	/// <summary>
	/// نشانی تصویر صفحه اولیه
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(Name = "Home Page ImageUrl")]
	public string? HomePageImageUrl { get; set; }
	#endregion /public string? HomePageImageUrl { get; set; }

	#region public string? HomePageDescription { get; set; }
	/// <summary>
	/// توضیحات صفحه اولیه
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(Name = "Home Page Description")]

	[System.ComponentModel.DataAnnotations.MaxLength
		(length: Constants.MaxLength.MetaDescription,
		ErrorMessageResourceType = typeof(Resources.Messages.Validations),
		ErrorMessageResourceName = nameof(Resources.Messages.Validations.MaxLength))]
	public string? HomePageDescription { get; set; }
	#endregion /public string? HomePageDescription { get; set; }

	#endregion /Properties
}
