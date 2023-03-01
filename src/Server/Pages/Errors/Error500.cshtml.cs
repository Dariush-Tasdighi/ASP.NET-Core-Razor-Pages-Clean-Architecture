namespace Server.Pages.Errors;

public class Error500Model : Infrastructure.BasePageModel
{
	public Error500Model() : base()
	{
	}

	public async System.Threading.Tasks.Task OnGet()
	{
		await System.Threading.Tasks.Task.CompletedTask;
	}
}
