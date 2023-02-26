namespace ViewModels.Pages.Features.Identity.Admin.Roles;

public class IndexItemViewModel : object
{
	#region Constructor
	public IndexItemViewModel() : base()
	{
		Name = Resources.DataDictionary.DefaultNullValue;
		Title = Resources.DataDictionary.DefaultNullValue;
	}
	#endregion /Constructor

	#region Properties

	public System.Guid Id { get; set; }

	public bool IsActive { get; set; }

	public string Name { get; set; }

	public Domain.Features.Identity.Enums.RoleEnum Code { get; set; }

	public int Ordering { get; set; }

	public string Title { get; set; }

	public int UserCount { get; set; }

	public System.DateTimeOffset InsertDateTime { get; set; }

	public System.DateTimeOffset UpdateDateTime { get; set; }

	#endregion /Properties
}
