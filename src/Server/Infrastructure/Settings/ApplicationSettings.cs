namespace Infrastructure.Settings;

public class ApplicationSettings : object
{
	public static readonly string KeyName = nameof(ApplicationSettings);

	public ApplicationSettings() : base()
	{
		ToastSettings =
			new ToastSettings();

		FileManagerSettings =
			new FileManagerSettings();

		TablesDefaultSettings =
			new TablesDefaultSettings();
	}

	public string[]? ActivationKeys { get; set; }

	public ToastSettings ToastSettings { get; set; }
	public FileManagerSettings FileManagerSettings { get; set; }
	public TablesDefaultSettings TablesDefaultSettings { get; set; }
}
