namespace Domain.Features.Cms;

public class Post :
	Seedwork.LocalizedEntity,
	Dtat.Seedwork.Abstractions.IEntityHasCode,
	Dtat.Seedwork.Abstractions.IEntityHasIsActive,
	Dtat.Seedwork.Abstractions.IEntityHasOrdering,
	Dtat.Seedwork.Abstractions.IEntityHasIsDeleted,
	Dtat.Seedwork.Abstractions.IEntityHasIsTestData,
	Dtat.Seedwork.Abstractions.IEntityHasUpdateDateTime
{
	#region Constructor
	public Post(System.Guid cultureId,
		System.Guid userId, System.Guid categoryId, string title) : base(cultureId: cultureId)
	{
		Title = title;
		Ordering = 10_000;

		UserId = userId;
		CategoryId = categoryId;

		UpdateDateTime = InsertDateTime;

		Comments =
			new System.Collections.Generic.List<PostComment>();
	}
	#endregion /Constructor

	#region Properties

	#region public System.Guid UserId { get; set; }
	/// <summary>
	/// مالک مطلب
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.User))]

	[System.ComponentModel.DataAnnotations.Required
		(AllowEmptyStrings = false,
		ErrorMessageResourceType = typeof(Resources.Messages.Validations),
		ErrorMessageResourceName = nameof(Resources.Messages.Validations.Required))]
	public System.Guid UserId { get; set; }
	#endregion /public System.Guid UserId { get; set; }

	#region public virtual Identity.User? User { get; set; }
	/// <summary>
	/// مالک مطلب
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.User))]

	[System.ComponentModel.DataAnnotations.Required
		(AllowEmptyStrings = false,
		ErrorMessageResourceType = typeof(Resources.Messages.Validations),
		ErrorMessageResourceName = nameof(Resources.Messages.Validations.Required))]
	public virtual Identity.User? User { get; set; }
	#endregion /public virtual Identity.User? User { get; set; }

	#region public System.Guid CategoryId { get; set; }
	/// <summary>
	/// طبقه‌بندی
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.Category))]

	[System.ComponentModel.DataAnnotations.Required
		(AllowEmptyStrings = false,
		ErrorMessageResourceType = typeof(Resources.Messages.Validations),
		ErrorMessageResourceName = nameof(Resources.Messages.Validations.Required))]
	public System.Guid CategoryId { get; set; }
	#endregion /public System.Guid CategoryId { get; set; }

	#region public virtual PostCategory? Category { get; set; }
	/// <summary>
	/// طبقه‌بندی
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.Category))]

	[System.ComponentModel.DataAnnotations.Required
		(AllowEmptyStrings = false,
		ErrorMessageResourceType = typeof(Resources.Messages.Validations),
		ErrorMessageResourceName = nameof(Resources.Messages.Validations.Required))]
	public virtual PostCategory? Category { get; set; }
	#endregion /public virtual PostCategory? Category { get; set; }



	#region public bool IsDraft { get; set; }
	/// <summary>
	/// وضعیت صفحه
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.IsDraft))]
	public bool IsDraft { get; set; }
	#endregion /public bool IsDraft { get; set; }

	#region public bool IsActive { get; set; }
	/// <summary>
	/// وضعیت صفحه
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.IsActive))]
	public bool IsActive { get; set; }
	#endregion /public bool IsActive { get; set; }

	#region public bool IsFeatured { get; set; }
	/// <summary>
	/// ویژه بودن مطلب
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.IsFeatured))]
	public bool IsFeatured { get; set; }
	#endregion /public bool IsFeatured { get; set; }

	#region public bool IsTestData { get; set; }
	/// <summary>
	/// داده تستی
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.IsTestData))]
	public bool IsTestData { get; set; }
	#endregion /public bool IsTestData { get; set; }

	#region public bool IsDeleted { get; private set; }
	/// <summary>
	/// آیا به طور مجازی حذف شده؟
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.IsDeleted))]
	public bool IsDeleted { get; private set; }
	#endregion /public bool IsDeleted { get; private set; }

	#region public bool IsCommentingEnabled { get; set; }
	/// <summary>
	/// فعال بودن اظهار نظر در صفحه
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.IsCommentingEnabled))]
	public bool IsCommentingEnabled { get; set; }
	#endregion /public bool IsCommentingEnabled { get; set; }

	#region public bool DoesSearchEnginesIndexIt { get; set; }
	/// <summary>
	/// آیا موتورهای جستجو آن را فهرست می کنند؟
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.DoesSearchEnginesIndexIt))]
	public bool DoesSearchEnginesIndexIt { get; set; }
	#endregion /public bool DoesSearchEnginesIndexIt { get; set; }

	#region public bool DoesSearchEnginesFollowIt { get; set; }
	/// <summary>
	/// آیا موتورهای جستجو از آن پیروی می کنند؟
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.DoesSearchEnginesFollowIt))]
	public bool DoesSearchEnginesFollowIt { get; set; }
	#endregion /public bool DoesSearchEnginesFollowIt { get; set; }

	#region public bool DisplayCommentsAfterVerification { get; set; }
	/// <summary>
	/// نمایش کامنت‌ها صرفا بعد از تایید نویسنده
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.DisplayCommentsAfterVerification))]
	public bool DisplayCommentsAfterVerification { get; set; }
	#endregion /public bool DisplayCommentsAfterVerification { get; set; }



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
	/// امتیاز مطلب
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.Score))]
	public int Score { get; set; }
	#endregion /public int Score { get; set; }

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

	#region public int Code { get; private set; }
	/// <summary>
	/// کد
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.Code))]

	[System.ComponentModel.DataAnnotations.Schema.DatabaseGenerated(databaseGeneratedOption:
		System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
	public int Code { get; private set; }
	#endregion /public int Code { get; private set; }



	#region public string Title { get; set; }
	/// <summary>
	/// عنوان صفحه
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.Title))]

	[System.ComponentModel.DataAnnotations.Required
		(AllowEmptyStrings = false,
		ErrorMessageResourceType = typeof(Resources.Messages.Validations),
		ErrorMessageResourceName = nameof(Resources.Messages.Validations.Required))]

	[System.ComponentModel.DataAnnotations.MaxLength
		(length: Constants.MaxLength.MetaTitle,
		ErrorMessageResourceType = typeof(Resources.Messages.Validations),
		ErrorMessageResourceName = nameof(Resources.Messages.Validations.MaxLength))]
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

	#region public string? Author { get; set; }
	/// <summary>
	/// نام و نام خانوادگی کاربر
	/// این فیلد برای جلوگیری از اجرای کوئوری‌های سنگین در نظر گرفته شده است
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.Author))]
	public string? Author { get; set; }
	#endregion /public string? Author { get; set; }

	#region public string? ImageUrl { get; set; }
	/// <summary>
	/// آدرس تصویر
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.ImageUrl))]
	public string? ImageUrl { get; set; }
	#endregion /public string? ImageUrl { get; set; }

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

	#region public string? Introduction { get; set; }
	/// <summary>
	/// مقدمه
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.Introduction))]
	public string? Introduction { get; set; }
	#endregion /public string? Introduction { get; set; }

	#region public string? AdminDescription { get; set; }
	/// <summary>
	/// توضیحات مدیریتی
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.AdminDescription))]
	public string? AdminDescription { get; set; }
	#endregion /public string? AdminDescription { get; set; }



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

	#region public System.DateTimeOffset? DeleteDateTime { get; private set; }
	/// <summary>
	/// زمان حذف مجازی
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.DeleteDateTime))]

	[System.ComponentModel.DataAnnotations.Schema.DatabaseGenerated
		(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None)]
	public System.DateTimeOffset? DeleteDateTime { get; private set; }
	#endregion /public System.DateTimeOffset? DeleteDateTime { get; private set; }

	#endregion /Properties

	#region Methods

	#region Delete()
	public void Delete()
	{
		IsDeleted = true;

		DeleteDateTime =
			Dtat.DateTime.Now;
	}
	#endregion /Delete()

	#region Undelete()
	public void Undelete()
	{
		IsDeleted = false;

		DeleteDateTime = null;
	}
	#endregion /Undelete()

	#region SetUpdateDateTime()
	public void SetUpdateDateTime()
	{
		UpdateDateTime = Dtat.DateTime.Now;
	}
	#endregion /SetUpdateDateTime()

	#endregion /Methods

	#region Collections

	public virtual System.Collections.Generic.IList<PostComment> Comments { get; private set; }

	#endregion /Collections
}
