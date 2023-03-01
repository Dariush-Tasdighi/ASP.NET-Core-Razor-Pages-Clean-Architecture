using Dtat;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Server.Pages.Test;

public class CreateTestDataModel :
	Infrastructure.BasePageModelWithDatabaseContext
{
	public CreateTestDataModel
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
			DatabaseContext.Slides
			.Where(current => current.IsTestData)
			.Any();

		if (hasAny)
		{
			return;
		}

		hasAny =
			DatabaseContext.MenuItems
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

		await CreatePersianSlidesAsync();
		await CreateEnglishSlidesAsync();

		await CreatePersianMenuItemsAsync();
		await CreateEnglishMenuItemsAsync();

		await CreatePersianPostCategoriesAsync();
		await CreateEnglishPostCategoriesAsync();
	}

	private async System.Threading.Tasks.Task CreatePersianSlidesAsync()
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

		var slideCaptionTemplate = "سلام زندگی";
		var slideTitleTemplate = "عنوان اسلاید";

		for (var slideIndex = 1; slideIndex <= 6; slideIndex++)
		{
			var slideIndexString =
				slideIndex
				.ToString().PadLeft(totalWidth: 2, paddingChar: '0')
				.ConvertDigitsToUnicode();

			var slideTitle =
				$"{slideTitleTemplate} {slideIndexString}";

			var slideCaption =
				$"{slideCaptionTemplate} {slideIndexString}";

			var imageUrl =
				$"/images/slides/slide_{slideIndex}.jpg";

			var slide =
				new Domain.Features.Cms.Slide
				(cultureId: persianCulture.Id, title: slideTitle, imageUrl: imageUrl)
				{
					IsTestData = true,
					IsActive = (slideIndex % 2 == 0),
					Interval = slideIndex * 1000,
					OpenUrlInNewWindow = slideIndex % 4 == 0,
					Caption = (slideIndex % 2 == 0) ? slideCaption : null,
					NavigationUrl = slideIndex % 2 == 0 ? "http://date2date.ir" : null,
				};

			await DatabaseContext.AddAsync(entity: slide);
		}

		await DatabaseContext.SaveChangesAsync();
	}

	private async System.Threading.Tasks.Task CreateEnglishSlidesAsync()
	{
		var persianCulture =
			await
			DatabaseContext.Cultures
			.Where(current => current.CultureName.ToLower() == "en-us")
			.FirstOrDefaultAsync();

		if (persianCulture == null)
		{
			return;
		}

		var slideTitleTemplate = "Slide Tite";
		var slideCaptionTemplate = "Hello, World!";

		for (var slideIndex = 7; slideIndex <= 11; slideIndex++)
		{
			var slideIndexString =
				slideIndex
				.ToString().PadLeft(totalWidth: 2, paddingChar: '0')
				;

			var slideTitle =
				$"{slideTitleTemplate} {slideIndexString}";

			var slideCaption =
				$"{slideCaptionTemplate} {slideIndexString}";

			var imageUrl =
				$"/images/slides/slide_{slideIndex}.jpg";

			var slide =
				new Domain.Features.Cms.Slide
				(cultureId: persianCulture.Id, title: slideTitle, imageUrl: imageUrl)
				{
					IsTestData = true,
					IsActive = (slideIndex % 2 == 0),
					Interval = slideIndex * 1000,
					OpenUrlInNewWindow = slideIndex % 4 == 0,
					Caption = (slideIndex % 2 == 0) ? slideCaption : null,
					NavigationUrl = slideIndex % 2 == 0 ? "http://date2date.ir" : null,
				};

			await DatabaseContext.AddAsync(entity: slide);
		}

		await DatabaseContext.SaveChangesAsync();
	}

	private async System.Threading.Tasks.Task CreatePersianMenuItemsAsync()
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

		var menuItemTitleTemplate = "آیتم منو";
		var subMenuItemTitleTemplate = "آیتم زیر منو";

		for (var menuItemIndex = 1; menuItemIndex <= 9; menuItemIndex++)
		{
			var menuItemIndexString =
				menuItemIndex
				.ToString().PadLeft(totalWidth: 2, paddingChar: '0')
				.ConvertDigitsToUnicode()
				;

			var menuItemTitle =
				$"{menuItemTitleTemplate} {menuItemIndexString}";

			var menuItem =
				new Domain.Features.Cms.MenuItem
				(cultureId: persianCulture.Id, title: menuItemTitle)
				{
					IsTestData = true,
					IsDisabled = (menuItemIndex == 1 || menuItemIndex == 4 || menuItemIndex == 8),
					NavigationUrl = (menuItemIndex == 1 || menuItemIndex == 3 || menuItemIndex == 7 || menuItemIndex == 9) ? null : "http://date2date.ir",
					IsVisible = (menuItemIndex == 1 || menuItemIndex == 2 || menuItemIndex == 4 || menuItemIndex == 5 || menuItemIndex == 7 || menuItemIndex == 8 || menuItemIndex == 9),
				};

			await DatabaseContext.AddAsync(entity: menuItem);

			if (string.IsNullOrWhiteSpace(value: menuItem.NavigationUrl) == false)
			{
				continue;
			}

			for (var subMenuItemIndex = 1; subMenuItemIndex <= 5; subMenuItemIndex++)
			{
				var subMenuItemIndexString =
					subMenuItemIndex
					.ToString().PadLeft(totalWidth: 2, paddingChar: '0')
					.ConvertDigitsToUnicode()
					;

				var subMenuItemTitle =
					$"{subMenuItemTitleTemplate} {menuItemIndexString} {subMenuItemIndexString}";

				var subMenuItem =
					new Domain.Features.Cms.MenuItem
					(cultureId: persianCulture.Id, title: subMenuItemTitle)
					{
						IsTestData = true,
						ParentId = menuItem.Id,
						IsVisible = (subMenuItemIndex % 3 != 0),
						NavigationUrl = (subMenuItemIndex % 2 == 0) ? "http://date2date.ir" : null,
						IsDisabled = (subMenuItemIndex == 0 || subMenuItemIndex == 7),
					};

				await DatabaseContext.AddAsync(entity: subMenuItem);
			}
		}

		await DatabaseContext.SaveChangesAsync();
	}

	private async System.Threading.Tasks.Task CreateEnglishMenuItemsAsync()
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

		var menuItemTitleTemplate = "Menu Item";
		var subMenuItemTitleTemplate = "Sub Menu Item";

		for (var menuItemIndex = 1; menuItemIndex <= 9; menuItemIndex++)
		{
			var menuItemIndexString =
				menuItemIndex
				.ToString().PadLeft(totalWidth: 2, paddingChar: '0')
				;

			var menuItemTitle =
				$"{menuItemTitleTemplate} {menuItemIndexString}";

			var menuItem =
				new Domain.Features.Cms.MenuItem
				(cultureId: englishCulture.Id, title: menuItemTitle)
				{
					IsTestData = true,
					IsDisabled = (menuItemIndex == 1 || menuItemIndex == 4 || menuItemIndex == 8),
					NavigationUrl = (menuItemIndex == 1 || menuItemIndex == 3 || menuItemIndex == 7 || menuItemIndex == 9) ? null : "http://date2date.ir",
					IsVisible = (menuItemIndex == 1 || menuItemIndex == 2 || menuItemIndex == 4 || menuItemIndex == 5 || menuItemIndex == 7 || menuItemIndex == 8 || menuItemIndex == 9),
				};

			await DatabaseContext.AddAsync(entity: menuItem);

			if (string.IsNullOrWhiteSpace(value: menuItem.NavigationUrl) == false)
			{
				continue;
			}

			for (var subMenuItemIndex = 1; subMenuItemIndex <= 5; subMenuItemIndex++)
			{
				var subMenuItemIndexString =
					subMenuItemIndex
					.ToString().PadLeft(totalWidth: 2, paddingChar: '0')
					;

				var subMenuItemTitle =
					$"{subMenuItemTitleTemplate} {menuItemIndexString} {subMenuItemIndexString}";

				var subMenuItem =
					new Domain.Features.Cms.MenuItem
					(cultureId: englishCulture.Id, title: subMenuItemTitle)
					{
						IsTestData = true,
						ParentId = menuItem.Id,
						IsVisible = (subMenuItemIndex % 3 != 0),
						NavigationUrl = (subMenuItemIndex % 2 == 0) ? "http://date2date.ir" : null,
						IsDisabled = (subMenuItemIndex == 0 || subMenuItemIndex == 7),
					};

				await DatabaseContext.AddAsync(entity: subMenuItem);
			}
		}

		await DatabaseContext.SaveChangesAsync();
	}

	private async System.Threading.Tasks.Task CreatePersianPostCategoriesAsync()
	{
		var maxPostCategories = 12;
		var maxPostsPerPostCategory = 24;

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
			.Where(current => current.EmailAddress.ToLower() == "DariushT@GMail.com".ToLower())
			.FirstOrDefaultAsync();

		if (dariushUser == null)
		{
			return;
		}

		var postTitleTemplate = "عنوان مطلب";
		var postDescriptionTemplate = "توضیحات مطلب";

		var postCategoryNameTemplate = "Category";
		var postCategoryTitleTemplate = "طبقه‌بندی مطلب";

		for (var postCategoryIndex = 1; postCategoryIndex <= maxPostCategories; postCategoryIndex++)
		{
			var postCategoryIndexString =
				postCategoryIndex
				.ToString().PadLeft(totalWidth: 2, paddingChar: '0')
				.ConvertDigitsToUnicode();

			var postCategoryName =
				$"{postCategoryNameTemplate}_{postCategoryIndex}";

			var postCategoryTitle =
				$"{postCategoryTitleTemplate} {postCategoryIndexString}";

			var postCategory =
				new Domain.Features.Cms.PostCategory
				(cultureId: persianCulture.Id,
				name: postCategoryName, title: postCategoryTitle)
				{
					IsTestData = true,
					IsActive = (postCategoryIndex % 2 == 0),
					DisplayInHomePage = (postCategoryIndex % 3 == 0),
				};

			await DatabaseContext.AddAsync(entity: postCategory);

			for (var postIndex = 1; postIndex <= maxPostsPerPostCategory; postIndex++)
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
		var maxPostCategories = 12;
		var maxPostsPerPostCategory = 24;

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
			.Where(current => current.EmailAddress.ToLower() == "DariushT@GMail.com".ToLower())
			.FirstOrDefaultAsync();

		if (dariushUser == null)
		{
			return;
		}

		var postTitleTemplate = "Title";
		var postDescriptionTemplate = "Description";

		var postCategoryNameTemplate = "Category";
		var postCategoryTitleTemplate = "Category";

		for (var postCategoryIndex = 1; postCategoryIndex <= maxPostCategories; postCategoryIndex++)
		{
			var postCategoryIndexString =
				postCategoryIndex
				.ToString().PadLeft(totalWidth: 2, paddingChar: '0')
				;

			var postCategoryName =
				$"{postCategoryNameTemplate}_{postCategoryIndexString}";

			var postCategoryTitle =
				$"{postCategoryTitleTemplate} {postCategoryIndexString}";

			var postCategory =
				new Domain.Features.Cms.PostCategory
				(cultureId: englishCulture.Id,
				name: postCategoryName, title: postCategoryTitle)
				{
					IsTestData = true,
					IsActive = (postCategoryIndex % 2 == 0),
					DisplayInHomePage = (postCategoryIndex % 3 == 0),
				};

			await DatabaseContext.AddAsync(entity: postCategory);

			for (var postIndex = 1; postIndex <= maxPostsPerPostCategory; postIndex++)
			{
				var postIndexString =
					postIndex
					.ToString().PadLeft(totalWidth: 2, paddingChar: '0')
					;

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
