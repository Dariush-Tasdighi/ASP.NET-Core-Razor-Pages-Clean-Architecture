namespace ViewModels.Pages.Features.Cms.Admin.Posts;

public class IndexItemViewModel : object
{
	#region Constructor
	public IndexItemViewModel() : base()
	{
		Title = Resources.DataDictionary.DefaultNullValue;
		CategoryName = Resources.DataDictionary.DefaultNullValue;
	}
	#endregion /Constructor

	#region Properties

	public System.Guid Id { get; set; }


	public System.Guid TypeId { get; set; }

	public string? TypeName { get; set; }


	public System.Guid CategoryId { get; set; }

	public string? CategoryName { get; set; }


	public System.Guid UserId { get; set; }

	public string? Author { get; set; }

	public bool IsDraft { get; set; }

	public bool IsActive { get; set; }

	public bool IsFeatured { get; set; }

	public bool IsDeleted { get; set; }

	public bool IsCommentingEnabled { get; set; }

	public string Title { get; set; }

	public int CommentCount { get; set; }

	public int Score { get; set; }

	public int Hits { get; set; }

	public int Ordering { get; set; }

	public System.DateTimeOffset InsertDateTime { get; set; }

	public System.DateTimeOffset UpdateDateTime { get; set; }

	public System.DateTimeOffset? DeleteDateTime { get; set; }

	#endregion /Properties
}
