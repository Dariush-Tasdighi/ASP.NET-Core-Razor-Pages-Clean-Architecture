namespace Persistence.Configurations.Features.Cms;

internal sealed class MenuItemConfiguration : object, Microsoft
	.EntityFrameworkCore.IEntityTypeConfiguration<Domain.Features.Cms.MenuItem>
{
	public MenuItemConfiguration() : base()
	{
	}

	public void Configure(Microsoft.EntityFrameworkCore.Metadata
		.Builders.EntityTypeBuilder<Domain.Features.Cms.MenuItem> builder)
	{
		// **************************************************
		// **************************************************
		// **************************************************
		// بهتر است که دستور ذیل را اجرا نکنیم
		//builder
		//	.Property(current => current.NavigationUrl)
		//	.IsUnicode(unicode: false)
		//	;
		// **************************************************

		// **************************************************
		// بهتر است که دستور ذیل را اجرا نکنیم
		//builder
		//	.Property(current => current.NavigationUrl)
		//	.IsUnicode(unicode: true)
		//	;
		// **************************************************
		// **************************************************
		// **************************************************

		// **************************************************
		// **************************************************
		// **************************************************
		builder
			.HasMany(current => current.Children)
			.WithOne(other => other.Parent)
			.IsRequired(required: false)
			.HasForeignKey(other => other.ParentId)
			.OnDelete(deleteBehavior:
				Microsoft.EntityFrameworkCore.DeleteBehavior.NoAction)
			;
		// **************************************************
		// **************************************************
		// **************************************************
	}
}
