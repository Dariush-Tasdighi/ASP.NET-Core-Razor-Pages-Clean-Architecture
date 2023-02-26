namespace Server.Pages.Errors;

public class Error404Model : Infrastructure.BasePageModel
{
	public Error404Model() : base()
	{
	}

	//public void OnGet()
	//{
	//}

	public async System.Threading.Tasks.Task OnGetAsync()
	{
		await System.Threading.Tasks.Task.CompletedTask;
	}
}
