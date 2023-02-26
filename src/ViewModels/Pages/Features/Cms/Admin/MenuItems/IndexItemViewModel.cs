namespace ViewModels.Pages.Features.Cms.Admin.MenuItems;

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

	public bool IsVisible { get; set; }

	public bool IsDisabled { get; set; }

	public bool OpenUrlInNewWindow { get; set; }

	public string Title { get; set; }

	public int Ordering { get; set; }

	public string? NavigationUrl { get; set; }

	public int ChildCount { get; set; }

	public System.DateTimeOffset InsertDateTime { get; set; }

	public System.DateTimeOffset UpdateDateTime { get; set; }

	#endregion /Properties
}
