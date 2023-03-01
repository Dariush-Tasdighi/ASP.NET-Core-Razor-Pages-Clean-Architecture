using Dtat;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication;

namespace Server.Pages.Account;

public class LoginModel :
	Infrastructure.BasePageModelWithDatabaseContext
{
	#region Constructor
	public LoginModel
		(Persistence.DatabaseContext databaseContext) :
		base(databaseContext: databaseContext)
	{
		ViewModel = new();
	}
	#endregion /Constructor

	#region Properties

	[Microsoft.AspNetCore.Mvc.BindProperty]
	public ViewModels.Pages.Account.LoginViewModel ViewModel { get; set; }

	#endregion /Properties

	#region Methods

	public async System.Threading.Tasks.Task
		<Microsoft.AspNetCore.Mvc.IActionResult> OnGetAsync(string? returnUrl)
	{
		// **************************************************
		var currentUICultureLcid = Domain.Features
			.Common.CultureEnumHelper.GetCurrentUICultureLcid();

		var currentCulture =
			await
			DatabaseContext.Cultures

			.Where(current => current.Lcid == currentUICultureLcid)

			.FirstOrDefaultAsync();

		if (currentCulture is null)
		{
			return RedirectToPage(pageName:
				Constants.CommonRouting.NotFound);
		}

		if (currentCulture.IsActive == false)
		{
			return RedirectToPage(pageName:
				Constants.CommonRouting.NotFound);
		}
		// **************************************************

		ViewModel.ReturnUrl = returnUrl;

		return Page();
	}

	public async System.Threading.Tasks.Task
		<Microsoft.AspNetCore.Mvc.IActionResult> OnPostAsync()
	{
		// **************************************************
		var currentUICultureLcid = Domain.Features
			.Common.CultureEnumHelper.GetCurrentUICultureLcid();

		var currentCulture =
			await
			DatabaseContext.Cultures

			.Where(current => current.Lcid == currentUICultureLcid)

			.FirstOrDefaultAsync();

		if (currentCulture is null)
		{
			return RedirectToPage(pageName:
				Constants.CommonRouting.NotFound);
		}

		if (currentCulture.IsActive == false)
		{
			return RedirectToPage(pageName:
				Constants.CommonRouting.NotFound);
		}
		// **************************************************

		if (ModelState.IsValid == false)
		{
			return Page();
		}

		// **************************************************
		var username =
			ViewModel.Username.Fix()!.ToLower();

		var foundedUser =
			await
			DatabaseContext.Users

			.Include(current => current.Role)
			.Include(current => current.Profiles)
			.ThenInclude(current => current.Culture)

			.Where(current => current.Username != null
				&& current.Username.ToLower() == username)

			.FirstOrDefaultAsync()
			;

		if (foundedUser is null)
		{
			var errorMessage = string.Format
				(format: Resources.Messages.Errors
				.UsernameAndOrPasswordIsNotCorrect);

			AddPageError(message: errorMessage);

			return Page();
		}
		// **************************************************

		// **************************************************
		// TODO
		// ApplicationSettings: MasterPassword
		// **************************************************

		// **************************************************
		var password =
			ViewModel.Password.Fix()!;

		var passwordHash =
			Dtat.Security.Hashing.GetSha256(text: password);

		if (string.Compare(strA: foundedUser.Password,
			strB: passwordHash, ignoreCase: false) != 0)
		{
			var errorMessage = string.Format
				(format: Resources.Messages.Errors
				.UsernameAndOrPasswordIsNotCorrect);

			AddPageError(message: errorMessage);

			return Page();
		}
		// **************************************************

		// **************************************************
		if (foundedUser.Role is null)
		{
			return RedirectToPage(pageName:
				Constants.CommonRouting.NotFound);
		}

		if (foundedUser.IsEmailAddressVerified == false)
		{
			var errorMessage =
				string.Format(format:
				Resources.Messages.Errors.EmailAddressIsNotVerified);

			AddPageError(message: errorMessage);

			return Page();
		}

		if (foundedUser.Role.IsActive == false)
		{
			var errorMessage =
				string.Format(format:
				Resources.Messages.Errors.YourRoleIsNotActive);

			AddPageError(message: errorMessage);

			return Page();
		}

		if (foundedUser.IsActive == false)
		{
			var errorMessage =
				string.Format(format:
				Resources.Messages.Errors.UserIsNotActive);

			AddPageError(message: errorMessage);

			return Page();
		}
		// **************************************************

		// **************************************************
		foundedUser.LastLoginDateTime = Dtat.DateTime.Now;

		await DatabaseContext.SaveChangesAsync();
		// **************************************************

		// **************************************************
		// **************************************************
		// **************************************************
		var claims =
			new System.Collections.Generic
			.List<System.Security.Claims.Claim>();

		System.Security.Claims.Claim claim;

		var userProfile =
			foundedUser.Profiles

			.Where(current => current.Culture != null
				&& current.Culture.Lcid == currentUICultureLcid)

			.FirstOrDefault();

		if (userProfile is not null)
		{
			// **************************************************
			claim =
				new System.Security.Claims.Claim
				(type: "FirstName", value: userProfile.FirstName);

			claims.Add(item: claim);
			// **************************************************

			// **************************************************
			claim =
				new System.Security.Claims.Claim
				(type: "LastName", value: userProfile.LastName);

			claims.Add(item: claim);
			// **************************************************
		}

		// **************************************************
		//claim =
		//	new System.Security.Claims.Claim
		//	(type: "Role", value: foundedUser.Role.Name);

		claim =
			new System.Security.Claims.Claim
			(type: System.Security.Claims.ClaimTypes.Role, value: foundedUser.Role.Name);

		claims.Add(item: claim);
		// **************************************************

		// **************************************************
		// نباید از دستور ذیل استفاده کنیم
		//claim =
		//	new System.Security.Claims.Claim
		//	(type: System.Security.Claims.ClaimTypes.Name, value: foundedUser.Username);

		claim =
			new System.Security.Claims.Claim
			(type: System.Security.Claims.ClaimTypes.Name, value: foundedUser.EmailAddress);

		claims.Add(item: claim);
		// **************************************************

		// **************************************************
		claim =
			new System.Security.Claims.Claim
			(type: System.Security.Claims.ClaimTypes.Email, value: foundedUser.EmailAddress);

		claims.Add(item: claim);
		// **************************************************

		// **************************************************
		if (string.IsNullOrWhiteSpace(value: foundedUser.Username) == false)
		{
			claim =
				new System.Security.Claims.Claim
				(type: System.Security.Claims.ClaimTypes.NameIdentifier, value: foundedUser.Username);

			claims.Add(item: claim);
		}
		// **************************************************

		// **************************************************
		if (string.IsNullOrWhiteSpace(value: foundedUser.CellPhoneNumber) == false)
		{
			claim =
				new System.Security.Claims.Claim
				(type: System.Security.Claims.ClaimTypes.MobilePhone, value: foundedUser.CellPhoneNumber);

			claims.Add(item: claim);
		}
		// **************************************************
		// **************************************************
		// **************************************************

		// **************************************************
		// **************************************************
		// **************************************************
		// دستور ذیل غلط است و کار نمی‌کند
		//var claimsIdentity =
		//	new System.Security.Claims.ClaimsIdentity(claims: claims);

		var claimsIdentity =
			new System.Security.Claims.ClaimsIdentity(claims: claims,
			authenticationType: Infrastructure.Security.Constants.DefaultScheme);
		// **************************************************
		// **************************************************
		// **************************************************

		// **************************************************
		// **************************************************
		// **************************************************
		var claimsPrincipal =
			new System.Security.Claims.ClaimsPrincipal(identity: claimsIdentity);
		// **************************************************

		// **************************************************
		//var claimsPrincipal =
		//	new System.Security.Claims.ClaimsPrincipal();

		//claimsPrincipal.AddIdentity(identity: claimsIdentity);
		// **************************************************

		// **************************************************
		//var claimsPrincipal =
		//	new System.Security.Claims.ClaimsPrincipal(identities: new[] { claimsIdentity });
		// **************************************************
		// **************************************************
		// **************************************************

		// **************************************************
		// **************************************************
		// **************************************************
		var authenticationProperties =
			new Microsoft.AspNetCore.Authentication.AuthenticationProperties
			{
				IsPersistent = ViewModel.RememberMe,
			};
		// **************************************************
		// **************************************************
		// **************************************************

		// **************************************************
		// **************************************************
		// **************************************************
		// SignInAsync -> using Microsoft.AspNetCore.Authentication;
		await HttpContext.SignInAsync
			(scheme: Infrastructure.Security.Constants.DefaultScheme,
			principal: claimsPrincipal, properties: authenticationProperties);
		// **************************************************
		// **************************************************
		// **************************************************

		if (string.IsNullOrWhiteSpace(value: ViewModel.ReturnUrl))
		{
			return RedirectToPage(pageName:
				Constants.CommonRouting.RootIndex);
		}
		else
		{
			return Redirect(url: ViewModel.ReturnUrl);
		}
	}

	#endregion /Methods
}
