﻿namespace Persistence.Extensions;

public static class ModelBuilderExtensions : object
{
	static ModelBuilderExtensions()
	{
	}

	public static void Seed
		(this Microsoft.EntityFrameworkCore.ModelBuilder modelBuilder)
	{
		// **************************************************
		// *** Cultures *************************************
		// **************************************************
		var persianCultureInfo =
			Domain.Features.Common.CultureEnumHelper.GetByLcid
			(lcid: Domain.Features.Common.Enums.CultureEnum.Persian);

		var persianCulture =
			new Domain.Features.Common.Culture
			(lcid: Domain.Features.Common.Enums.CultureEnum.Persian,
			cultureName: persianCultureInfo.Name,
			nativeName: persianCultureInfo.NativeName)
			{
				//Id,
				//Lcid,
				//Pages,
				//Posts,
				//Slides,
				//NativeName,
				//CultureName,
				//InsertDateTime,
				//UpdateDateTime,

				IsActive = true,
				Ordering = 10_000,
				Description = null,
			};

		modelBuilder.Entity<Domain.Features
			.Common.Culture>().HasData(data: persianCulture);
		// **************************************************

		// **************************************************
		var englishCultureInfo =
			Domain.Features.Common.CultureEnumHelper.GetByLcid
			(lcid: Domain.Features.Common.Enums.CultureEnum.English);

		var englishCulture =
			new Domain.Features.Common.Culture
			(lcid: Domain.Features.Common.Enums.CultureEnum.English,
			cultureName: englishCultureInfo.Name,
			nativeName: englishCultureInfo.NativeName)
			{
				//Id,
				//Lcid,
				//Pages,
				//Posts,
				//Slides,
				//NativeName,
				//CultureName,
				//InsertDateTime,
				//UpdateDateTime,

				IsActive = true,
				Ordering = 10_100,
				Description = null,
			};

		modelBuilder.Entity<Domain.Features
			.Common.Culture>().HasData(data: englishCulture);
		// **************************************************
		// *** /Cultures ************************************
		// **************************************************

		// **************************************************
		// *** Genders **************************************
		// **************************************************
		var persianUnspecified =
			new Domain.Features.Identity.Gender
			(cultureId: persianCulture.Id,
			code: Domain.Features.Identity.Enums.GenderEnum.Unspecified, title: "نامشخص")
			{
				//Id,
				//Code,
				//Title,
				//Culture,
				//CultureId,
				//UserProfiles,
				//InsertDateTime,
				//UpdateDateTime,

				IsActive = true,
				Ordering = 10_000,
				Description = null,
			};

		modelBuilder.Entity<Domain.Features
			.Identity.Gender>().HasData(data: persianUnspecified);

		var persianMale =
			new Domain.Features.Identity.Gender
			(cultureId: persianCulture.Id,
			code: Domain.Features.Identity.Enums.GenderEnum.Male, title: "آقا")
			{
				//Id,
				//Code,
				//Title,
				//Culture,
				//CultureId,
				//UserProfiles,
				//InsertDateTime,
				//UpdateDateTime,

				IsActive = true,
				Ordering = 10_000,
				Description = null,
			};

		modelBuilder.Entity<Domain.Features
			.Identity.Gender>().HasData(data: persianMale);

		var persianFemale =
			new Domain.Features.Identity.Gender
			(cultureId: persianCulture.Id,
			code: Domain.Features.Identity.Enums.GenderEnum.Female, title: "خانم")
			{
				//Id,
				//Code,
				//Title,
				//Culture,
				//CultureId,
				//UserProfiles,
				//InsertDateTime,
				//UpdateDateTime,

				IsActive = true,
				Ordering = 10_000,
				Description = null,
			};

		modelBuilder.Entity<Domain.Features
			.Identity.Gender>().HasData(data: persianFemale);

		var englishUnspecified =
			new Domain.Features.Identity.Gender
			(cultureId: englishCulture.Id,
			code: Domain.Features.Identity.Enums.GenderEnum.Unspecified, title: "Unspecified")
			{
				//Id,
				//Code,
				//Title,
				//Culture,
				//CultureId,
				//UserProfiles,
				//InsertDateTime,
				//UpdateDateTime,

				IsActive = true,
				Ordering = 10_000,
				Description = null,
			};

		modelBuilder.Entity<Domain.Features
			.Identity.Gender>().HasData(data: englishUnspecified);

		var englishMale =
			new Domain.Features.Identity.Gender
			(cultureId: englishCulture.Id,
			code: Domain.Features.Identity.Enums.GenderEnum.Male, title: "Male")
			{
				//Id,
				//Code,
				//Title,
				//Culture,
				//CultureId,
				//UserProfiles,
				//InsertDateTime,
				//UpdateDateTime,

				IsActive = true,
				Ordering = 10_000,
				Description = null,
			};

		modelBuilder.Entity<Domain.Features
			.Identity.Gender>().HasData(data: englishMale);

		var englishFemale =
			new Domain.Features.Identity.Gender
			(cultureId: englishCulture.Id,
			code: Domain.Features.Identity.Enums.GenderEnum.Female, title: "Female")
			{
				//Id,
				//Code,
				//Title,
				//Culture,
				//CultureId,
				//UserProfiles,
				//InsertDateTime,
				//UpdateDateTime,

				IsActive = true,
				Ordering = 10_000,
				Description = null,
			};

		modelBuilder.Entity<Domain.Features
			.Identity.Gender>().HasData(data: englishFemale);
		// **************************************************
		// *** /Genders *************************************
		// **************************************************

		// **************************************************
		// *** Roles ****************************************
		// **************************************************
		var simpleUser =
			new Domain.Features.Identity.Role
			(code: Domain.Features.Identity.Enums.RoleEnum.SimpleUser,
			name: nameof(Domain.Features.Identity.Enums.RoleEnum.SimpleUser),
			title: "کاربر")
			{
				//Id,
				//Code,
				//Name,
				//Title,
				//Users,
				//InsertDateTime,
				//UpdateDateTime,

				IsActive = true,
				Ordering = 10_000,
				Description = null,
			};

		modelBuilder.Entity<Domain.Features
			.Identity.Role>().HasData(data: simpleUser);
		// **************************************************

		// **************************************************
		var specialUser =
			new Domain.Features.Identity.Role
			(code: Domain.Features.Identity.Enums.RoleEnum.SpecialUser,
			name: nameof(Domain.Features.Identity.Enums.RoleEnum.SpecialUser),
			title: "کاربر ویژه")
			{
				//Id,
				//Code,
				//Name,
				//Title,
				//Users,
				//InsertDateTime,
				//UpdateDateTime,

				IsActive = true,
				Ordering = 10_100,
				Description = null,
			};

		modelBuilder.Entity<Domain.Features
			.Identity.Role>().HasData(data: specialUser);
		// **************************************************

		// **************************************************
		var supervisor =
			new Domain.Features.Identity.Role
			(code: Domain.Features.Identity.Enums.RoleEnum.Supervisor,
			name: nameof(Domain.Features.Identity.Enums.RoleEnum.Supervisor),
			title: "مسئول پایگاه")
			{
				//Id,
				//Code,
				//Name,
				//Title,
				//Users,
				//InsertDateTime,
				//UpdateDateTime,

				IsActive = true,
				Ordering = 10_200,
				Description = null,
			};

		modelBuilder.Entity<Domain.Features
			.Identity.Role>().HasData(data: supervisor);
		// **************************************************

		// **************************************************
		var administrator =
			new Domain.Features.Identity.Role
			(code: Domain.Features.Identity.Enums.RoleEnum.Administrator,
			name: nameof(Domain.Features.Identity.Enums.RoleEnum.Administrator),
			title: "مدیر پایگاه")
			{
				//Id,
				//Code,
				//Name,
				//Title,
				//Users,
				//InsertDateTime,
				//UpdateDateTime,

				IsActive = true,
				Ordering = 10_300,
				Description = null,
			};

		modelBuilder.Entity<Domain.Features
			.Identity.Role>().HasData(data: administrator);
		// **************************************************

		// **************************************************
		var applicationOwner =
			new Domain.Features.Identity.Role
			(code: Domain.Features.Identity.Enums.RoleEnum.ApplicationOwner,
			name: nameof(Domain.Features.Identity.Enums.RoleEnum.ApplicationOwner),
			title: "مالک پایگاه")
			{
				//Id,
				//Code,
				//Name,
				//Title,
				//Users,
				//InsertDateTime,
				//UpdateDateTime,

				IsActive = true,
				Ordering = 10_400,
				Description = null,
			};

		modelBuilder.Entity<Domain.Features
			.Identity.Role>().HasData(data: applicationOwner);
		// **************************************************

		// **************************************************
		var programmer =
			new Domain.Features.Identity.Role
			(code: Domain.Features.Identity.Enums.RoleEnum.Programmer,
			name: nameof(Domain.Features.Identity.Enums.RoleEnum.Programmer),
			title: "برنامه‌نویس")
			{
				//Id,
				//Code,
				//Name,
				//Title,
				//Users,
				//InsertDateTime,
				//UpdateDateTime,

				IsActive = true,
				Ordering = 10_500,
				Description = null,
			};

		modelBuilder.Entity<Domain.Features
			.Identity.Role>().HasData(data: programmer);
		// **************************************************
		// *** /Roles ****************************************
		// **************************************************

		// **************************************************
		// *** Users ****************************************
		// **************************************************
		// TODO
		//var user =
		//	new Domain.Features.Identity.User
		//	(emailAddress: "DariushT@GMail.com", roleId: programmer.Id)

		var user =
			new Domain.Features.Identity.User
			(emailAddress: "DariushT@GMail.com", roleId: administrator.Id)
			{
				//Id,
				//Role,
				//RoleId,
				//LoginLogs,
				//CreatedPages,
				//EmailAddress,
				//InsertDateTime,
				//UpdateDateTime,
				//EmailAddressVerificationKey
				//CellPhoneNumberVerificationKey,

				Ordering = 1,

				IsActive = true,
				IsSystemic = true,
				IsUndeletable = true,
				IsProfilePublic = true,
				IsEmailAddressVerified = true,
				IsVisibleInContactUsPage = true,
				IsCellPhoneNumberVerified = true,

				AdminDescription = null,
				LastLoginDateTime = null,

				Username = "Dariush",
				CellPhoneNumber = "00989121087461",

				//Password = "1234512345",

				Password =
					Dtat.Security.Hashing.GetSha256(text: "1234512345"),
			};

		modelBuilder.Entity<Domain.Features
			.Identity.User>().HasData(data: user);

		var persianUserProfile =
			new Domain.Features.Identity.UserProfile
			(cultureId: persianCulture.Id, userId: user.Id,
			genderId: persianMale.Id, firstName: "داریوش", lastName: "تصدیقی")
			{
				TitleInContactUsPage = "مالک پایگاه",
			};

		modelBuilder.Entity<Domain.Features
			.Identity.UserProfile>().HasData(data: persianUserProfile);

		//var englishUserProfile =
		//	new Domain.Features.Identity.UserProfile
		//	(userId: user.Id, cultureId: englishCulture.Id,
		//	firstName: "Dariush", lastName: "Tasdighi")
		//	{
		//		TitleInContactUsPage = "Site Owner",
		//	};

		//modelBuilder.Entity<Domain.Features
		//	.Identity.UserProfile>().HasData(data: englishUserProfile);
		// **************************************************
		// *** /Users ****************************************
		// **************************************************

		// **************************************************
		// *** Layouts **************************************
		// **************************************************
		var defaultLayout =
			new Domain.Features.Cms.Layout
			(name: "_Default", title: "قالب پیش‌فرض")
			{
				//Id,
				//Name,
				//Pages,
				//Title,
				//DisplayName
				//InsertDateTime,
				//UpdateDateTime,

				IsActive = true,
				Ordering = 1_000,
				Description = null,
			};

		modelBuilder.Entity<Domain.Features
			.Cms.Layout>().HasData(data: defaultLayout);
		// **************************************************

		// **************************************************
		var emptyLayout =
			new Domain.Features.Cms.Layout
			(name: "_Empty", title: "قالب خالی")
			{
				//Id,
				//Name,
				//Pages,
				//Title,
				//DisplayName
				//InsertDateTime,
				//UpdateDateTime,

				IsActive = true,
				Ordering = 1_000,
				Description = null,
			};

		modelBuilder.Entity<Domain.Features
			.Cms.Layout>().HasData(data: emptyLayout);
		// **************************************************
		// *** /Layouts *************************************
		// **************************************************

		// **************************************************
		// *** Pages ****************************************
		// **************************************************
		var persianAboutPage =
			new Domain.Features.Cms.Page
			(cultureId: persianCulture.Id,
			layoutId: defaultLayout.Id, name: "about", title: "درباره ما")
			{
				IsActive = true,

				Body = "<h1>درباره ما</h1>",
			};

		modelBuilder.Entity<Domain.Features
			.Cms.Page>().HasData(data: persianAboutPage);

		var englishAboutPage =
			new Domain.Features.Cms.Page
			(cultureId: englishCulture.Id,
			layoutId: defaultLayout.Id, name: "about", title: "About US")
			{
				IsActive = true,

				Body = "<h1>About US</h1>",
			};

		modelBuilder.Entity<Domain.Features
			.Cms.Page>().HasData(data: englishAboutPage);
		// **************************************************

		// **************************************************
		var persianHelpPage =
			new Domain.Features.Cms.Page
			(cultureId: persianCulture.Id,
			layoutId: defaultLayout.Id, name: "help", title: "راهنما")
			{
				IsActive = true,

				Body = "<h1>راهنمای سامانه</h1>",
			};

		modelBuilder.Entity<Domain.Features
			.Cms.Page>().HasData(data: persianHelpPage);

		var englishHelpPage =
			new Domain.Features.Cms.Page
			(cultureId: englishCulture.Id,
			layoutId: defaultLayout.Id, name: "help", title: "Help")
			{
				IsActive = true,

				Body = "<h1>Help</h1>",
			};

		modelBuilder.Entity<Domain.Features
			.Cms.Page>().HasData(data: englishHelpPage);
		// **************************************************
		// *** /Pages ***************************************
		// **************************************************

		// **************************************************
		// *** Slides ***************************************
		// **************************************************
		var slide_01 =
			new Domain.Features.Cms.Slide
			(cultureId: persianCulture.Id,
			title: "Slide 1", imageUrl: "/images/slides/slide_01.jpg")
			{
				//Id,
				//Title,
				//ImageUrl,
				//InsertDateTime,
				//UpdateDateTime,

				IsActive = true,
				OpenUrlInNewWindow = true,

				Interval = 6_000,
				Ordering = 10_006,

				Caption = null,
				Description = null,

				NavigationUrl =
					"http://date2date.ir",

				PublishStartDateTime = null,
				PublishFinishDateTime = null,
			};

		modelBuilder.Entity<Domain.Features
			.Cms.Slide>().HasData(data: slide_01);
		// **************************************************

		// **************************************************
		var slide_02 =
			new Domain.Features.Cms.Slide
			(cultureId: persianCulture.Id,
			title: "Slide 2", imageUrl: "/images/slides/slide_02.jpg")
			{
				//Id,
				//Title,
				//ImageUrl,
				//InsertDateTime,
				//UpdateDateTime,

				IsActive = false,
				OpenUrlInNewWindow = false,

				Interval = 7_000,
				Ordering = 10_007,

				Caption = null,
				Description = null,
				NavigationUrl = null,

				PublishStartDateTime = null,
				PublishFinishDateTime = null,
			};

		modelBuilder.Entity<Domain.Features
			.Cms.Slide>().HasData(data: slide_02);
		// **************************************************

		// **************************************************
		var slide_03 =
			new Domain.Features.Cms.Slide
			(cultureId: englishCulture.Id,
			title: "Slide 3", imageUrl: "/images/slides/slide_03.jpg")
			{
				//Id,
				//Title,
				//ImageUrl,
				//InsertDateTime,
				//UpdateDateTime,

				IsActive = true,
				OpenUrlInNewWindow = false,

				Interval = 2_000,
				Ordering = 10_002,

				Caption = null,
				Description = null,

				NavigationUrl =
					"http://iranianexperts.ir",

				PublishStartDateTime = null,
				PublishFinishDateTime = null,
			};

		modelBuilder.Entity<Domain.Features
			.Cms.Slide>().HasData(data: slide_03);
		// **************************************************

		// **************************************************
		var slide_04 =
			new Domain.Features.Cms.Slide
			(cultureId: englishCulture.Id,
			title: "Slide 4", imageUrl: "/images/slides/slide_04.jpg")
			{
				//Id,
				//Title,
				//ImageUrl,
				//InsertDateTime,
				//UpdateDateTime,

				IsActive = true,
				OpenUrlInNewWindow = false,

				Interval = 4_000,
				Ordering = 10_004,

				Description = null,
				NavigationUrl = null,

				Caption =
					"<h1>فروش ویژه</h1>",

				PublishStartDateTime = null,
				PublishFinishDateTime = null,
			};

		modelBuilder.Entity<Domain.Features
			.Cms.Slide>().HasData(data: slide_04);

		// **************************************************
		// *** /Slides **************************************
		// **************************************************

		// **************************************************
		// *** Menu Items ***********************************
		// **************************************************
		Domain.Features.Cms.MenuItem? persianFirstLevelSubMenu =
			new(title: "زیر منوی اول",
			cultureId: persianCulture.Id)
			{
				Ordering = 5,
				IsVisible = true,
				IsDisabled = true,
				NavigationUrl = "http://date2date.ir",
			};

		modelBuilder.Entity<Domain.Features
			.Cms.MenuItem>().HasData(data: persianFirstLevelSubMenu);

		Domain.Features.Cms.MenuItem? persianFirstLevelMenuItem =
			new(title: "صفحه دوم",
			cultureId: persianCulture.Id)
			{
				Ordering = 1,
				IsVisible = true,
				IsDisabled = true,
				NavigationUrl = "http://iranianexperts.ir",
			};

		modelBuilder.Entity<Domain.Features
			.Cms.MenuItem>().HasData(data: persianFirstLevelMenuItem);

		persianFirstLevelMenuItem =
			new(title: "صفحه سوم",
			cultureId: persianCulture.Id)
			{
				Ordering = 2,
				IsVisible = false,
				NavigationUrl = "http://iranianexperts.ir",
			};

		modelBuilder.Entity<Domain.Features
			.Cms.MenuItem>().HasData(data: persianFirstLevelMenuItem);

		persianFirstLevelMenuItem =
			new(title: "صفحه چهارم",
			cultureId: persianCulture.Id)
			{
				Ordering = 4,
				IsVisible = true,
				NavigationUrl = "http://google.ir",
			};

		modelBuilder.Entity<Domain.Features
			.Cms.MenuItem>().HasData(data: persianFirstLevelMenuItem);

		Domain.Features.Cms.MenuItem? englishFirstLevelMenuItem =
			new(title: "Page 1",
			cultureId: englishCulture.Id)
			{
				Ordering = 5,
				IsVisible = true,
				NavigationUrl = "http://date2date.ir",
			};

		modelBuilder.Entity<Domain.Features
			.Cms.MenuItem>().HasData(data: englishFirstLevelMenuItem);

		Domain.Features.Cms.MenuItem? persianSecondLevelMenuItem =
			new(title: "صفحه اول اول",
			cultureId: persianCulture.Id)
			{
				Ordering = 5,
				IsVisible = true,
				ParentId = persianFirstLevelSubMenu.Id,
				NavigationUrl = "http://nic.ir",
			};

		modelBuilder.Entity<Domain.Features
			.Cms.MenuItem>().HasData(data: persianSecondLevelMenuItem);

		persianSecondLevelMenuItem =
			new(title: " - ", cultureId: persianCulture.Id)
			{
				Ordering = 4,
				IsVisible = true,
				ParentId = persianFirstLevelSubMenu.Id,
			};

		modelBuilder.Entity<Domain.Features
			.Cms.MenuItem>().HasData(data: persianSecondLevelMenuItem);

		persianSecondLevelMenuItem =
			new(title: "صفحه اول سوم", cultureId: persianCulture.Id)
			{
				Ordering = 7,
				IsVisible = true,
				IsDisabled = true,
				ParentId = persianFirstLevelSubMenu.Id,
				NavigationUrl = "http://nic.ir",
			};

		modelBuilder.Entity<Domain.Features
			.Cms.MenuItem>().HasData(data: persianSecondLevelMenuItem);

		persianSecondLevelMenuItem =
			new(title: "صفحه اول دوم", cultureId: persianCulture.Id)
			{
				Ordering = 3,
				IsVisible = true,
				ParentId = persianFirstLevelSubMenu.Id,
				NavigationUrl = "http://datin.ir",
			};

		modelBuilder.Entity<Domain.Features
			.Cms.MenuItem>().HasData(data: persianSecondLevelMenuItem);
		// **************************************************
		// *** /Menu Items **********************************
		// **************************************************
	}
}
