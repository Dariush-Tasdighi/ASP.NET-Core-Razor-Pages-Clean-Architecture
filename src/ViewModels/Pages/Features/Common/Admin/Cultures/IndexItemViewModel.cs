namespace ViewModels.Pages.Features.Common.Admin.Cultures;

public class IndexItemViewModel : object
{
	public IndexItemViewModel() : base()
	{
		NativeName = Resources.DataDictionary.DefaultNullValue;
		CultureName = Resources.DataDictionary.DefaultNullValue;
	}

	public System.Guid Id { get; set; }

	public bool IsActive { get; set; }

	public Domain.Features.Common.Enums.CultureEnum Lcid { get; set; }

	public string CultureName { get; set; }

	public string NativeName { get; set; }

	public int PageCount { get; set; }

	public int PostCount { get; set; }

	public int SlideCount { get; set; }

	public int MenuItemCount { get; set; }

	public int PostCategoryCount { get; set; }

	public System.DateTimeOffset InsertDateTime { get; set; }

	public System.DateTimeOffset UpdateDateTime { get; set; }
}
