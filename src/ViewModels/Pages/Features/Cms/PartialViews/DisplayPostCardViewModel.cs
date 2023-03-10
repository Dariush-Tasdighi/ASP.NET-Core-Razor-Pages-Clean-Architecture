namespace ViewModels.Pages.Features.Cms.PartialViews;

public class DisplayPostCardViewModel : object
{
	#region Constructor
	public DisplayPostCardViewModel() : base()
	{
		DisplayAuthor = true;
		DisplayTypeLink = true;
		DisplayCategoryLink = true;

		TextBackground = "text-bg-light";
	}
	#endregion /Constructor

	#region Properties

	public System.Guid Id { get; set; }

	public int Hits { get; set; }

	public string? Title { get; set; }

	public string? ImageUrl { get; set; }
	public string? Username { get; set; }
	public string? Description { get; set; }

	public string? Author { get; set; }
	public bool DisplayAuthor { get; set; }

	public string? TypeName { get; set; }
	public string? TypeTitle { get; set; }
	public bool DisplayTypeLink { get; set; }

	public string? CategoryName { get; set; }
	public string? CategoryTitle { get; set; }
	public bool DisplayCategoryLink { get; set; }

	public string? TextBackground { get; set; }

	public bool IsVerified { get; set; }
	public System.DateTimeOffset? UpdateDateTime { get; set; }

	#endregion /Properties
}
