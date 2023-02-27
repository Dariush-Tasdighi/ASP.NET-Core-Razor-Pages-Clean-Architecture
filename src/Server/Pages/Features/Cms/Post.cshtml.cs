using Dtat;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Server.Pages.Features.Cms;

public class PostModel :
	Infrastructure.BasePageModelWithDatabaseContext
{
	#region Constructor
	public PostModel
		(Persistence.DatabaseContext databaseContext) :
		base(databaseContext: databaseContext)
	{
		ViewModel = new();
	}
	#endregion /Constructor

	#region Properties
	public ViewModels.Pages.Features.Cms.PostViewModel ViewModel { get; set; }
	#endregion /Properties

	#region Methods

	#region OnGetAsync()
	public async System.Threading.Tasks.Task
		<Microsoft.AspNetCore.Mvc.IActionResult> OnGetAsync
		(string? culture = null, string? id = null)
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
		id =
			id.Fix();

		if (id is null)
		{
			return RedirectToPage(pageName:
				Constants.CommonRouting.BadRequest);
		}

		id = id.Replace
			(oldValue: " ", newValue: string.Empty);

		System.Guid? idGuid = null;

		try
		{
			idGuid =
				new System.Guid(g: id);
		}
		catch
		{
			return RedirectToPage(pageName:
				Constants.CommonRouting.BadRequest);
		}
		// **************************************************

		// **************************************************
		// دارد SEO‌ روش و نگاه ذیل مشکل
		//var currentUICulture =
		//	System.Globalization.CultureInfo.CurrentUICulture;

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
		var foundedPost =
			await
			DatabaseContext.Posts

			.Include(current => current.User)
			.Include(current => current.Category)

			.Where(current => current.Culture != null
				&& current.Culture.Id == foundedCulture.Id)

			.Where(current => current.Id == idGuid.Value)

			.FirstOrDefaultAsync();

		if (foundedPost is null)
		{
			return RedirectToPage(pageName:
				Constants.CommonRouting.NotFound);
		}

		if (foundedPost.IsDraft)
		{
			return RedirectToPage(pageName:
				Constants.CommonRouting.NotFound);
		}

		if (foundedPost.IsDeleted)
		{
			return RedirectToPage(pageName:
				Constants.CommonRouting.NotFound);
		}

		if (foundedPost.IsActive == false)
		{
			return RedirectToPage(pageName:
				Constants.CommonRouting.NotFound);
		}



		if (foundedPost.Category is null)
		{
			return RedirectToPage(pageName:
				Constants.CommonRouting.NotFound);
		}

		if (foundedPost.Category.IsActive == false)
		{
			return RedirectToPage(pageName:
				Constants.CommonRouting.NotFound);
		}



		if (foundedPost.User is null)
		{
			return RedirectToPage(pageName:
				Constants.CommonRouting.NotFound);
		}

		if (foundedPost.User.IsDeleted)
		{
			return RedirectToPage(pageName:
				Constants.CommonRouting.NotFound);
		}

		if (foundedPost.User.IsActive == false)
		{
			return RedirectToPage(pageName:
				Constants.CommonRouting.NotFound);
		}
		// **************************************************

		// **************************************************
		foundedPost.Hits++;

		await DatabaseContext.SaveChangesAsync();
		// **************************************************

		// **************************************************
		ViewModel =
			new ViewModels.Pages.Features.Cms.PostViewModel
			{
				Id = foundedPost.Id,
				UserId = foundedPost.UserId,
				CategoryId = foundedPost.CategoryId,

				Hits = foundedPost.Hits,
				Score = foundedPost.Score,
				CommentCount = foundedPost.Comments.Count,

				Body = foundedPost.Body,
				Title = foundedPost.Title,
				Author = foundedPost.Author,
				ImageUrl = foundedPost.ImageUrl,
				Description = foundedPost.Description,
				CategoryName = foundedPost.Category.Name,

				InsertDateTime = foundedPost.InsertDateTime,
				UpdateDateTime = foundedPost.UpdateDateTime,

				IsFeatured = foundedPost.IsFeatured,
				IsCommentingEnabled = foundedPost.IsCommentingEnabled,
				DoesSearchEnginesIndexIt = foundedPost.DoesSearchEnginesIndexIt,
				DoesSearchEnginesFollowIt = foundedPost.DoesSearchEnginesFollowIt,
			};
		// **************************************************

		return Page();
	}
	#endregion /OnGetAsync()

	#endregion /Methods
}
