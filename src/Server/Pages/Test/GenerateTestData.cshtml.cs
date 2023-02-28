using Dtat;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Server.Pages.Test;

public class GenerateTestDataModel :
	Infrastructure.BasePageModelWithDatabaseContext
{
	public GenerateTestDataModel
		(Persistence.DatabaseContext databaseContext) :
		base(databaseContext: databaseContext)
	{
	}

	public async System.Threading.Tasks.Task OnGet()
	{
		var hasAny =
			DatabaseContext.Posts
			.Where(current => current.IsTestData)
			.Any();

		if (hasAny)
		{
			return;
		}

		hasAny =
			DatabaseContext.PostCategories
			.Where(current => current.IsTestData)
			.Any();

		if (hasAny)
		{
			return;
		}

		await CreatePersianPostCategoriesAsync();
		await CreateEnglishPostCategoriesAsync();
	}

	private async System.Threading.Tasks.Task CreatePersianPostCategoriesAsync()
	{
		var persianCulture =
			await
			DatabaseContext.Cultures
			.Where(current => current.CultureName.ToLower() == "fa-ir")
			.FirstOrDefaultAsync();

		if (persianCulture == null)
		{
			return;
		}

		var dariushUser =
			await
			DatabaseContext.Users
			.Where(current => current.EmailAddress.ToLower() == "DariushT@GMail.com")
			.FirstOrDefaultAsync();

		if (dariushUser == null)
		{
			return;
		}

		var postTitleTemplate = "عنوان مطلب";
		var postDescriptionTemplate = "توضیحات مطلب";
		var postCategoryNameTemplate = "طبقه‌بندی مطلب";

		for (var postCategoryIndex = 1; postCategoryIndex <= 10; postCategoryIndex++)
		{
			var postCategoryIndexString =
				postCategoryIndex
				.ToString().PadLeft(totalWidth: 2, paddingChar: '0')
				.ConvertDigitsToUnicode();

			var postCategoryName =
				$"{postCategoryNameTemplate} {postCategoryIndexString}";

			var postCategory =
				new Domain.Features.Cms.PostCategory
				(cultureId: persianCulture.Id, name: postCategoryName)
				{
					IsTestData = true,
					IsActive = (postCategoryIndex % 2 == 0),
					DisplayInHomePage = (postCategoryIndex % 3 == 0),
				};

			await DatabaseContext.AddAsync(entity: postCategory);

			for (var postIndex = 1; postIndex <= 10; postIndex++)
			{
				var postIndexString =
					postIndex
					.ToString().PadLeft(totalWidth: 2, paddingChar: '0')
					.ConvertDigitsToUnicode();

				var postTitle =
					$"{postTitleTemplate} {postIndexString}";

				var postDescription =
					$"{postDescriptionTemplate} {postIndexString}";

				var post =
					new Domain.Features.Cms.Post
					(cultureId: persianCulture.Id, userId: dariushUser.Id,
					categoryId: postCategory.Id, title: postTitle)
					{
						IsTestData = true,

						IsActive = (postIndex % 2 == 0),

						IsDraft = false,
						//IsDraft = (postIndex % 3 == 0),

						IsFeatured = (postIndex % 3 == 0),

						Description = postDescription,

						ImageUrl =
							$"/images/post_images/pic_{postIndex % 8 + 1}.jpg",
					};

				//if(postIndex % 5 == 0)
				//{
				//	post.Delete();
				//}

				await DatabaseContext.AddAsync(entity: post);
			}
		}

		await DatabaseContext.SaveChangesAsync();
	}

	private async System.Threading.Tasks.Task CreateEnglishPostCategoriesAsync()
	{
		var englishCulture =
			await
			DatabaseContext.Cultures
			.Where(current => current.CultureName.ToLower() == "en-us")
			.FirstOrDefaultAsync();

		if (englishCulture == null)
		{
			return;
		}

		var dariushUser =
			await
			DatabaseContext.Users
			.Where(current => current.EmailAddress.ToLower() == "DariushT@GMail.com")
			.FirstOrDefaultAsync();

		if (dariushUser == null)
		{
			return;
		}

		var postTitleTemplate = "Title";
		var postDescriptionTemplate = "Description";
		var postCategoryNameTemplate = "Category";

		for (var postCategoryIndex = 1; postCategoryIndex <= 10; postCategoryIndex++)
		{
			var postCategoryIndexString =
				postCategoryIndex
				.ToString().PadLeft(totalWidth: 2, paddingChar: '0')
				.ConvertDigitsToUnicode();

			var postCategoryName =
				$"{postCategoryNameTemplate} {postCategoryIndexString}";

			var postCategory =
				new Domain.Features.Cms.PostCategory
				(cultureId: englishCulture.Id, name: postCategoryName)
				{
					IsTestData = true,
					IsActive = (postCategoryIndex % 2 == 0),
					DisplayInHomePage = (postCategoryIndex % 3 == 0),
				};

			await DatabaseContext.AddAsync(entity: postCategory);

			for (var postIndex = 1; postIndex <= 10; postIndex++)
			{
				var postIndexString =
					postIndex
					.ToString().PadLeft(totalWidth: 2, paddingChar: '0')
					.ConvertDigitsToUnicode();

				var postTitle =
					$"{postTitleTemplate} {postIndexString}";

				var postDescription =
					$"{postDescriptionTemplate} {postIndexString}";

				var post =
					new Domain.Features.Cms.Post
					(cultureId: englishCulture.Id, userId: dariushUser.Id,
					categoryId: postCategory.Id, title: postTitle)
					{
						IsTestData = true,

						IsActive = (postIndex % 2 == 0),

						IsDraft = false,
						//IsDraft = (postIndex % 3 == 0),

						IsFeatured = (postIndex % 3 == 0),

						Description = postDescription,

						ImageUrl =
							$"/images/post_images/pic_{postIndex % 8 + 1}.jpg",
					};

				//if (postIndex % 5 == 0)
				//{
				//	post.Delete();
				//}

				await DatabaseContext.AddAsync(entity: post);
			}
		}

		await DatabaseContext.SaveChangesAsync();
	}
}
