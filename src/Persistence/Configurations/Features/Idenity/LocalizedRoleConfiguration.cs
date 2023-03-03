namespace Persistence.Configurations.Features.Idenity;

internal sealed class LocalizedRoleConfiguration : object, Microsoft
	.EntityFrameworkCore.IEntityTypeConfiguration<Domain.Features.Identity.LocalizedRole>
{
	public LocalizedRoleConfiguration() : base()
	{
	}

	public void Configure(Microsoft.EntityFrameworkCore.Metadata
		.Builders.EntityTypeBuilder<Domain.Features.Identity.LocalizedRole> builder)
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
			.HasIndex(current => new { current.CultureId, current.RoleId })
			.IsUnique(unique: true)
			;
		// **************************************************
		// **************************************************
		// **************************************************
	}
}
