namespace ViewModels.Pages.Features.Cms.Admin.Pages;

public class IndexItemViewModel : object
{
	#region Constructor
	public IndexItemViewModel() : base()
	{
		Name = Resources.DataDictionary.DefaultNullValue;
		Title = Resources.DataDictionary.DefaultNullValue;
		LayoutName = Resources.DataDictionary.DefaultNullValue;
	}
	#endregion /Constructor

	#region Properties

	public System.Guid Id { get; set; }

	public bool IsActive { get; set; }

	public string Name { get; set; }

	public string LayoutName { get; set; }

	public string Title { get; set; }

	public int Hits { get; set; }

	public System.DateTimeOffset InsertDateTime { get; set; }

	public System.DateTimeOffset UpdateDateTime { get; set; }

	#endregion /Properties
}
