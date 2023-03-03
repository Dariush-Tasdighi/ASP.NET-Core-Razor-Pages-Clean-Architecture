namespace Persistence.Configurations.Features.Idenity;

internal sealed class LocalizedUserConfiguration : object, Microsoft
	.EntityFrameworkCore.IEntityTypeConfiguration<Domain.Features.Identity.LocalizedUser>
{
	public LocalizedUserConfiguration() : base()
	{
	}

	public void Configure(Microsoft.EntityFrameworkCore.Metadata
		.Builders.EntityTypeBuilder<Domain.Features.Identity.LocalizedUser> builder)
	{
		// **************************************************
		// **************************************************
		// **************************************************
		builder
			.HasIndex(current => new { current.CultureId, current.UserId })
			.IsUnique(unique: true)
			;
		// **************************************************
		// **************************************************
		// **************************************************
	}
}
