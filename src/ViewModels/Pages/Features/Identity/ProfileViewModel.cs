namespace ViewModels.Pages.Features.Identity;

public class ProfileViewModel : object
{
	#region Constructor
	public ProfileViewModel()
	{
		EmailAddress = string.Empty;
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
	/// تعداد دفعات بازدید
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.Hits))]
	public int Hits { get; set; }
	#endregion /public int Hits { get; set; }

	#region public int Score { get; set; }
	/// <summary>
	/// امتیاز کاربر
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.Score))]
	public int Score { get; set; }
	#endregion /public int Score { get; set; }

	#region public bool IsFeatured { get; set; }
	/// <summary>
	/// ویژه بودن کاربر
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.IsFeatured))]
	public bool IsFeatured { get; set; }
	#endregion /public bool IsFeatured { get; set; }

	#region public bool IsVerified { get; set; }
	/// <summary>
	/// وضعیت
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.IsVerified))]
	public bool IsVerified { get; set; }
	#endregion /public bool IsVerified { get; set; }

	#region public string? ImageUrl { get; set; }
	/// <summary>
	/// نشانی تصویر
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.ImageUrl))]
	public string? ImageUrl { get; set; }
	#endregion /public string? ImageUrl { get; set; }

	#region public string? LastName { get; set; }
	/// <summary>
	/// نام خانوادگی
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.LastName))]
	public string? LastName { get; set; }
	#endregion /public string? LastName { get; set; }

	#region public string? FirstName { get; set; }
	/// <summary>
	/// نام
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.FirstName))]
	public string? FirstName { get; set; }
	#endregion /public string? FirstName { get; set; }

	#region public string? RoleTitle { get; set; }
	/// <summary>
	/// نام
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.UserRoleTitle))]
	public string? RoleTitle { get; set; }
	#endregion /public string? RoleTitle { get; set; }

	#region public string? GenderTitle { get; set; }
	/// <summary>
	/// نام
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.Gender))]
	public string? GenderTitle { get; set; }
	#endregion /public string? GenderTitle { get; set; }

	#region public string EmailAddress { get; set; }
	/// <summary>
	/// نشانی پست الکترونیکی
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.EmailAddress))]
	public string EmailAddress { get; set; }
	#endregion /public string EmailAddress { get; set; }

	#region public string? Description { get; set; }
	/// <summary>
	/// توضیحات
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.Description))]
	public string? Description { get; set; }
	#endregion /public string? Description { get; set; }

	#region public string? WideImageUrl { get; set; }
	/// <summary>
	/// نشانی تصویر پهن
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.WideImageUrl))]
	public string? WideImageUrl { get; set; }
	#endregion /public string? WideImageUrl { get; set; }

	#region public string? GenderPrefix { get; set; }
	/// <summary>
	/// نام
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.Gender))]
	public string? GenderPrefix { get; set; }
	#endregion /public string? GenderPrefix { get; set; }

	#region public string? CellPhoneNumber { get; set; }
	/// <summary>
	/// شماره تلفن همراه
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.CellPhoneNumber))]
	public string? CellPhoneNumber { get; set; }
	#endregion /public string? CellPhoneNumber { get; set; }

	#region public bool IsEmailAddressVerified { get; set; }
	/// <summary>
	/// نشانی پست الکترونیکی تایید شده است
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.IsEmailAddressVerified))]
	public bool IsEmailAddressVerified { get; set; }
	#endregion /public bool IsEmailAddressVerified { get; set; }

	#region public bool IsCellPhoneNumberVerified { get; set; }
	/// <summary>
	/// شماره تلفن همراه تایید شده است
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.IsCellPhoneNumberVerified))]
	public bool IsCellPhoneNumberVerified { get; set; }
	#endregion /public bool IsCellPhoneNumberVerified { get; set; }

	#region public System.DateTimeOffset InsertDateTime { get; set; }
	/// <summary>
	/// زمان ویرایش
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.RegisterDate))]
	public System.DateTimeOffset InsertDateTime { get; set; }
	#endregion /public System.DateTimeOffset InsertDateTime { get; set; }

	#region public System.DateTimeOffset UpdateDateTime { get; set; }
	/// <summary>
	/// زمان ویرایش
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.UpdateProfileDate))]
	public System.DateTimeOffset UpdateDateTime { get; set; }
	#endregion /public System.DateTimeOffset UpdateDateTime { get; set; }

	#endregion /Properties

	#region Readonly Properties

	public string? DisplayName
	{
		get
		{
			string? result = null;

			if (string.IsNullOrWhiteSpace(GenderPrefix) == false)
			{
				result = GenderPrefix;
			}

			if (string.IsNullOrWhiteSpace(FirstName) == false)
			{
				if (string.IsNullOrWhiteSpace(result) == false)
				{
					result += " ";
				}

				result += FirstName;
			}

			if (string.IsNullOrWhiteSpace(LastName) == false)
			{
				if (string.IsNullOrWhiteSpace(result) == false)
				{
					result += " ";
				}

				result += LastName;
			}

			return result;
		}
	}

	#endregion /Readonly Properties
}
