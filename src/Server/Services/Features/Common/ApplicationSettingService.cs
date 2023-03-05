using Microsoft.EntityFrameworkCore;

namespace Services.Features.Common;

public class ApplicationSettingService :
	Infrastructure.BaseServiceWithDatabaseContext
{
	public ApplicationSettingService
		(Persistence.DatabaseContext databaseContext) :
		base(databaseContext: databaseContext)
	{
	}

	public async System.Threading.Tasks.Task
		<Domain.Features.Common.ApplicationSetting> GetInstanceAsync()
	{
		var result =
			await
			DatabaseContext.ApplicationSettings
			.FirstOrDefaultAsync();

		if (result != null)
		{
			return result;
		}

		result =
			new Domain.Features.Common.ApplicationSetting();

		await DatabaseContext.AddAsync(entity: result);

		await DatabaseContext.SaveChangesAsync();

		return result;
	}
}
