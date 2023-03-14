namespace Domain.Features.Common;

public class ApplicationSetting :
	Seedwork.Entity,
	Dtat.Seedwork.Abstractions.IEntityHasUpdateDateTime
{
	#region Constructor
	public ApplicationSetting(System.Guid defaultCultureId) : base()
	{
		UpdateDateTime = InsertDateTime;

		DefaultCultureId = defaultCultureId;
	}
	#endregion /Constructor

	#region Properties

	#region public System.Guid DefaultCultureId { get; set; }
	/// <summary>
	/// زبان
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

	#region public virtual Features.Common.Culture? DefaultCulture { get; private set; }
	/// <summary>
	/// زبان
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.DefaultCulture))]

	[System.ComponentModel.DataAnnotations.Required
		(AllowEmptyStrings = false,
		ErrorMessageResourceType = typeof(Resources.Messages.Validations),
		ErrorMessageResourceName = nameof(Resources.Messages.Validations.Required))]
	public virtual Features.Common.Culture? DefaultCulture { get; private set; }
	#endregion /public virtual Features.Common.Culture? DefaultCulture { get; private set; }



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

	#region public bool IsRegistrationEnabled { get; set; }
	/// <summary>
	/// ثبت‌نام فعال است
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.IsRegistrationEnabled))]
	public bool IsRegistrationEnabled { get; set; }
	#endregion /public bool IsRegistrationEnabled { get; set; }



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
		UpdateDateTime = Dtat.DateTime.Now;
	}
	#endregion /SetUpdateDateTime()

	#endregion /Methods
}
