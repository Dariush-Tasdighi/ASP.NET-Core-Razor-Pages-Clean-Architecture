using Dtat;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Server.Pages.Features.Cms;

public class CategoryModel :
	Infrastructure.BasePageModelWithDatabaseContext
{
	#region Constructor
	public CategoryModel
		(Persistence.DatabaseContext databaseContext) :
		base(databaseContext: databaseContext)
	{
		ViewModel = new();
	}
	#endregion /Constructor

	#region Properties
	public ViewModels.Pages.Features.Cms.CategoryViewModel ViewModel { get; set; }
	#endregion /Properties

	#region Methods

	#region OnGetAsync()
	public async System.Threading.Tasks.Task
		<Microsoft.AspNetCore.Mvc.IActionResult> OnGetAsync
		(string? culture = null, string? name = null)
	{
		// **************************************************
		culture =
			culture.Fix();

		if (culture is null)
		{
			return RedirectToPage(pageName:
				Constants.CommonRouting.BadRequest);
		}

		culture = culture.Replace
			(oldValue: " ", newValue: string.Empty);
		// **************************************************

		// **************************************************
		name =
			name.Fix();

		if (name is null)
		{
			return RedirectToPage(pageName:
				Constants.CommonRouting.BadRequest);
		}

		name = name.Replace
			(oldValue: " ", newValue: string.Empty);
		// **************************************************

		// **************************************************
		// دارد SEO‌ روش و نگاه ذیل مشکل
		// **************************************************
		//var currentUICulture =
		//	System.Globalization.CultureInfo.CurrentUICulture;
		// **************************************************

		var foundedCulture =
			await
			DatabaseContext.Cultures
			.Where(current => current.CultureName.ToLower() == culture.ToLower())
			.FirstOrDefaultAsync();

		if (foundedCulture is null)
		{
			return RedirectToPage(pageName:
				Constants.CommonRouting.BadRequest);
		}

		if (foundedCulture.IsActive == false)
		{
			return RedirectToPage(pageName:
				Constants.CommonRouting.NotFound);
		}
		// **************************************************

		// **************************************************
		var foundedCategory =
			await
			DatabaseContext.PostCategories

			.Where(current => current.Culture != null
				&& current.Culture.Id == foundedCulture.Id)

			.Where(current => current.Name.ToLower() == name.ToLower())

			.FirstOrDefaultAsync();

		if (foundedCategory is null)
		{
			return RedirectToPage(pageName:
				Constants.CommonRouting.NotFound);
		}

		if (foundedCategory.IsActive == false)
		{
			return RedirectToPage(pageName:
				Constants.CommonRouting.NotFound);
		}
		// **************************************************

		// **************************************************
		foundedCategory.Hits++;

		await DatabaseContext.SaveChangesAsync();
		// **************************************************

		// **************************************************
		var postCount =
			DatabaseContext.Posts

			.Where(current => current.CategoryId == foundedCategory.Id)

			.Where(current => current.IsActive)
			.Where(current => current.IsDraft == false)
			.Where(current => current.IsDeleted == false)

			//.Where(current => current.User != null && current.User.IsActive)
			.Where(current => current.User != null && current.User.IsDeleted == false)

			.Count();
		// **************************************************

		// **************************************************
		ViewModel =
			new ViewModels.Pages.Features.Cms.CategoryViewModel
			{
				Id = foundedCategory.Id,

				ImageUrl = foundedCategory.ImageUrl,
				WideImageUrl = foundedCategory.WideImageUrl,

				Name = foundedCategory.Name,
				Body = foundedCategory.Body,
				Title = foundedCategory.Title,
				Description = foundedCategory.Description,

				PostCount = postCount,
				Hits = foundedCategory.Hits,
				MaxDisplayPostCount = foundedCategory.MaxDisplayPostCount,
			};
		// **************************************************

		return Page();
	}
	#endregion /OnGetAsync()

	#endregion /Methods
}
