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
	public Microsoft.EntityFrameworkCore.DbSet<Domain.Features.Cms.MenuItem> MenuItems { get; set; }
	public Microsoft.EntityFrameworkCore.DbSet<Domain.Features.Cms.PostComment> PostComments { get; set; }
	public Microsoft.EntityFrameworkCore.DbSet<Domain.Features.Cms.PostCategory> PostCategories { get; set; }

	#endregion /CMS Feature

	#region Common Feature

	public Microsoft.EntityFrameworkCore.DbSet<Domain.Features.Common.Culture> Cultures { get; set; }

	#endregion /Common Feature

	#region Identity Feature

	public Microsoft.EntityFrameworkCore.DbSet<Domain.Features.Identity.Role> Roles { get; set; }
	public Microsoft.EntityFrameworkCore.DbSet<Domain.Features.Identity.User> Users { get; set; }
	public Microsoft.EntityFrameworkCore.DbSet<Domain.Features.Identity.LoginLog> LoginLogs { get; set; }
	public Microsoft.EntityFrameworkCore.DbSet<Domain.Features.Identity.UserProfile> UserProfiles { get; set; }

	#endregion /Identity Feature

	#endregion /Properties

	#region Methods

	#region OnModelCreating()
	protected override void OnModelCreating
		(Microsoft.EntityFrameworkCore.ModelBuilder modelBuilder)
	{
		modelBuilder.ApplyConfigurationsFromAssembly
			(assembly: typeof(DatabaseContext).Assembly);

		modelBuilder.Seed();
	}
	#endregion /OnModelCreating()

	#endregion /Methods
}
