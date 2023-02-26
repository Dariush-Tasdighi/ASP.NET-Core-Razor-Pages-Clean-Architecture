namespace Constants;

/// <summary>
/// TODO: بعد از داینامیک کردن سیستم، باید حذف شود
/// </summary>
public static class Role : object
{
	static Role()
	{
	}

	public const string SimpleUser = nameof(SimpleUser);
	public const string SpecialUser = nameof(SpecialUser);

	public const string Supervisor = nameof(Supervisor);

	public const string Administrator = nameof(Administrator);
	public const string ApplicationOwner = nameof(ApplicationOwner);

	public const string Programmer = nameof(Programmer);
}
