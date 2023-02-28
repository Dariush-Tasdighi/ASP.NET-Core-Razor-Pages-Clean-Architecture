namespace Domain.Seedwork;

public abstract class LocalizedEntity : Entity,
	Dtat.Seedwork.Abstractions.IEntityHasCultureId<System.Guid>
{
	#region Constructor
	public LocalizedEntity(System.Guid cultureId) : base()
	{
		CultureId = cultureId;
	}
	#endregion /Constructor

	#region Properties

	#region public System.Guid CultureId { get; private set; }
	/// <summary>
	/// زبان
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.Culture))]

	[System.ComponentModel.DataAnnotations.Required
		(AllowEmptyStrings = false,
		ErrorMessageResourceType = typeof(Resources.Messages.Validations),
		ErrorMessageResourceName = nameof(Resources.Messages.Validations.Required))]
	public System.Guid CultureId { get; private set; }
	#endregion /public System.Guid CultureId { get; private set; }

	#region public virtual Features.Common.Culture? Culture { get; private set; }
	/// <summary>
	/// زبان
	/// </summary>
	[System.ComponentModel.DataAnnotations.Display
		(ResourceType = typeof(Resources.DataDictionary),
		Name = nameof(Resources.DataDictionary.Culture))]

	[System.ComponentModel.DataAnnotations.Required
		(AllowEmptyStrings = false,
		ErrorMessageResourceType = typeof(Resources.Messages.Validations),
		ErrorMessageResourceName = nameof(Resources.Messages.Validations.Required))]
	public virtual Features.Common.Culture? Culture { get; private set; }
	#endregion /public virtual Features.Common.Culture? Culture { get; private set; }

	#endregion /Properties
}
