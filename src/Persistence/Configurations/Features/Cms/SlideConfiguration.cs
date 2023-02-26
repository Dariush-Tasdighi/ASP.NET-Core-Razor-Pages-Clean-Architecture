namespace Persistence.Configurations.Features.Cms;

internal sealed class SlideConfiguration : object, Microsoft
	.EntityFrameworkCore.IEntityTypeConfiguration<Domain.Features.Cms.Slide>
{
	public SlideConfiguration() : base()
	{
	}

	public void Configure(Microsoft.EntityFrameworkCore.Metadata
		.Builders.EntityTypeBuilder<Domain.Features.Cms.Slide> builder)
	{
		// **************************************************
		// **************************************************
		// **************************************************
		// بهتر است که دستور ذیل را اجرا نکنیم
		//builder
		//	.Property(current => current.ImageUrl)
		//	.IsUnicode(unicode: true)
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
	}
}
