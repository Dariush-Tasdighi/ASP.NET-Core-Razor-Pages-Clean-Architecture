namespace ViewModels.Pages.Features.Common.Admin.ApplicationSettings;

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

	#region public System.Guid DefaultCultureId { get; set; }
	/// <summary>
	/// زبان پیش‌فرض
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.DefaultCulture))]

	[System.ComponentModel.DataAnnotations.Required
		(AllowEmptyStrings = false,
		ErrorMessageResourceType = typeof(Resources.Messages.Validations),
		ErrorMessageResourceName = nameof(Resources.Messages.Validations.Required))]
	public System.Guid DefaultCultureId { get; set; }
	#endregion /public System.Guid DefaultCultureId { get; set; }



	#region public string? MasterPassword { get; set; }
	/// <summary>
	/// گذرواژه اصلی / شاه کلید
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.MasterPassword))]
	public string? MasterPassword { get; set; }
	#endregion /public string? MasterPassword { get; set; }

	#region public string? GoogleAnalyticsCode { get; set; }
	/// <summary>
	/// Google Analytics Code
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(Name = "Google Analytics Code")]
	public string? GoogleAnalyticsCode { get; set; }
	#endregion /public string? GoogleAnalyticsCode { get; set; }

	#region public string? GoogleTagManagerCode { get; set; }
	/// <summary>
	/// Google Tag Manager Code
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(Name = "Google Tag Manager Code")]
	public string? GoogleTagManagerCode { get; set; }
	#endregion /public string? GoogleTagManagerCode { get; set; }



	#region public int FavoriteUserProfileLevel { get; set; }
	/// <summary>
	/// سطح مورد نظر برای پروفایل کاربر
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.FavoriteUserProfileLevel))]
	public int FavoriteUserProfileLevel { get; set; }
	#endregion /public int FavoriteUserProfileLevel { get; set; }



	#region public bool IsSslEnabled { get; set; }
	/// <summary>
	/// را پشتیبانی می‌کند SSL سامانه
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.IsSslEnabled))]
	public bool IsSslEnabled { get; set; }
	#endregion /public bool IsSslEnabled { get; set; }

	#region public bool IsLoginVisible { get; set; }
	/// <summary>
	/// نمایش ورود به سامانه
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.IsLoginVisible))]
	public bool IsLoginVisible { get; set; }
	#endregion /public bool IsLoginVisible { get; set; }

	#region public bool IsGoogleSsoEnabled { get; set; }
	/// <summary>
	/// امکان ورود به سامانه از طریق گوگل
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.IsGoogleSsoEnabled))]
	public bool IsGoogleSsoEnabled { get; set; }
	#endregion /public bool IsGoogleSsoEnabled { get; set; }

	#region public bool IsRegistrationEnabled { get; set; }
	/// <summary>
	/// ثبت‌نام فعال است
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.IsRegistrationEnabled))]
	public bool IsRegistrationEnabled { get; set; }
	#endregion /public bool IsRegistrationEnabled { get; set; }

	#endregion /Properties
}
