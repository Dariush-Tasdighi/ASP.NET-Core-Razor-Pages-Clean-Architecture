namespace Server.Pages.Errors;

public class Error403Model : Infrastructure.BasePageModel
{
	public Error403Model() : base()
	{
	}

	public async System.Threading.Tasks.Task OnGetAsync()
	{
		await System.Threading.Tasks.Task.CompletedTask;
	}
}
