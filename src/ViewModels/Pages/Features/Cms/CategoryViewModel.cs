﻿namespace ViewModels.Pages.Features.Cms;

public class CategoryViewModel : object
{
	#region Constructor
	public CategoryViewModel() : base()
	{
		Name = string.Empty;
		Title = string.Empty;
	}
	#endregion /Constructor

	#region Properties

	public System.Guid Id { get; set; }

	public string Name { get; set; }
	public string? Body { get; set; }
	public string Title { get; set; }
	public string? Description { get; set; }

	public string? ImageUrl { get; set; }
	public string? CoverImageUrl { get; set; }

	public int Hits { get; set; }
	public int PostCount { get; set; }
	public int MaxDisplayPostCount { get; set; }

	#endregion /Properties
}
