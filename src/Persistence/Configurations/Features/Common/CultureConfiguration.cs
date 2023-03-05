namespace Persistence.Configurations.Features.Common;

internal sealed class CultureConfiguration : object, Microsoft
	.EntityFrameworkCore.IEntityTypeConfiguration<Domain.Features.Common.Culture>
{
	public CultureConfiguration() : base()
	{
	}

	public void Configure(Microsoft.EntityFrameworkCore.Metadata
		.Builders.EntityTypeBuilder<Domain.Features.Common.Culture> builder)
	{
		// **************************************************
		// **************************************************
		// **************************************************
		builder
			.Property(current => current.CultureName)
			.IsUnicode(unicode: false)
			;

		builder
			.HasIndex(current => new { current.CultureName })
			.IsUnique(unique: true)
			;
		// **************************************************
		// **************************************************
		// **************************************************

		// **************************************************
		// **************************************************
		// **************************************************
		builder
			.HasMany(current => current.Pages)
			.WithOne(other => other.Culture)
			.IsRequired(required: true)
			.HasForeignKey(other => other.CultureId)
			.OnDelete(deleteBehavior:
				Microsoft.EntityFrameworkCore.DeleteBehavior.NoAction)
			;
		// **************************************************

		// **************************************************
		builder
			.HasMany(current => current.Posts)
			.WithOne(other => other.Culture)
			.IsRequired(required: true)
			.HasForeignKey(other => other.CultureId)
			.OnDelete(deleteBehavior:
				Microsoft.EntityFrameworkCore.DeleteBehavior.NoAction)
			;
		// **************************************************

		// **************************************************
		builder
			.HasMany(current => current.Slides)
			.WithOne(other => other.Culture)
			.IsRequired(required: true)
			.HasForeignKey(other => other.CultureId)
			.OnDelete(deleteBehavior:
				Microsoft.EntityFrameworkCore.DeleteBehavior.NoAction)
			;
		// **************************************************

		// **************************************************
		builder
			.HasMany(current => current.MenuItems)
			.WithOne(other => other.Culture)
			.IsRequired(required: true)
			.HasForeignKey(other => other.CultureId)
			.OnDelete(deleteBehavior:
				Microsoft.EntityFrameworkCore.DeleteBehavior.NoAction)
			;
		// **************************************************

		// **************************************************
		builder
			.HasMany(current => current.PostCategories)
			.WithOne(other => other.Culture)
			.IsRequired(required: true)
			.HasForeignKey(other => other.CultureId)
			.OnDelete(deleteBehavior:
				Microsoft.EntityFrameworkCore.DeleteBehavior.NoAction)
			;
		// **************************************************



		// **************************************************
		builder
			.HasMany(current => current.LocalizedGenders)
			.WithOne(other => other.Culture)
			.IsRequired(required: true)
			.HasForeignKey(other => other.CultureId)
			.OnDelete(deleteBehavior:
				Microsoft.EntityFrameworkCore.DeleteBehavior.NoAction)
			;
		// **************************************************

		// **************************************************
		builder
			.HasMany(current => current.LocalizedUsers)
			.WithOne(other => other.Culture)
			.IsRequired(required: true)
			.HasForeignKey(other => other.CultureId)
			.OnDelete(deleteBehavior:
				Microsoft.EntityFrameworkCore.DeleteBehavior.NoAction)
			;
		// **************************************************

		// **************************************************
		builder
			.HasMany(current => current.LocalizedRoles)
			.WithOne(other => other.Culture)
			.IsRequired(required: true)
			.HasForeignKey(other => other.CultureId)
			.OnDelete(deleteBehavior:
				Microsoft.EntityFrameworkCore.DeleteBehavior.NoAction)
			;
		// **************************************************



		// **************************************************
		builder
			.HasMany(current => current.LocalizedMailSettings)
			.WithOne(other => other.Culture)
			.IsRequired(required: true)
			.HasForeignKey(other => other.CultureId)
			.OnDelete(deleteBehavior:
				Microsoft.EntityFrameworkCore.DeleteBehavior.NoAction)
			;
		// **************************************************

		// **************************************************
		builder
			.HasMany(current => current.LocalizedApplicationSettings)
			.WithOne(other => other.Culture)
			.IsRequired(required: true)
			.HasForeignKey(other => other.CultureId)
			.OnDelete(deleteBehavior:
				Microsoft.EntityFrameworkCore.DeleteBehavior.NoAction)
			;
		// **************************************************
		// **************************************************
		// **************************************************
	}
}
