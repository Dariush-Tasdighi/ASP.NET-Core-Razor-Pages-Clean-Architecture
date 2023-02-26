namespace Persistence.Configurations.Features.Idenity;

internal sealed class UserLoginConfiguration : object, Microsoft
	.EntityFrameworkCore.IEntityTypeConfiguration<Domain.Features.Identity.LoginLog>
{
	public UserLoginConfiguration() : base()
	{
	}

	public void Configure(Microsoft.EntityFrameworkCore.Metadata
		.Builders.EntityTypeBuilder<Domain.Features.Identity.LoginLog> builder)
	{
		// **************************************************
		// **************************************************
		// **************************************************
		builder
			.Property(current => current.UserIP)
			.IsUnicode(unicode: false)
			;

		builder
			.HasIndex(current => new { current.UserIP })
			.IsUnique(unique: false)
			;
		// **************************************************
		// **************************************************
		// **************************************************
	}
}
