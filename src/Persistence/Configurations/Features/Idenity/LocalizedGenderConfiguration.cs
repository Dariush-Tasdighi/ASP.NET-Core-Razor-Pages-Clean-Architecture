namespace Persistence.Configurations.Features.Idenity;

internal sealed class LocalizedGenderConfiguration : object, Microsoft
	.EntityFrameworkCore.IEntityTypeConfiguration<Domain.Features.Identity.LocalizedGender>
{
	public LocalizedGenderConfiguration() : base()
	{
	}

	public void Configure(Microsoft.EntityFrameworkCore.Metadata
		.Builders.EntityTypeBuilder<Domain.Features.Identity.LocalizedGender> builder)
	{
		// **************************************************
		// **************************************************
		// **************************************************
		builder
			.HasIndex(current => new { current.CultureId, current.Title })
			.IsUnique(unique: true)
			;
		// **************************************************

		// **************************************************
		builder
			.HasIndex(current => new { current.CultureId, current.GenderId })
			.IsUnique(unique: true)
			;
		// **************************************************
		// **************************************************
		// **************************************************
	}
}
