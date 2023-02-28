namespace ViewModels.Pages.Features.Identity.Admin.Genders;

public class IndexItemViewModel : object
{
	#region Constructor
	public IndexItemViewModel() : base()
	{
		Title = Resources.DataDictionary.DefaultNullValue;
	}
	#endregion /Constructor

	#region Properties

	public System.Guid Id { get; set; }

	public bool IsActive { get; set; }

	public Domain.Features.Identity.Enums.GenderEnum Code { get; set; }

	public int Ordering { get; set; }

	public string Title { get; set; }

	public string? Prefix { get; set; }

	public int UserCount { get; set; }

	public System.DateTimeOffset InsertDateTime { get; set; }

	public System.DateTimeOffset UpdateDateTime { get; set; }

	#endregion /Properties
}
