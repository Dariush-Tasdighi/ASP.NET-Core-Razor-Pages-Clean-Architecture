namespace Constants;

public static class CommonRouting : object
{
	static CommonRouting()
	{
	}

	/// <summary>
	/// Error 400
	/// </summary>
	public const string BadRequest = "/Error/Error400";

	/// <summary>
	/// Error 403
	/// </summary>
	public const string Forbidden = "/Error/Error403";

	/// <summary>
	/// Error 403
	/// </summary>
	public const string NotFound = "/Error/Error403";

	/// <summary>
	/// Error 500
	/// </summary>
	public const string InternalServerError = "/Error/Error500";

	/// <summary>
	/// Login
	/// </summary>
	public const string Login = "/Account/Login";

	/// <summary>
	/// Logout
	/// </summary>
	public const string Logout = "/Account/Logout";
}
