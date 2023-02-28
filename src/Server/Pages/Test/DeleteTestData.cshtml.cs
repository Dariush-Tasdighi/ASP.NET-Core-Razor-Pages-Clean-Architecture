using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Server.Pages.Test;

public class DeleteTestDataModel :
	Infrastructure.BasePageModelWithDatabaseContext
{
	public DeleteTestDataModel
		(Persistence.DatabaseContext databaseContext) :
		base(databaseContext: databaseContext)
	{
	}

	public async System.Threading.Tasks.Task OnGet()
	{
		await DeletePostsAsync();
		await DeletePostCategoriesAsync();
	}

	private async System.Threading.Tasks.Task DeletePostsAsync()
	{
		// **************************************************
		// *** Old Solution *********************************
		// **************************************************
		//var items =
		//	await
		//	DatabaseContext.Posts
		//	.Where(current => current.IsTestData)
		//	.ToListAsync();

		//DatabaseContext.RemoveRange(entities: items);

		//await DatabaseContext.SaveChangesAsync();
		// **************************************************

		// **************************************************
		// *** New Solution (in EF 7.x) *********************
		// **************************************************
		await
		DatabaseContext.Posts
		.Where(current => current.IsTestData)
		.ExecuteDeleteAsync();
		// **************************************************
	}

	private async System.Threading.Tasks.Task DeletePostCategoriesAsync()
	{
		await
		DatabaseContext.PostCategories
		.Where(current => current.IsTestData)
		.ExecuteDeleteAsync();
	}
}
