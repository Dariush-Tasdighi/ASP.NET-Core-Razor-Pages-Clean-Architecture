namespace ViewModels.Pages.Account;

public class ChangeEmailAddressViewModel : object
{
	public ChangeEmailAddressViewModel() : base()
	{
	}

	public string? OldEmailAddress { get; set; }

	public string? NewEmailAddress { get; set; }
}
