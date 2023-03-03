namespace Domain.Features.Identity;

public class Gender :
	Seedwork.Entity,
	Dtat.Seedwork.Abstractions.IEntityHasIsActive,
	Dtat.Seedwork.Abstractions.IEntityHasOrdering,
	Dtat.Seedwork.Abstractions.IEntityHasUpdateDateTime
{
	#region Constructor
	public Gender(Enums.GenderEnum code) : base()
	{
		Code = code;

		Ordering = 10_000;

		UpdateDateTime = InsertDateTime;

		Users =
			new System.Collections.Generic.List<User>();

		LocalizedGenders =
			new System.Collections.Generic.List<LocalizedGender>();
	}
	#endregion /Constructor

	#region Properties

	#region public bool IsActive { get; set; }
	/// <summary>
	/// وضعیت - فعال/غیرفعال
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

	#region public Enums.GenderEnum Code { get; set; }
	/// <summary>
	/// کد
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.Code))]
	public Enums.GenderEnum Code { get; set; }
	#endregion /public Enums.GenderEnum Code { get; set; }



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

	#region Collections

	public virtual System.Collections.Generic.IList<User> Users { get; private set; }
	public virtual System.Collections.Generic.IList<LocalizedGender> LocalizedGenders { get; private set; }

	#endregion /Collections
}
