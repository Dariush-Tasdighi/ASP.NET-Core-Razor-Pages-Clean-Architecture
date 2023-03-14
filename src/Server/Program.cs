// **************************************************
using Infrastructure.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Security.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;
// **************************************************

// **************************************************
var webApplicationOptions =
	new Microsoft.AspNetCore.Builder.WebApplicationOptions
	{
		EnvironmentName =
			System.Diagnostics.Debugger.IsAttached ?
			Microsoft.Extensions.Hosting.Environments.Development
			:
			Microsoft.Extensions.Hosting.Environments.Production,
	};

var builder =
	Microsoft.AspNetCore.Builder
	.WebApplication.CreateBuilder(options: webApplicationOptions);
// **************************************************

// **************************************************
// using Microsoft.Extensions.DependencyInjection;
builder.Services.AddHttpContextAccessor();
// **************************************************

// **************************************************
// حل مشکل نمایش متن فارسی در صفحات
// **************************************************
//builder.Services.AddSingleton
//	(implementationInstance: System.Text.Encodings.Web.HtmlEncoder.Create
//	(allowedRanges: new[]
//	{
//		System.Text.Unicode.UnicodeRanges.Arabic,
//		System.Text.Unicode.UnicodeRanges.BasicLatin,
//	}));
// **************************************************

// **************************************************
builder.Services.AddRouteAnalyzer();
// **************************************************

// **************************************************
builder.Services.AddRouting(options =>
{
	options.LowercaseUrls = true;
	options.LowercaseQueryStrings = true;

	//options.AppendTrailingSlash
	//options.SuppressCheckForUnhandledSecurityMetadata = false;
});
// **************************************************

// **************************************************
// using Microsoft.Extensions.DependencyInjection;
builder.Services.AddRazorPages();
// **************************************************

// **************************************************
// using Microsoft.Extensions.DependencyInjection;
builder.Services.Configure<Infrastructure.Settings.ApplicationSettings>
	(builder.Configuration.GetSection(key: Infrastructure.Settings.ApplicationSettings.KeyName))
	// using Microsoft.Extensions.DependencyInjection;
	.AddSingleton
	(implementationFactory: serviceType =>
	{
		var result =
			// using Microsoft.Extensions.DependencyInjection;
			serviceType.GetRequiredService
			<Microsoft.Extensions.Options.IOptions
			<Infrastructure.Settings.ApplicationSettings>>().Value;

		return result;
	});
// **************************************************

// **************************************************
// **************************************************
// **************************************************
builder.Services
	.AddAuthentication(defaultScheme: Infrastructure.Security.Constants.DefaultScheme)
	.AddCookie(authenticationScheme: Infrastructure.Security.Constants.DefaultScheme);
// **************************************************

// **************************************************
//builder.Services
//	.AddAuthentication(defaultScheme: Infrastructure.Security.Utility.AuthenticationScheme)
//	.AddCookie(authenticationScheme: Infrastructure.Security.Utility.AuthenticationScheme)
//	.AddGoogle(authenticationScheme: Microsoft.AspNetCore.Authentication.Google.GoogleDefaults.AuthenticationScheme,
//	configureOptions: options =>
//	{
//		options.ClientId =
//			builder.Configuration["ApplicationSettings:Authentication:Google:ClientId"];

//		options.ClientSecret =
//			builder.Configuration["ApplicationSettings:Authentication:Google:ClientSecret"];

//		// using Microsoft.AspNetCore.Authentication;
//		options.ClaimActions.MapJsonKey
//			(claimType: "urn:google:picture", jsonKey: "picture", valueType: "url");
//	})
//	;
// **************************************************
// **************************************************
// **************************************************

// **************************************************
// *** Use Sql Server *******************************
// **************************************************
//// using Microsoft.Extensions.Configuration;
//var connectionString =
//	builder.Configuration
//	.GetConnectionString(name: "ConnectionString");

//// using Microsoft.Extensions.DependencyInjection;
//builder.Services.AddDbContext<Persistence.DatabaseContext>
//	(optionsAction: options =>
//	{
//		// using Microsoft.EntityFrameworkCore;
//		options
//			.UseLazyLoadingProxies();

//		// using Microsoft.EntityFrameworkCore;
//		options
//			.UseSqlServer(connectionString: connectionString);
//	});
// **************************************************
// *** /Use Sql Server ******************************
// **************************************************

// **************************************************
// *** Use Sqlite ***********************************
// **************************************************
builder.Services.AddDbContext<Persistence.DatabaseContext>
	(optionsAction: options =>
	{
		options
			.UseLazyLoadingProxies();

		options
			.UseSqlite(connectionString: "Data Source=Database\\MySQLite.db");
	});
// **************************************************
// *** /Use Sqlite **********************************
// **************************************************

// **************************************************
builder.Services.AddScoped
	<Services.Features.Identity.UserService>();

builder.Services.AddScoped
	<Services.Features.Common.ApplicationSettingService>();

builder.Services.AddScoped
	<Services.Features.Identity.AuthenticatedUserService>();

builder.Services.AddScoped
	<Services.Features.Common.LocalizedMailSettingService>();

builder.Services.AddScoped
	<Services.Features.Common.LocalizedApplicationSettingService>();
// **************************************************

// **************************************************
builder.Services.AddSingleton<Services.ColorService>();
// **************************************************

// **************************************************
var app =
	builder.Build();
// **************************************************

// using Microsoft.Extensions.Hosting;
if (app.Environment.IsDevelopment())
{
	// **************************************************
	// using Microsoft.AspNetCore.Builder;
	app.UseDeveloperExceptionPage();
	// **************************************************
}
else
{
	// **************************************************
	// using Infrastructure.Middlewares;
	app.UseGlobalException();
	// **************************************************

	// **************************************************
	// using Microsoft.AspNetCore.Builder;
	app.UseExceptionHandler("/Errors/Error");
	// **************************************************

	// **************************************************
	// The default HSTS value is 30 days.
	// You may want to change this for production scenarios,
	// see https://aka.ms/aspnetcore-hsts
	//
	// using Microsoft.AspNetCore.Builder; 
	app.UseHsts();
	// **************************************************
}

// **************************************************
// using Microsoft.AspNetCore.Builder;
app.UseHttpsRedirection();
// **************************************************

// **************************************************
// using Microsoft.AspNetCore.Builder;
app.UseStaticFiles();
// **************************************************

// **************************************************
// using Infrastructure.Middlewares;
app.UseActivationKeys();
// **************************************************

// **************************************************
// using Microsoft.AspNetCore.Builder;
app.UseRouting();
// **************************************************

// **************************************************
// using Microsoft.AspNetCore.Builder;
app.UseAuthentication();
// **************************************************

// **************************************************
// using Microsoft.AspNetCore.Builder;
app.UseAuthorization();
// **************************************************

// **************************************************
// using Infrastructure.Middlewares;
app.UseCultureCookie();
// **************************************************

// **************************************************
// using Microsoft.AspNetCore.Builder;
app.MapRazorPages();
// **************************************************

// **************************************************
app.Run();
// **************************************************
