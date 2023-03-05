using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Services.Features.Common;

public class LocalizedMailSettingService :
	Infrastructure.BaseServiceWithDatabaseContext
{
	public LocalizedMailSettingService
		(Persistence.DatabaseContext databaseContext) :
		base(databaseContext: databaseContext)
	{
	}

	public async System.Threading.Tasks.Task
		<Domain.Features.Common.LocalizedMailSetting> GetInstanceAsync()
	{
		var currentUICultureLcid = Domain.Features
			.Common.CultureEnumHelper.GetCurrentUICultureLcid();

		var result =
			await
			DatabaseContext.LocalizedMailSettings
			.Where(current => current.Culture != null
				&& current.Culture.Lcid == currentUICultureLcid)
			.FirstOrDefaultAsync();

		if (result != null)
		{
			return result;
		}

		var currentCulture =
			await
			DatabaseContext.Cultures
			.Where(current => current.Lcid == currentUICultureLcid)
			.FirstOrDefaultAsync();

		if (currentCulture == null)
		{
			throw new System.Exception
				($"{nameof(currentCulture)} is null!");
		}

		result = new Domain.Features.Common
			.LocalizedMailSetting(cultureId: currentCulture.Id);

		await DatabaseContext.AddAsync(entity: result);

		await DatabaseContext.SaveChangesAsync();

		return result;
	}
}
