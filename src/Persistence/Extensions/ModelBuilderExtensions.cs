using static System.Net.WebRequestMethods;

namespace Persistence.Extensions;

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
		// **************************************************

		// **************************************************
		var culturePersian =
			new Domain.Features.Common.Culture
			(lcid: Domain.Features.Common.Enums.CultureEnum.Persian,
			cultureName: persianCultureInfo.Name,
			nativeName: persianCultureInfo.NativeName)
			{
				IsActive = true,
				Ordering = 10_000,
				Description = null,
			};

		modelBuilder.Entity<Domain.Features
			.Common.Culture>().HasData(data: culturePersian);
		// **************************************************

		// **************************************************
		var englishCultureInfo =
			Domain.Features.Common.CultureEnumHelper.GetByLcid
			(lcid: Domain.Features.Common.Enums.CultureEnum.English);
		// **************************************************

		// **************************************************
		var cultureEnglish =
			new Domain.Features.Common.Culture
			(lcid: Domain.Features.Common.Enums.CultureEnum.English,
			cultureName: englishCultureInfo.Name,
			nativeName: englishCultureInfo.NativeName)
			{
				IsActive = false,
				Ordering = 10_100,
				Description = null,
			};

		modelBuilder.Entity<Domain.Features
			.Common.Culture>().HasData(data: cultureEnglish);
		// **************************************************
		// *** /Cultures ************************************
		// **************************************************

		// **************************************************
		// *** Genders **************************************
		// **************************************************

		// **************************************************
		Domain.Features.Identity.LocalizedGender localizedGender;
		// **************************************************

		// **************************************************
		var genderUnspecified =
			new Domain.Features.Identity.Gender
			(code: Domain.Features.Identity.Enums.GenderEnum.Unspecified)
			{
				IsActive = true,
				Ordering = 10_000,
			};

		modelBuilder.Entity<Domain.Features
			.Identity.Gender>().HasData(data: genderUnspecified);

		localizedGender =
			new Domain.Features.Identity.LocalizedGender
			(cultureId: culturePersian.Id, genderId: genderUnspecified.Id, title: "مشخص نشده")
			{
				Prefix = null,
				Description = null,
			};

		modelBuilder.Entity<Domain.Features
			.Identity.LocalizedGender>().HasData(data: localizedGender);

		localizedGender =
			new Domain.Features.Identity.LocalizedGender
			(cultureId: cultureEnglish.Id, genderId: genderUnspecified.Id, title: "Unspecified")
			{
				Prefix = null,
				Description = null,
			};

		modelBuilder.Entity<Domain.Features
			.Identity.LocalizedGender>().HasData(data: localizedGender);
		// **************************************************

		// **************************************************
		var genderMale =
			new Domain.Features.Identity.Gender
			(code: Domain.Features.Identity.Enums.GenderEnum.Male)
			{
				IsActive = true,
				Ordering = 10_000,
			};

		modelBuilder.Entity<Domain.Features
			.Identity.Gender>().HasData(data: genderMale);

		localizedGender =
			new Domain.Features.Identity.LocalizedGender
			(cultureId: culturePersian.Id, genderId: genderMale.Id, title: "آقا")
			{
				Prefix = "آقای",
				Description = null,
			};

		modelBuilder.Entity<Domain.Features
			.Identity.LocalizedGender>().HasData(data: localizedGender);

		localizedGender =
			new Domain.Features.Identity.LocalizedGender
			(cultureId: cultureEnglish.Id, genderId: genderMale.Id, title: "Male")
			{
				Prefix = "Mr.",
				Description = null,
			};

		modelBuilder.Entity<Domain.Features
			.Identity.LocalizedGender>().HasData(data: localizedGender);
		// **************************************************

		// **************************************************
		var genderFemale =
			new Domain.Features.Identity.Gender
			(code: Domain.Features.Identity.Enums.GenderEnum.Female)
			{
				IsActive = true,
				Ordering = 10_000,
			};

		modelBuilder.Entity<Domain.Features
			.Identity.Gender>().HasData(data: genderFemale);

		localizedGender =
			new Domain.Features.Identity.LocalizedGender
			(cultureId: culturePersian.Id, genderId: genderFemale.Id, title: "خانم")
			{
				Prefix = "خانم",
				Description = null,
			};

		modelBuilder.Entity<Domain.Features
			.Identity.LocalizedGender>().HasData(data: localizedGender);

		localizedGender =
			new Domain.Features.Identity.LocalizedGender
			(cultureId: cultureEnglish.Id, genderId: genderFemale.Id, title: "Female")
			{
				Prefix = "Ms.",
				Description = null,
			};

		modelBuilder.Entity<Domain.Features
			.Identity.LocalizedGender>().HasData(data: localizedGender);
		// **************************************************

		// **************************************************
		var genderTrance =
			new Domain.Features.Identity.Gender
			(code: Domain.Features.Identity.Enums.GenderEnum.Trance)
			{
				IsActive = false,
				Ordering = 10_000,
			};

		modelBuilder.Entity<Domain.Features
			.Identity.Gender>().HasData(data: genderTrance);

		localizedGender =
			new Domain.Features.Identity.LocalizedGender
			(cultureId: culturePersian.Id, genderId: genderTrance.Id, title: "ترنس")
			{
				Prefix = null,
				Description = null,
			};

		modelBuilder.Entity<Domain.Features
			.Identity.LocalizedGender>().HasData(data: localizedGender);

		localizedGender =
			new Domain.Features.Identity.LocalizedGender
			(cultureId: cultureEnglish.Id, genderId: genderTrance.Id, title: "Trance")
			{
				Prefix = null,
				Description = null,
			};

		modelBuilder.Entity<Domain.Features
			.Identity.LocalizedGender>().HasData(data: localizedGender);
		// **************************************************
		// *** /Genders *************************************
		// **************************************************

		// **************************************************
		// *** Roles ****************************************
		// **************************************************

		// **************************************************
		Domain.Features.Identity.LocalizedRole localizedRole;
		// **************************************************

		// **************************************************
		var roleSimpleUser =
			new Domain.Features.Identity.Role
			(code: Domain.Features.Identity.Enums.RoleEnum.SimpleUser,
			name: nameof(Domain.Features.Identity.Enums.RoleEnum.SimpleUser))
			{
				IsActive = true,
				Ordering = 10_000,
			};

		modelBuilder.Entity<Domain.Features
			.Identity.Role>().HasData(data: roleSimpleUser);

		localizedRole = new Domain.Features.Identity.LocalizedRole
			(cultureId: culturePersian.Id, roleId: roleSimpleUser.Id, title: "کاربر")
		{
			Description = null,
		};

		modelBuilder.Entity<Domain.Features
			.Identity.LocalizedRole>().HasData(data: localizedRole);

		localizedRole = new Domain.Features.Identity.LocalizedRole
			(cultureId: cultureEnglish.Id, roleId: roleSimpleUser.Id, title: "User")
		{
			Description = null,
		};

		modelBuilder.Entity<Domain.Features
			.Identity.LocalizedRole>().HasData(data: localizedRole);
		// **************************************************

		// **************************************************
		var roleSpecialUser =
			new Domain.Features.Identity.Role
			(code: Domain.Features.Identity.Enums.RoleEnum.SpecialUser,
			name: nameof(Domain.Features.Identity.Enums.RoleEnum.SpecialUser))
			{
				IsActive = true,
				Ordering = 10_100,
			};

		modelBuilder.Entity<Domain.Features
			.Identity.Role>().HasData(data: roleSpecialUser);

		localizedRole = new Domain.Features.Identity.LocalizedRole
			(cultureId: culturePersian.Id, roleId: roleSpecialUser.Id, title: "کاربر ویژه")
		{
			Description = null,
		};

		modelBuilder.Entity<Domain.Features
			.Identity.LocalizedRole>().HasData(data: localizedRole);

		localizedRole = new Domain.Features.Identity.LocalizedRole
			(cultureId: cultureEnglish.Id, roleId: roleSpecialUser.Id, title: "VIP User")
		{
			Description = null,
		};

		modelBuilder.Entity<Domain.Features
			.Identity.LocalizedRole>().HasData(data: localizedRole);
		// **************************************************

		// **************************************************
		var roleSupervisor =
			new Domain.Features.Identity.Role
			(code: Domain.Features.Identity.Enums.RoleEnum.Supervisor,
			name: nameof(Domain.Features.Identity.Enums.RoleEnum.Supervisor))
			{
				IsActive = true,
				Ordering = 10_200,
			};

		modelBuilder.Entity<Domain.Features
			.Identity.Role>().HasData(data: roleSupervisor);

		localizedRole = new Domain.Features.Identity.LocalizedRole
			(cultureId: culturePersian.Id, roleId: roleSupervisor.Id, title: "مسئول پایگاه")
		{
			Description = null,
		};

		modelBuilder.Entity<Domain.Features
			.Identity.LocalizedRole>().HasData(data: localizedRole);

		localizedRole = new Domain.Features.Identity.LocalizedRole
			(cultureId: cultureEnglish.Id, roleId: roleSupervisor.Id, title: "Supervisor")
		{
			Description = null,
		};

		modelBuilder.Entity<Domain.Features
			.Identity.LocalizedRole>().HasData(data: localizedRole);
		// **************************************************

		// **************************************************
		var roleAdministrator =
			new Domain.Features.Identity.Role
			(code: Domain.Features.Identity.Enums.RoleEnum.Administrator,
			name: nameof(Domain.Features.Identity.Enums.RoleEnum.Administrator))
			{
				IsActive = true,
				Ordering = 10_300,
			};

		modelBuilder.Entity<Domain.Features
			.Identity.Role>().HasData(data: roleAdministrator);

		localizedRole = new Domain.Features.Identity.LocalizedRole
			(cultureId: culturePersian.Id, roleId: roleAdministrator.Id, title: "مدیر پایگاه")
		{
			Description = null,
		};

		modelBuilder.Entity<Domain.Features
			.Identity.LocalizedRole>().HasData(data: localizedRole);

		localizedRole = new Domain.Features.Identity.LocalizedRole
			(cultureId: cultureEnglish.Id, roleId: roleAdministrator.Id, title: "Administrator")
		{
			Description = null,
		};

		modelBuilder.Entity<Domain.Features
			.Identity.LocalizedRole>().HasData(data: localizedRole);
		// **************************************************

		// **************************************************
		var roleApplicationOwner =
			new Domain.Features.Identity.Role
			(code: Domain.Features.Identity.Enums.RoleEnum.ApplicationOwner,
			name: nameof(Domain.Features.Identity.Enums.RoleEnum.ApplicationOwner))
			{
				IsActive = true,
				Ordering = 10_400,
			};

		modelBuilder.Entity<Domain.Features
			.Identity.Role>().HasData(data: roleApplicationOwner);

		localizedRole = new Domain.Features.Identity.LocalizedRole
			(cultureId: culturePersian.Id, roleId: roleApplicationOwner.Id, title: "مالک پایگاه")
		{
			Description = null,
		};

		modelBuilder.Entity<Domain.Features
			.Identity.LocalizedRole>().HasData(data: localizedRole);

		localizedRole = new Domain.Features.Identity.LocalizedRole
			(cultureId: cultureEnglish.Id, roleId: roleApplicationOwner.Id, title: "Application Owner")
		{
			Description = null,
		};

		modelBuilder.Entity<Domain.Features
			.Identity.LocalizedRole>().HasData(data: localizedRole);
		// **************************************************

		// **************************************************
		var roleProgrammer =
			new Domain.Features.Identity.Role
			(code: Domain.Features.Identity.Enums.RoleEnum.Programmer,
			name: nameof(Domain.Features.Identity.Enums.RoleEnum.Programmer))
			{
				IsActive = true,
				Ordering = 10_500,
			};

		modelBuilder.Entity<Domain.Features
			.Identity.Role>().HasData(data: roleProgrammer);

		localizedRole = new Domain.Features.Identity.LocalizedRole
			(cultureId: culturePersian.Id, roleId: roleProgrammer.Id, title: "برنامه‌نویس")
		{
			Description = null,
		};

		modelBuilder.Entity<Domain.Features
			.Identity.LocalizedRole>().HasData(data: localizedRole);

		localizedRole = new Domain.Features.Identity.LocalizedRole
			(cultureId: cultureEnglish.Id, roleId: roleProgrammer.Id, title: "Programmer")
		{
			Description = null,
		};

		modelBuilder.Entity<Domain.Features
			.Identity.LocalizedRole>().HasData(data: localizedRole);
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
			new Domain.Features.Identity.User(emailAddress: "DariushT@GMail.com",
			roleId: roleAdministrator.Id, genderId: genderMale.Id)
			{
				Ordering = 1,

				IsActive = true,
				IsVerified = true,
				IsUndeletable = true,
				IsProfilePublic = true,
				IsEmailAddressVerified = true,
				IsVisibleInContactUsPage = true,
				IsCellPhoneNumberVerified = true,

				AdminDescription = null,
				LastLoginDateTime = null,

				Username = "Dariush",
				CellPhoneNumber = "00989121087461",

				ImageUrl = "/images/dariush.png",
				CoverImageUrl = "/images/dariush_cover.png",

				//Password = "1234512345",

				Password =
					Dtat.Security.Hashing.GetSha256(text: "1234512345"),
			};

		modelBuilder.Entity<Domain.Features
			.Identity.User>().HasData(data: user);

		Domain.Features.Identity.LocalizedUser localizedUser;

		localizedUser =
			new Domain.Features.Identity.LocalizedUser(cultureId: culturePersian.Id,
			userId: user.Id, firstName: "داریوش", lastName: "تصدیقی")
			{
				TitleInContactUsPage = "مالک پایگاه",
			};

		modelBuilder.Entity<Domain.Features
			.Identity.LocalizedUser>().HasData(data: localizedUser);

		//localizedUser =
		//	new Domain.Features.Identity.LocalizedUser(cultureId: cultureEnglish.Id,
		//	userId: user.Id, firstName: "Dariush", lastName: "Tasdighi")
		//	{
		//		TitleInContactUsPage = "Site Owner",
		//	};

		//modelBuilder.Entity<Domain.Features
		//	.Identity.LocalizedUser>().HasData(data: localizedUser);
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
		var aboutPagePersian =
			new Domain.Features.Cms.Page
			(cultureId: culturePersian.Id,
			layoutId: defaultLayout.Id, name: "about", title: "درباره ما")
			{
				IsActive = true,

				Body = "<h1>درباره ما</h1>",
			};

		modelBuilder.Entity<Domain.Features
			.Cms.Page>().HasData(data: aboutPagePersian);
		// **************************************************

		// **************************************************
		var aboutPageEnglish =
			new Domain.Features.Cms.Page
			(cultureId: cultureEnglish.Id,
			layoutId: defaultLayout.Id, name: "about", title: "About us")
			{
				IsActive = true,

				Body = "<h1>About us</h1>",
			};

		modelBuilder.Entity<Domain.Features
			.Cms.Page>().HasData(data: aboutPageEnglish);
		// **************************************************

		// **************************************************
		var contactPagePersian =
			new Domain.Features.Cms.Page
			(cultureId: culturePersian.Id,
			layoutId: defaultLayout.Id, name: "contact", title: "تماس با ما")
			{
				IsActive = true,

				Body = "<h1>تماس با ما</h1>",
			};

		modelBuilder.Entity<Domain.Features
			.Cms.Page>().HasData(data: contactPagePersian);
		// **************************************************

		// **************************************************
		var contactPageEnglish =
			new Domain.Features.Cms.Page
			(cultureId: cultureEnglish.Id,
			layoutId: defaultLayout.Id, name: "contact", title: "Contact us")
			{
				IsActive = true,

				Body = "<h1>Contact us</h1>",
			};

		modelBuilder.Entity<Domain.Features
			.Cms.Page>().HasData(data: contactPageEnglish);
		// **************************************************

		// **************************************************
		var helpPagePersian =
			new Domain.Features.Cms.Page
			(cultureId: culturePersian.Id,
			layoutId: defaultLayout.Id, name: "help", title: "راهنما")
			{
				IsActive = true,

				Body = "<h1>راهنمای سامانه</h1>",
			};

		modelBuilder.Entity<Domain.Features
			.Cms.Page>().HasData(data: helpPagePersian);
		// **************************************************

		// **************************************************
		var helpPageEnglish =
			new Domain.Features.Cms.Page
			(cultureId: cultureEnglish.Id,
			layoutId: defaultLayout.Id, name: "help", title: "Help")
			{
				IsActive = true,

				Body = "<h1>Help</h1>",
			};

		modelBuilder.Entity<Domain.Features
			.Cms.Page>().HasData(data: helpPageEnglish);
		// **************************************************
		// *** /Pages ***************************************
		// **************************************************

		// **************************************************
		// *** Menu Items ***********************************
		// **************************************************
		var aboutMenuItemPersian =
			new Domain.Features.Cms.MenuItem
			(cultureId: culturePersian.Id, title: "درباره ما")
			{
				Ordering = 9_000,
				IsVisible = false,
				IsDisabled = false,
				NavigationUrl = "/page/fa-ir/about",
			};

		modelBuilder.Entity<Domain.Features
			.Cms.MenuItem>().HasData(data: aboutMenuItemPersian);
		// **************************************************

		// **************************************************
		var aboutMenuItemEnglish =
			new Domain.Features.Cms.MenuItem
			(cultureId: cultureEnglish.Id, title: "About")
			{
				Ordering = 9_000,
				IsVisible = false,
				IsDisabled = false,
				NavigationUrl = "/page/en-us/about",
			};

		modelBuilder.Entity<Domain.Features
			.Cms.MenuItem>().HasData(data: aboutMenuItemEnglish);
		// **************************************************

		// **************************************************
		var contactMenuItemPersian =
			new Domain.Features.Cms.MenuItem
			(cultureId: culturePersian.Id, title: "تماس با ما")
			{
				Ordering = 9_100,
				IsVisible = false,
				IsDisabled = false,
				NavigationUrl = "/page/fa-ir/contact",
			};

		modelBuilder.Entity<Domain.Features
			.Cms.MenuItem>().HasData(data: contactMenuItemPersian);
		// **************************************************

		// **************************************************
		var contactMenuItemEnglish =
			new Domain.Features.Cms.MenuItem
			(cultureId: cultureEnglish.Id, title: "Contact")
			{
				Ordering = 9_100,
				IsVisible = false,
				IsDisabled = false,
				NavigationUrl = "/page/en-us/about",
			};

		modelBuilder.Entity<Domain.Features
			.Cms.MenuItem>().HasData(data: contactMenuItemEnglish);
		// **************************************************

		// **************************************************
		var helpMenuItemPersian =
			new Domain.Features.Cms.MenuItem
			(cultureId: culturePersian.Id, title: "راهنما")
			{
				Ordering = 9_200,
				IsVisible = false,
				IsDisabled = true,
				NavigationUrl = "/page/fa-ir/help",
			};

		modelBuilder.Entity<Domain.Features
			.Cms.MenuItem>().HasData(data: helpMenuItemPersian);
		// **************************************************

		// **************************************************
		var helpMenuItemEnglish =
			new Domain.Features.Cms.MenuItem
			(cultureId: cultureEnglish.Id, title: "Help")
			{
				Ordering = 9_200,
				IsVisible = false,
				IsDisabled = true,
				NavigationUrl = "/page/en-us/help",
			};

		modelBuilder.Entity<Domain.Features
			.Cms.MenuItem>().HasData(data: helpMenuItemEnglish);
		// **************************************************

		// **************************************************
		var usersMenuItemPersian =
			new Domain.Features.Cms.MenuItem
			(cultureId: culturePersian.Id, title: "کاربران")
			{
				Ordering = 9_300,
				IsVisible = false,
				IsDisabled = false,
				NavigationUrl = "/users/fa-ir/",
			};

		modelBuilder.Entity<Domain.Features
			.Cms.MenuItem>().HasData(data: usersMenuItemPersian);
		// **************************************************

		// **************************************************
		var usersMenuItemEnglish =
			new Domain.Features.Cms.MenuItem
			(cultureId: cultureEnglish.Id, title: "users")
			{
				Ordering = 9_300,
				IsVisible = false,
				IsDisabled = false,
				NavigationUrl = "/users/en-us/",
			};

		modelBuilder.Entity<Domain.Features
			.Cms.MenuItem>().HasData(data: usersMenuItemEnglish);
		// **************************************************

		// **************************************************
		var postTypesMenuItemPersian =
			new Domain.Features.Cms.MenuItem
			(cultureId: culturePersian.Id, title: "دسته‌بندی‌ها")
			{
				Ordering = 9_400,
				IsVisible = false,
				IsDisabled = false,
				NavigationUrl = "/types/fa-ir/",
			};

		modelBuilder.Entity<Domain.Features
			.Cms.MenuItem>().HasData(data: postTypesMenuItemPersian);
		// **************************************************

		// **************************************************
		var postTypesMenuItemEnglish =
			new Domain.Features.Cms.MenuItem
			(cultureId: cultureEnglish.Id, title: "Types")
			{
				Ordering = 9_400,
				IsVisible = false,
				IsDisabled = false,
				NavigationUrl = "/types/en-us/",
			};

		modelBuilder.Entity<Domain.Features
			.Cms.MenuItem>().HasData(data: postTypesMenuItemEnglish);
		// **************************************************

		// **************************************************
		var categoriesMenuItemPersian =
			new Domain.Features.Cms.MenuItem
			(cultureId: culturePersian.Id, title: "طبقه‌بندی‌ها")
			{
				Ordering = 9_500,
				IsVisible = false,
				IsDisabled = false,
				NavigationUrl = "/categories/fa-ir/",
			};

		modelBuilder.Entity<Domain.Features
			.Cms.MenuItem>().HasData(data: categoriesMenuItemPersian);
		// **************************************************

		// **************************************************
		var categoriesMenuItemEnglish =
			new Domain.Features.Cms.MenuItem
			(cultureId: cultureEnglish.Id, title: "Categories")
			{
				Ordering = 9_500,
				IsVisible = false,
				IsDisabled = false,
				NavigationUrl = "/categories/en-us/",
			};

		modelBuilder.Entity<Domain.Features
			.Cms.MenuItem>().HasData(data: categoriesMenuItemEnglish);
		// **************************************************
		// *** /Menu Items **********************************
		// **************************************************

		// **************************************************
		// *** Post Types ***********************************
		// **************************************************
		var newsPostTypePersian = new Domain.Features.Cms.PostType
			(cultureId: culturePersian.Id, name: "News", title: "خبر")
		{
			IsActive = true,

			Ordering = 9_000,
			MaxPostCountInHomePage = 12,
			MaxPostCountInMainPage = 48,

			//ImageUrl = "/images/types/fa-ir/news.png",
			//CoverImageUrl = "/images/types/fa-ir/news_cover.png",
		};

		modelBuilder.Entity<Domain.Features
			.Cms.PostType>().HasData(data: newsPostTypePersian);
		// **************************************************

		// **************************************************
		var newsPostTypeEnglish = new Domain.Features.Cms.PostType
			(cultureId: cultureEnglish.Id, name: "News", title: "News")
		{
			IsActive = true,

			Ordering = 9_000,
			MaxPostCountInHomePage = 12,
			MaxPostCountInMainPage = 48,

			//ImageUrl = "/images/types/en-us/news.png",
			//CoverImageUrl = "/images/types/en-us/news_cover.png",
		};

		modelBuilder.Entity<Domain.Features
			.Cms.PostType>().HasData(data: newsPostTypeEnglish);
		// **************************************************

		// **************************************************
		var articlePostTypePersian = new Domain.Features.Cms.PostType
			(cultureId: culturePersian.Id, name: "Article", title: "مقاله")
		{
			IsActive = true,

			Ordering = 9_100,
			MaxPostCountInHomePage = 12,
			MaxPostCountInMainPage = 48,

			//ImageUrl = "/images/types/fa-ir/article.png",
			//CoverImageUrl = "/images/types/fa-ir/article_cover.png",
		};

		modelBuilder.Entity<Domain.Features
			.Cms.PostType>().HasData(data: articlePostTypePersian);
		// **************************************************

		// **************************************************            
		var articlePostTypeEnglish = new Domain.Features.Cms.PostType
			(cultureId: cultureEnglish.Id, name: "Article", title: "Article")
		{
			IsActive = true,

			Ordering = 9_100,
			MaxPostCountInHomePage = 12,
			MaxPostCountInMainPage = 48,

			//ImageUrl = "/images/types/en-us/article.png",
			//CoverImageUrl = "/images/types/en-us/article_cover.png",
		};

		modelBuilder.Entity<Domain.Features
			.Cms.PostType>().HasData(data: articlePostTypeEnglish);
		// **************************************************
		// *** /Post Types **********************************
		// **************************************************
	}
}
