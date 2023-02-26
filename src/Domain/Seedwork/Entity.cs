namespace Domain.Seedwork;

public abstract class Entity : object,
	Dtat.Seedwork.Abstractions.IEntity<System.Guid>
{
	#region Constructor
	public Entity() : base()
	{
		Ordering = 10_000;

		InsertDateTime =
			Dtat.DateTime.Now;

		Id = System
			.Guid.NewGuid();
	}
	#endregion /Constructor

	#region Properties

	#region public System.Guid Id { get; protected set; }
	/// <summary>
	/// شناسه
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.Id))]

	[System.ComponentModel.DataAnnotations.Schema.DatabaseGenerated
		(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None)]
	public System.Guid Id { get; protected set; }
	#endregion /public System.Guid Id { get; protected set; }

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

	#region public System.DateTimeOffset InsertDateTime { get; private set; }
	/// <summary>
	/// زمان ایجاد
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.InsertDateTime))]

	[System.ComponentModel.DataAnnotations.Schema.DatabaseGenerated
		(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None)]
	public System.DateTimeOffset InsertDateTime { get; private set; }
	#endregion /public System.DateTimeOffset InsertDateTime { get; private set; }

	#endregion /Properties
}
