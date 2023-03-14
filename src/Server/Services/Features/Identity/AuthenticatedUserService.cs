using System.Linq;

namespace Services.Features.Identity;

public class AuthenticatedUserService : object
{
	public const string SessionIdKeyName = "SessionId";

	public AuthenticatedUserService
		(Microsoft.AspNetCore.Http.IHttpContextAccessor httpContextAccessor)
	{
		HttpContextAccessor = httpContextAccessor;

		HttpContext =
			HttpContextAccessor.HttpContext;

		User = HttpContext?.User;

		Identity = User?.Identity;
	}

	private System.Security.Claims.ClaimsPrincipal? User { get; }

	private System.Security.Principal.IIdentity? Identity { get; }

	private Microsoft.AspNetCore.Http.HttpContext? HttpContext { get; }

	private Microsoft.AspNetCore.Http.IHttpContextAccessor HttpContextAccessor { get; }

	public bool IsAuthenticated
	{
		get
		{
			if (Identity is null)
			{
				return false;
			}

			return Identity.IsAuthenticated;

			//if (User is null)
			//{
			//	return false;
			//}

			//if (User.Identity is null)
			//{
			//	return false;
			//}

			//return User.Identity.IsAuthenticated;
		}
	}

	public System.Guid? SessionId
	{
		get
		{
			var sessionId =
				GetClaimValue(keyName: SessionIdKeyName);

			if (sessionId is null)
			{
				return null;
			}

			try
			{
				var result =
					new System.Guid(g: sessionId);

				return result;
			}
			catch
			{
				return null;
			}
		}
	}

	private string? GetClaimValue(string? keyName)
	{
		if (User is null)
		{
			return null;
		}

		if (string.IsNullOrWhiteSpace(value: keyName))
		{
			return null;
		}

		var claim =
			User.Claims
			.Where(current => current.Type.ToLower() == keyName.ToLower())
			.FirstOrDefault();

		if (claim is null)
		{
			return null;
		}

		var value =
			claim.Value;

		return value;
	}
}
