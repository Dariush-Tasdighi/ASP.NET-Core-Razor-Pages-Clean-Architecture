using System.Linq;
using Persistence.Extensions;

namespace Persistence;

public class DatabaseContext :
	Microsoft.EntityFrameworkCore.DbContext
{
	#region Constructor
	public DatabaseContext(Microsoft.EntityFrameworkCore
		.DbContextOptions<DatabaseContext> options) : base(options: options)
	{
		// TODO
		Database.EnsureCreated();
	}
	#endregion /Constructor

	#region Properties

	#region CMS Feature

	public Microsoft.EntityFrameworkCore.DbSet<Domain.Features.Cms.Page> Pages { get; set; }
	public Microsoft.EntityFrameworkCore.DbSet<Domain.Features.Cms.Post> Posts { get; set; }
	public Microsoft.EntityFrameworkCore.DbSet<Domain.Features.Cms.Slide> Slides { get; set; }
	public Microsoft.EntityFrameworkCore.DbSet<Domain.Features.Cms.Layout> Layouts { get; set; }
	public Microsoft.EntityFrameworkCore.DbSet<Domain.Features.Cms.PostType> PostTypes { get; set; }
	public Microsoft.EntityFrameworkCore.DbSet<Domain.Features.Cms.MenuItem> MenuItems { get; set; }
	public Microsoft.EntityFrameworkCore.DbSet<Domain.Features.Cms.PostComment> PostComments { get; set; }
	public Microsoft.EntityFrameworkCore.DbSet<Domain.Features.Cms.PostCategory> PostCategories { get; set; }

	#endregion /CMS Feature

	#region Common Feature

	public Microsoft.EntityFrameworkCore.DbSet<Domain.Features.Common.Culture> Cultures { get; set; }
	public Microsoft.EntityFrameworkCore.DbSet<Domain.Features.Common.ApplicationSetting> ApplicationSettings { get; set; }
	public Microsoft.EntityFrameworkCore.DbSet<Domain.Features.Common.LocalizedMailSetting> LocalizedMailSettings { get; set; }
	public Microsoft.EntityFrameworkCore.DbSet<Domain.Features.Common.LocalizedApplicationSetting> LocalizedApplicationSettings { get; set; }

	#endregion /Common Feature

	#region Identity Feature

	public Microsoft.EntityFrameworkCore.DbSet<Domain.Features.Identity.Role> Roles { get; set; }
	public Microsoft.EntityFrameworkCore.DbSet<Domain.Features.Identity.User> Users { get; set; }
	public Microsoft.EntityFrameworkCore.DbSet<Domain.Features.Identity.Gender> Genders { get; set; }
	public Microsoft.EntityFrameworkCore.DbSet<Domain.Features.Identity.LoginLog> LoginLogs { get; set; }
	public Microsoft.EntityFrameworkCore.DbSet<Domain.Features.Identity.LocalizedUser> LocalizedUsers { get; set; }
	public Microsoft.EntityFrameworkCore.DbSet<Domain.Features.Identity.LocalizedRole> LocalizedRoles { get; set; }
	public Microsoft.EntityFrameworkCore.DbSet<Domain.Features.Identity.LocalizedGender> LocalizedGenders { get; set; }

	#endregion /Identity Feature

	#endregion /Properties

	#region Methods

	#region OnModelCreating()
	protected override void OnModelCreating
		(Microsoft.EntityFrameworkCore.ModelBuilder modelBuilder)
	{
		modelBuilder.ApplyConfigurationsFromAssembly
			(assembly: typeof(DatabaseContext).Assembly);

		if (Database.ProviderName == "Microsoft.EntityFrameworkCore.Sqlite")
		{
			// SQLite does not have proper support for DateTimeOffset via Entity Framework Core, see the limitations
			// here: https://docs.microsoft.com/en-us/ef/core/providers/sqlite/limitations#query-limitations
			// To work around this, when the Sqlite database provider is used, all model properties of type DateTimeOffset
			// use the DateTimeOffsetToBinaryConverter
			// Based on: https://github.com/aspnet/EntityFrameworkCore/issues/10784#issuecomment-415769754
			// This only supports millisecond precision, but should be sufficient for most use cases.
			foreach (var entityType in modelBuilder.Model.GetEntityTypes())
			{
				var properties =
					entityType.ClrType.GetProperties()
					.Where(current =>
						current.PropertyType == typeof(System.DateTimeOffset) ||
						current.PropertyType == typeof(System.DateTimeOffset?));

				foreach (var property in properties)
				{
					modelBuilder
						.Entity(name: entityType.Name)
						.Property(propertyName: property.Name)
						.HasConversion(converter:
							new Microsoft.EntityFrameworkCore
							.Storage.ValueConversion.DateTimeOffsetToBinaryConverter());
				}
			}
		}

		modelBuilder.Seed();
	}
	#endregion /OnModelCreating()

	#endregion /Methods
}
