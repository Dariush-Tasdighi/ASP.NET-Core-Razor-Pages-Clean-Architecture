namespace Persistence.Configurations.Features.Idenity;

internal sealed class GenderConfiguration : object, Microsoft
	.EntityFrameworkCore.IEntityTypeConfiguration<Domain.Features.Identity.Gender>
{
	public GenderConfiguration() : base()
	{
	}

	public void Configure(Microsoft.EntityFrameworkCore.Metadata
		.Builders.EntityTypeBuilder<Domain.Features.Identity.Gender> builder)
	{
		// **************************************************
		// **************************************************
		// **************************************************
		builder
			.HasIndex(current => new { current.Code })
			.IsUnique(unique: true)
			;
		// **************************************************
		// **************************************************
		// **************************************************

		// **************************************************
		// **************************************************
		// **************************************************
		builder
			.HasMany(current => current.Users)
			.WithOne(other => other.Gender)
			.IsRequired(required: true)
			.HasForeignKey(other => other.GenderId)
			.OnDelete(deleteBehavior:
				Microsoft.EntityFrameworkCore.DeleteBehavior.NoAction)
			;
		// **************************************************

		// **************************************************
		builder
			.HasMany(current => current.LocalizedGenders)
			.WithOne(other => other.Gender)
			.IsRequired(required: true)
			.HasForeignKey(other => other.GenderId)
			.OnDelete(deleteBehavior:
				Microsoft.EntityFrameworkCore.DeleteBehavior.NoAction)
			;
		// **************************************************
		// **************************************************
		// **************************************************
	}
}
