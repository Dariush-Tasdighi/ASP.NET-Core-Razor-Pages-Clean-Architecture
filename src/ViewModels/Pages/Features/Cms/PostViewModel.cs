namespace ViewModels.Pages.Features.Cms;

public class PostViewModel : object
{
	#region Constructor
	public PostViewModel() : base()
	{
	}
	#endregion /Constructor

	#region Properties

	public System.Guid Id { get; set; }
	public System.Guid UserId { get; set; }
	public System.Guid CategoryId { get; set; }



	public int Hits { get; set; }
	public int CommentCount { get; set; }



	public string? Body { get; set; }
	public string? Title { get; set; }
	public string? Author { get; set; }
	public string? ImageUrl { get; set; }
	public string? Description { get; set; }
	public string? CategoryName { get; set; }



	public bool IsFeatured { get; set; }
	public bool IsCommentingEnabled { get; set; }
	public bool DoesSearchEnginesIndexIt { get; set; }
	public bool DoesSearchEnginesFollowIt { get; set; }



	public System.DateTimeOffset InsertDateTime { get; set; }
	public System.DateTimeOffset UpdateDateTime { get; set; }

	#endregion /Properties
}
