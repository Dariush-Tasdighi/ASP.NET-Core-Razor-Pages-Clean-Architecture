namespace ViewModels.Pages.Features.Cms.Admin.Slides;

public class IndexItemViewModel : object
{
	#region Constructor
	public IndexItemViewModel() : base()
	{
		Title = Resources.DataDictionary.DefaultNullValue;
		ImageUrl = Resources.DataDictionary.DefaultNullValue;
	}
	#endregion /Constructor

	#region Properties

	public System.Guid Id { get; set; }

	public bool IsActive { get; set; }

	public int Ordering { get; set; }

	public string Title { get; set; }

	public int Interval { get; set; }

	public string ImageUrl { get; set; }

	public string? NavigationUrl { get; set; }

	public bool OpenUrlInNewWindow { get; set; }

	public System.DateTimeOffset InsertDateTime { get; set; }

	public System.DateTimeOffset UpdateDateTime { get; set; }

	public System.DateTimeOffset? PublishStartDateTime { get; set; }

	public System.DateTimeOffset? PublishFinishDateTime { get; set; }

	#endregion /Properties
}
