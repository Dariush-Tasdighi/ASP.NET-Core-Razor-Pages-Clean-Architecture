namespace Persistence.Configurations.Features.Common;

internal sealed class ApplicationSettingConfiguration : object, Microsoft
	.EntityFrameworkCore.IEntityTypeConfiguration<Domain.Features.Common.ApplicationSetting>
{
	public ApplicationSettingConfiguration() : base()
	{
	}

	public void Configure(Microsoft.EntityFrameworkCore.Metadata
		.Builders.EntityTypeBuilder<Domain.Features.Common.ApplicationSetting> builder)
	{
		// **************************************************
		// **************************************************
		// **************************************************
		// دستور ذیل بسیار مهم می‌باشد
		// دستور ذیل باعث می‌شود که به اشتباه بیش
		// از یک رکورد در بانک اطلاعاتی ایجاد نگردد
		builder
			.HasIndex(current => new { current.Id })
			.IsUnique(unique: true)
			;
		// **************************************************
		// **************************************************
		// **************************************************
	}
}
