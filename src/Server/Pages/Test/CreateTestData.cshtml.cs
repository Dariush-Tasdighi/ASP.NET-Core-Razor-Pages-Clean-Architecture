using Dtat;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Server.Pages.Test;

public class CreateTestDataModel :
	Infrastructure.BasePageModelWithDatabaseContext
{
	#region Constructor
	public CreateTestDataModel
		(Persistence.DatabaseContext databaseContext,
		Microsoft.Extensions.Hosting.IHostEnvironment hostEnvironment,
		Services.ColorService colorService) :
		base(databaseContext: databaseContext)
	{
		ColorService = colorService;
		HostEnvironment = hostEnvironment;
	}
	#endregion /Constructor

	#region Properties

	private Services.ColorService ColorService { get; }

	private Microsoft.Extensions.Hosting.IHostEnvironment HostEnvironment { get; }

	#endregion /Properties

	#region Methods

	private int GetRandomNumber()
	{
		var byteArray =
			System.Guid.NewGuid().ToByteArray();

		var seed = System.BitConverter
			.ToInt32(value: byteArray, startIndex: 0);

		var random = new System.Random(Seed: seed);

		var value = random.Next
			(minValue: 0, ColorService.Colors.Count);

		return value;
	}

	public async System.Threading.Tasks.Task OnGet()
	{
		CreatePredefinedFoldersAsync();

		CreatePostImagesAsync();
		CreateSlideImagesAsync();

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

		var slideCount = 20;
		var slideCaptionTemplate = "سلام زندگی";
		var slideTitleTemplate = "عنوان اسلاید";

		for (var slideIndex = 1; slideIndex <= slideCount; slideIndex++)
		{
			var slideIndexString =
				slideIndex
				.ToString().PadLeft(totalWidth: 2, paddingChar: '0')
				.ConvertDigitsToUnicode();

			var slideTitle =
				$"{slideTitleTemplate} {slideIndexString}";

			var slideCaption =
				$"<h1>{slideCaptionTemplate} {slideIndexString}</h1>";

			var imageUrl =
				$"/images/predefined_slides/slide_{GetRandomNumber()}.png";

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

		var slideCount = 20;
		var slideTitleTemplate = "Slide Tite";
		var slideCaptionTemplate = "Hello, World!";

		for (var slideIndex = 1; slideIndex <= slideCount; slideIndex++)
		{
			var slideIndexString =
				slideIndex
				.ToString().PadLeft(totalWidth: 2, paddingChar: '0')
				;

			var slideTitle =
				$"{slideTitleTemplate} {slideIndexString}";

			var slideCaption =
				$"<h1>{slideCaptionTemplate} {slideIndexString}</h1>";

			var imageUrl =
				$"/images/predefined_slides/slide_{GetRandomNumber()}.png";

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
		var postCategoryTitleTemplate = "عنوان طبقه‌بندی";
		var postCategoryDescriptionTemplate = "توضیحات طبقه‌بندی";

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

			var postCategoryDescription =
				$"{postCategoryDescriptionTemplate} {postCategoryIndexString}";

			var postCategory =
				new Domain.Features.Cms.PostCategory
				(cultureId: persianCulture.Id,
				name: postCategoryName, title: postCategoryTitle)
				{
					IsTestData = true,
					Description = postCategoryDescription,

					IsActive = (postCategoryIndex % 2 == 0),
					DisplayInHomePage = (postCategoryIndex % 3 == 0),

					ImageUrl =
							$"/images/predefined_posts/post_{GetRandomNumber()}.png",

					WideImageUrl =
							$"/images/predefined_slides/slide_{GetRandomNumber()}.png",
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
							$"/images/predefined_posts/post_{GetRandomNumber()}.png",

						WideImageUrl =
							$"/images/predefined_slides/slide_{GetRandomNumber()}.png",
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

		var postTitleTemplate = "Post Title";
		var postDescriptionTemplate = "Post Description";

		var postCategoryNameTemplate = "Category";
		var postCategoryTitleTemplate = "Category Title";
		var postCategoryDescriptionTemplate = "Category Description";

		for (var postCategoryIndex = 1; postCategoryIndex <= maxPostCategories; postCategoryIndex++)
		{
			var postCategoryIndexString =
				postCategoryIndex
				.ToString().PadLeft(totalWidth: 2, paddingChar: '0')
				;

			var postCategoryName =
				$"{postCategoryNameTemplate}_{postCategoryIndex}";

			var postCategoryTitle =
				$"{postCategoryTitleTemplate} {postCategoryIndexString}";

			var postCategoryDescription =
				$"{postCategoryDescriptionTemplate} {postCategoryIndexString}";

			var postCategory =
				new Domain.Features.Cms.PostCategory
				(cultureId: englishCulture.Id,
				name: postCategoryName, title: postCategoryTitle)
				{
					IsTestData = true,
					Description = postCategoryDescription,

					IsActive = (postCategoryIndex % 2 == 0),
					DisplayInHomePage = (postCategoryIndex % 3 == 0),

					ImageUrl =
							$"/images/predefined_posts/post_{GetRandomNumber()}.png",

					WideImageUrl =
							$"/images/predefined_slides/slide_{GetRandomNumber()}.png",
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
							$"/images/predefined_posts/post_{GetRandomNumber()}.png",

						WideImageUrl =
							$"/images/predefined_slides/slide_{GetRandomNumber()}.png",
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

	private void CreatePredefinedFoldersAsync()
	{
		var path =
			$"{HostEnvironment.ContentRootPath}\\wwwroot\\images";

		if (System.IO.Directory.Exists(path: path) == false)
		{
			System.IO.Directory.CreateDirectory(path: path);
		}

		var postsPath =
			$"{path}\\posts";

		if (System.IO.Directory.Exists(path: postsPath) == false)
		{
			System.IO.Directory.CreateDirectory(path: postsPath);
		}

		var slidesPath =
			$"{path}\\slides";

		if (System.IO.Directory.Exists(path: slidesPath) == false)
		{
			System.IO.Directory.CreateDirectory(path: slidesPath);
		}

		var predefinedPostsPath =
			$"{path}\\predefined_posts";

		if (System.IO.Directory.Exists(path: predefinedPostsPath) == false)
		{
			System.IO.Directory.CreateDirectory(path: predefinedPostsPath);
		}

		var predefinedSlidesPath =
			$"{path}\\predefined_slides";

		if (System.IO.Directory.Exists(path: predefinedSlidesPath) == false)
		{
			System.IO.Directory.CreateDirectory(path: predefinedSlidesPath);
		}
	}

	private void CreatePostImagesAsync()
	{
		var width = 915;
		var height = 580;

		var path =
			$"{HostEnvironment.ContentRootPath}\\wwwroot\\images\\predefined_posts";

#pragma warning disable CA1416

		//var format =
		//	System.Drawing.Imaging.PixelFormat.Format32bppArgb;

		var format =
			System.Drawing.Imaging.PixelFormat.Format24bppRgb;

		for (var index = 0; index <= ColorService.Colors.Count - 1; index++)
		{
			var pathName =
				$"{path}\\post_{index}.png";

			if (System.IO.File.Exists(path: pathName))
			{
				continue;
			}

			var bitmap =
				new System.Drawing.Bitmap
				(width: width, height: height, format: format);

			var graphics = System.Drawing
				.Graphics.FromImage(image: bitmap);

			graphics.PageUnit =
				System.Drawing.GraphicsUnit.Pixel;

			graphics.PixelOffsetMode =
				System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;

			graphics.CompositingQuality =
				System.Drawing.Drawing2D.CompositingQuality.HighQuality;

			var backgroundColor =
				ColorService.Colors[index];

			var backgroundBrush = new System
				.Drawing.SolidBrush(color: backgroundColor);

			var rectangle =
				new System.Drawing.Rectangle
				(x: 0, y: 0, width: width, height: height);

			graphics.FillRectangle
				(brush: backgroundBrush, rect: rectangle);

			bitmap.Save(filename: pathName,
				format: System.Drawing.Imaging.ImageFormat.Png);
		}

#pragma warning restore CA1416
	}

	private void CreateSlideImagesAsync()
	{
		var width = 2880;
		var height = 600;

		var path =
			$"{HostEnvironment.ContentRootPath}\\wwwroot\\images\\predefined_slides";

#pragma warning disable CA1416

		//var format =
		//	System.Drawing.Imaging.PixelFormat.Format32bppArgb;

		var format =
			System.Drawing.Imaging.PixelFormat.Format24bppRgb;

		for (var index = 0; index <= ColorService.Colors.Count - 1; index++)
		{
			var pathName =
				$"{path}\\slide_{index}.png";

			if (System.IO.File.Exists(path: pathName))
			{
				continue;
			}

			var bitmap =
				new System.Drawing.Bitmap
				(width: width, height: height, format: format);

			var graphics = System.Drawing
				.Graphics.FromImage(image: bitmap);

			graphics.PageUnit =
				System.Drawing.GraphicsUnit.Pixel;

			graphics.PixelOffsetMode =
				System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;

			graphics.CompositingQuality =
				System.Drawing.Drawing2D.CompositingQuality.HighQuality;

			var backgroundColor =
				ColorService.Colors[index];

			var backgroundBrush = new System
				.Drawing.SolidBrush(color: backgroundColor);

			var rectangle =
				new System.Drawing.Rectangle
				(x: 0, y: 0, width: width, height: height);

			graphics.FillRectangle
				(brush: backgroundBrush, rect: rectangle);

			bitmap.Save(filename: pathName,
				format: System.Drawing.Imaging.ImageFormat.Png);
		}

#pragma warning restore CA1416
	}

	#endregion /Methods
}
