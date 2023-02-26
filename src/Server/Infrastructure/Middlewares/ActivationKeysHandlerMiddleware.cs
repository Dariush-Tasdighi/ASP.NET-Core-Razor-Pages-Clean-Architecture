using System.Linq;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Middlewares;

public class ActivationKeysHandlerMiddleware : object
{
	#region Statics
	private static string GetSha256(string value)
	{
		using var mySHA256 =
			System.Security.Cryptography.SHA256.Create();

		var stringBuilder =
			new System.Text.StringBuilder();

		try
		{
			var valueBytes =
				System.Text.Encoding.UTF8.GetBytes(s: value);

			// Compute the hash of the fileStream.
			byte[] hashBytes =
				mySHA256.ComputeHash(buffer: valueBytes);

			foreach (var theByte in hashBytes)
			{
				stringBuilder.Append
					(value: theByte.ToString(format: "x2"));
			}

			return stringBuilder.ToString();
		}
		catch
		{
			return string.Empty;
		}
	}

	private static string GetValidActivationKeyByDomain(string domain)
	{
		var result =
			GetSha256(value: domain);

		return result;
	}
	#endregion /Statics

	public ActivationKeysHandlerMiddleware
		(Microsoft.AspNetCore.Http.RequestDelegate next) : base()
	{
		Next = next;
	}

	private Microsoft.AspNetCore.Http.RequestDelegate Next { get; }

	public async System.Threading.Tasks.Task
		InvokeAsync(Microsoft.AspNetCore.Http.HttpContext httpContext,
		Infrastructure.Settings.ApplicationSettings applicationSettings)
	{
		var errorMessage =
			"No Activation Key!";

		if (applicationSettings is null ||
			applicationSettings.ActivationKeys is null ||
			applicationSettings.ActivationKeys.Length == 0)
		{
			// using Microsoft.AspNetCore.Http;
			await httpContext.Response
				.WriteAsync(text: errorMessage);

			return;
		}

		var domain =
			httpContext.Request.Host.Value;

		//domain = "dtat.ir";
		//domain = "temp.webdownloader.ir";

		var validActivationKey =
			GetValidActivationKeyByDomain(domain: domain);

		if (string.IsNullOrWhiteSpace(value: validActivationKey))
		{
			// using Microsoft.AspNetCore.Http;
			await httpContext.Response
				.WriteAsync(text: errorMessage);

			return;
		}

		var contains =
			applicationSettings.ActivationKeys
			.Where(current => current.ToLower() == validActivationKey.ToLower())
			.Any();

		if (contains == false)
		{
			// using Microsoft.AspNetCore.Http;
			await httpContext.Response
				.WriteAsync(text: errorMessage);

			return;
		}

		await Next(context: httpContext);
	}
}
