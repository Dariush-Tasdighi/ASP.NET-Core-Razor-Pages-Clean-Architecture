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
		var unspecifiedPersian =
			new Domain.Features.Identity.Gender
			(cultureId: culturePersian.Id,
			code: Domain.Features.Identity.Enums.GenderEnum.Unspecified, title: "نامشخص")
			{
				Prefix = null,
				IsActive = false,
				Ordering = 10_000,
				Description = null,
			};

		modelBuilder.Entity<Domain.Features
			.Identity.Gender>().HasData(data: unspecifiedPersian);
		// **************************************************

		// **************************************************
		var malePersian =
			new Domain.Features.Identity.Gender
			(cultureId: culturePersian.Id,
			code: Domain.Features.Identity.Enums.GenderEnum.Male, title: "آقا")
			{
				Prefix = "آقای",
				IsActive = true,
				Ordering = 10_000,
				Description = null,
			};

		modelBuilder.Entity<Domain.Features
			.Identity.Gender>().HasData(data: malePersian);
		// **************************************************

		// **************************************************
		var femalePersian =
			new Domain.Features.Identity.Gender
			(cultureId: culturePersian.Id,
			code: Domain.Features.Identity.Enums.GenderEnum.Female, title: "خانم")
			{
				Prefix = "خانم",
				IsActive = true,
				Ordering = 10_000,
				Description = null,
			};

		modelBuilder.Entity<Domain.Features
			.Identity.Gender>().HasData(data: femalePersian);
		// **************************************************

		// **************************************************
		var trancePersian =
			new Domain.Features.Identity.Gender
			(cultureId: culturePersian.Id,
			code: Domain.Features.Identity.Enums.GenderEnum.Trance, title: "ترنس")
			{
				Prefix = null,
				IsActive = false,
				Ordering = 10_000,
				Description = null,
			};

		modelBuilder.Entity<Domain.Features
			.Identity.Gender>().HasData(data: trancePersian);
		// **************************************************

		// **************************************************
		var unspecifiedEnglish =
			new Domain.Features.Identity.Gender
			(cultureId: cultureEnglish.Id,
			code: Domain.Features.Identity.Enums.GenderEnum.Unspecified, title: "Unspecified")
			{
				Prefix = null,
				IsActive = true,
				Ordering = 10_000,
				Description = null,
			};

		modelBuilder.Entity<Domain.Features
			.Identity.Gender>().HasData(data: unspecifiedEnglish);
		// **************************************************

		// **************************************************
		var maleEnglish =
			new Domain.Features.Identity.Gender
			(cultureId: cultureEnglish.Id,
			code: Domain.Features.Identity.Enums.GenderEnum.Male, title: "Male")
			{
				Prefix = "Mr.",
				IsActive = true,
				Ordering = 10_000,
				Description = null,
			};

		modelBuilder.Entity<Domain.Features
			.Identity.Gender>().HasData(data: maleEnglish);
		// **************************************************

		// **************************************************
		var femaleEnglish =
			new Domain.Features.Identity.Gender
			(cultureId: cultureEnglish.Id,
			code: Domain.Features.Identity.Enums.GenderEnum.Female, title: "Female")
			{
				Prefix = "Ms.",
				IsActive = true,
				Ordering = 10_000,
				Description = null,
			};

		modelBuilder.Entity<Domain.Features
			.Identity.Gender>().HasData(data: femaleEnglish);
		// **************************************************

		// **************************************************
		var tranceEnglish =
			new Domain.Features.Identity.Gender
			(cultureId: cultureEnglish.Id,
			code: Domain.Features.Identity.Enums.GenderEnum.Trance, title: "Trance")
			{
				Prefix = null,
				IsActive = true,
				Ordering = 10_000,
				Description = null,
			};

		modelBuilder.Entity<Domain.Features
			.Identity.Gender>().HasData(data: tranceEnglish);
		// **************************************************
		// *** /Genders *************************************
		// **************************************************

		// **************************************************
		// *** Roles ****************************************
		// **************************************************
		var simpleUserPersian =
			new Domain.Features.Identity.Role
			(cultureId: culturePersian.Id,
			code: Domain.Features.Identity.Enums.RoleEnum.SimpleUser,
			name: nameof(Domain.Features.Identity.Enums.RoleEnum.SimpleUser),
			title: "کاربر")
			{
				IsActive = true,
				Ordering = 10_000,
				Description = null,
			};

		modelBuilder.Entity<Domain.Features
			.Identity.Role>().HasData(data: simpleUserPersian);
		// **************************************************

		// **************************************************
		var specialUserPersian =
			new Domain.Features.Identity.Role
			(cultureId: culturePersian.Id,
			code: Domain.Features.Identity.Enums.RoleEnum.SpecialUser,
			name: nameof(Domain.Features.Identity.Enums.RoleEnum.SpecialUser),
			title: "کاربر ویژه")
			{
				IsActive = true,
				Ordering = 10_100,
				Description = null,
			};

		modelBuilder.Entity<Domain.Features
			.Identity.Role>().HasData(data: specialUserPersian);
		// **************************************************

		// **************************************************
		var supervisorPersian =
			new Domain.Features.Identity.Role
			(cultureId: culturePersian.Id,
			code: Domain.Features.Identity.Enums.RoleEnum.Supervisor,
			name: nameof(Domain.Features.Identity.Enums.RoleEnum.Supervisor),
			title: "مسئول پایگاه")
			{
				IsActive = true,
				Ordering = 10_200,
				Description = null,
			};

		modelBuilder.Entity<Domain.Features
			.Identity.Role>().HasData(data: supervisorPersian);
		// **************************************************

		// **************************************************
		var administratorPersian =
			new Domain.Features.Identity.Role
			(cultureId: culturePersian.Id,
			code: Domain.Features.Identity.Enums.RoleEnum.Administrator,
			name: nameof(Domain.Features.Identity.Enums.RoleEnum.Administrator),
			title: "مدیر پایگاه")
			{
				IsActive = true,
				Ordering = 10_300,
				Description = null,
			};

		modelBuilder.Entity<Domain.Features
			.Identity.Role>().HasData(data: administratorPersian);
		// **************************************************

		// **************************************************
		var applicationOwnerPersian =
			new Domain.Features.Identity.Role
			(cultureId: culturePersian.Id,
			code: Domain.Features.Identity.Enums.RoleEnum.ApplicationOwner,
			name: nameof(Domain.Features.Identity.Enums.RoleEnum.ApplicationOwner),
			title: "مالک پایگاه")
			{
				IsActive = true,
				Ordering = 10_400,
				Description = null,
			};

		modelBuilder.Entity<Domain.Features
			.Identity.Role>().HasData(data: applicationOwnerPersian);
		// **************************************************

		// **************************************************
		var programmerPersian =
			new Domain.Features.Identity.Role
			(cultureId: culturePersian.Id,
			code: Domain.Features.Identity.Enums.RoleEnum.Programmer,
			name: nameof(Domain.Features.Identity.Enums.RoleEnum.Programmer),
			title: "برنامه‌نویس")
			{
				IsActive = true,
				Ordering = 10_500,
				Description = null,
			};

		modelBuilder.Entity<Domain.Features
			.Identity.Role>().HasData(data: programmerPersian);
		// **************************************************

		// **************************************************
		var simpleUserEnglish =
			new Domain.Features.Identity.Role
			(cultureId: cultureEnglish.Id,
			code: Domain.Features.Identity.Enums.RoleEnum.SimpleUser,
			name: nameof(Domain.Features.Identity.Enums.RoleEnum.SimpleUser),
			title: "Simple User")
			{
				IsActive = true,
				Ordering = 10_000,
				Description = null,
			};

		modelBuilder.Entity<Domain.Features
			.Identity.Role>().HasData(data: simpleUserEnglish);
		// **************************************************

		// **************************************************
		var specialUserEnglish =
			new Domain.Features.Identity.Role
			(cultureId: cultureEnglish.Id,
			code: Domain.Features.Identity.Enums.RoleEnum.SpecialUser,
			name: nameof(Domain.Features.Identity.Enums.RoleEnum.SpecialUser),
			title: "VIP User")
			{
				IsActive = true,
				Ordering = 10_100,
				Description = null,
			};

		modelBuilder.Entity<Domain.Features
			.Identity.Role>().HasData(data: specialUserEnglish);
		// **************************************************

		// **************************************************
		var supervisorEnglish =
			new Domain.Features.Identity.Role
			(cultureId: cultureEnglish.Id,
			code: Domain.Features.Identity.Enums.RoleEnum.Supervisor,
			name: nameof(Domain.Features.Identity.Enums.RoleEnum.Supervisor),
			title: "Supervisor")
			{
				IsActive = true,
				Ordering = 10_200,
				Description = null,
			};

		modelBuilder.Entity<Domain.Features
			.Identity.Role>().HasData(data: supervisorEnglish);
		// **************************************************

		// **************************************************
		var administratorEnglish =
			new Domain.Features.Identity.Role
			(cultureId: cultureEnglish.Id,
			code: Domain.Features.Identity.Enums.RoleEnum.Administrator,
			name: nameof(Domain.Features.Identity.Enums.RoleEnum.Administrator),
			title: "Administrator")
			{
				IsActive = true,
				Ordering = 10_300,
				Description = null,
			};

		modelBuilder.Entity<Domain.Features
			.Identity.Role>().HasData(data: administratorEnglish);
		// **************************************************

		// **************************************************
		var applicationOwnerEnglish =
			new Domain.Features.Identity.Role
			(cultureId: cultureEnglish.Id,
			code: Domain.Features.Identity.Enums.RoleEnum.ApplicationOwner,
			name: nameof(Domain.Features.Identity.Enums.RoleEnum.ApplicationOwner),
			title: "Application Owner")
			{
				IsActive = true,
				Ordering = 10_400,
				Description = null,
			};

		modelBuilder.Entity<Domain.Features
			.Identity.Role>().HasData(data: applicationOwnerEnglish);
		// **************************************************

		// **************************************************
		var programmerEnglish =
			new Domain.Features.Identity.Role
			(cultureId: cultureEnglish.Id,
			code: Domain.Features.Identity.Enums.RoleEnum.Programmer,
			name: nameof(Domain.Features.Identity.Enums.RoleEnum.Programmer),
			title: "Programmer")
			{
				IsActive = true,
				Ordering = 10_500,
				Description = null,
			};

		modelBuilder.Entity<Domain.Features
			.Identity.Role>().HasData(data: programmerEnglish);
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
			(emailAddress: "DariushT@GMail.com", roleId: administratorPersian.Id)
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

				//Password = "1234512345",

				Password =
					Dtat.Security.Hashing.GetSha256(text: "1234512345"),
			};

		modelBuilder.Entity<Domain.Features
			.Identity.User>().HasData(data: user);

		var persianUserProfile =
			new Domain.Features.Identity.UserProfile
			(cultureId: culturePersian.Id, userId: user.Id,
			genderId: malePersian.Id, firstName: "داریوش", lastName: "تصدیقی")
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
			layoutId: defaultLayout.Id, name: "about", title: "About US")
			{
				IsActive = true,

				Body = "<h1>About US</h1>",
			};

		modelBuilder.Entity<Domain.Features
			.Cms.Page>().HasData(data: aboutPageEnglish);
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
	}
}
