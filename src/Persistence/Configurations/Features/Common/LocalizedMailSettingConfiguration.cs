namespace Persistence.Configurations.Features.Common;

internal sealed class LocalizedMailSettingConfiguration : object, Microsoft
	.EntityFrameworkCore.IEntityTypeConfiguration<Domain.Features.Common.LocalizedMailSetting>
{
	public LocalizedMailSettingConfiguration() : base()
	{
	}

	public void Configure(Microsoft.EntityFrameworkCore.Metadata
		.Builders.EntityTypeBuilder<Domain.Features.Common.LocalizedMailSetting> builder)
	{
		// **************************************************
		// **************************************************
		// **************************************************
		// دستور ذیل بسیار مهم می‌باشد
		// دستور ذیل باعث می‌شود که اگر مثلا دو زبان داریم
		// هیچ‌گاه به اشتباه بیش از دو رکورد ایجاد نگردد
		builder
			.HasIndex(current => new { current.CultureId })
			.IsUnique(unique: true)
			;
		// **************************************************
		// **************************************************
		// **************************************************
	}
}
