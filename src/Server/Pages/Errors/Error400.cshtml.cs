namespace Server.Pages.Errors;

public class Error400Model : Infrastructure.BasePageModel
{
	public Error400Model() : base()
	{
	}

	public async System.Threading.Tasks.Task OnGetAsync()
	{
		await System.Threading.Tasks.Task.CompletedTask;
	}
}
