namespace Server.Pages.Errors;

public class ErrorModel : Infrastructure.BasePageModel
{
	public ErrorModel() : base()
	{
	}

	public async System.Threading.Tasks.Task OnGetAsync()
	{
		await System.Threading.Tasks.Task.CompletedTask;
	}
}
