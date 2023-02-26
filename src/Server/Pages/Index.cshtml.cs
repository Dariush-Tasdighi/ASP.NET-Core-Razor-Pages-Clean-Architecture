namespace Server.Pages;

public class IndexModel :
	Infrastructure.BasePageModel
{
	public IndexModel() : base()
	{
	}

	public async System.Threading.Tasks.Task OnGetAsync()
	{
		await System.Threading.Tasks.Task.CompletedTask;
    }
}
