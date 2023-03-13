using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public static class SelectLists : object
{
	static SelectLists()
	{
	}

	#region GetRolesAsync()
	public static async System.Threading.Tasks.Task
		<Microsoft.AspNetCore.Mvc.Rendering.SelectList> GetRolesAsync
		(Persistence.DatabaseContext databaseContext, object? selectedValue = null)
	{
		var currentUICultureLcid = Domain.Features
			.Common.CultureEnumHelper.GetCurrentUICultureLcid();

		var list =
			await
			databaseContext.Roles

			.OrderBy(current => current.Ordering)
			.ThenBy(current => current.Code)

			.Select(current => new ViewModels.Shared.KeyValueViewModel
			{
				Id = current.Id,

#pragma warning disable CS8602

				Name = current.LocalizedRoles.FirstOrDefault
					(other => other.Culture != null &&
					other.Culture.Lcid == currentUICultureLcid).Title,

#pragma warning restore CS8602
			})
			.ToListAsync()
			;

		// **************************************************
		var emptyItem =
			new ViewModels.Shared.KeyValueViewModel
			(id: null, name: Resources.DataDictionary.SelectAnItem);

		list.Insert(index: 0, item: emptyItem);
		// **************************************************

		var result =
			new Microsoft.AspNetCore.Mvc.Rendering
			.SelectList(items: list,
			dataValueField: nameof(ViewModels.Shared.KeyValueViewModel.Id),
			dataTextField: nameof(ViewModels.Shared.KeyValueViewModel.Name),
			selectedValue: selectedValue);

		return result;
	}
	#endregion /GetRolesAsync()

	#region GetLayoutsAsync()
	public static async System.Threading.Tasks.Task
		<Microsoft.AspNetCore.Mvc.Rendering.SelectList> GetLayoutsAsync
		(Persistence.DatabaseContext databaseContext, object? selectedValue = null)
	{
		var list =
			await
			databaseContext.Layouts
			.OrderBy(current => current.Ordering)
			.ThenBy(current => current.Name)
			.Select(current => new ViewModels.Shared.KeyValueViewModel
			{
				Id = current.Id,
				Name = current.DisplayName,
			})
			.ToListAsync()
			;

		// **************************************************
		var emptyItem =
			new ViewModels.Shared.KeyValueViewModel
			(id: null, name: Resources.DataDictionary.SelectAnItem);

		list.Insert(index: 0, item: emptyItem);
		// **************************************************

		var result =
			new Microsoft.AspNetCore.Mvc.Rendering
			.SelectList(items: list,
			dataValueField: nameof(ViewModels.Shared.KeyValueViewModel.Id),
			dataTextField: nameof(ViewModels.Shared.KeyValueViewModel.Name),
			selectedValue: selectedValue);

		return result;
	}
	#endregion /GetLayoutsAsync()

	#region GetGendersForUserAsync()
	public static async System.Threading.Tasks.Task
		<Microsoft.AspNetCore.Mvc.Rendering.SelectList> GetGendersForUserAsync
		(Persistence.DatabaseContext databaseContext, object? selectedValue = null)
	{
		var currentUICultureLcid = Domain.Features
			.Common.CultureEnumHelper.GetCurrentUICultureLcid();

		var list =
			await
			databaseContext.Genders

			.Where(current => current.IsActive)

			.OrderBy(current => current.Ordering)

			.Select(current => new ViewModels.Shared.KeyValueViewModel
			{
				Id = current.Id,

#pragma warning disable CS8602

				Name = current.LocalizedGenders.FirstOrDefault
				(other => other.Culture != null && other.Culture.Lcid == currentUICultureLcid).Title,

#pragma warning restore CS8602

			})
			.ToListAsync()
			;

		// **************************************************
		var emptyItem =
			new ViewModels.Shared.KeyValueViewModel
			(id: null, name: Resources.DataDictionary.SelectAnItem);

		list.Insert(index: 0, item: emptyItem);
		// **************************************************

		var result =
			new Microsoft.AspNetCore.Mvc.Rendering
			.SelectList(items: list,
			dataValueField: nameof(ViewModels.Shared.KeyValueViewModel.Id),
			dataTextField: nameof(ViewModels.Shared.KeyValueViewModel.Name),
			selectedValue: selectedValue);

		return result;
	}
	#endregion /GetGendersForUserAsync()

	#region GetGendersForAdminAsync()
	public static async System.Threading.Tasks.Task
		<Microsoft.AspNetCore.Mvc.Rendering.SelectList> GetGendersForAdminAsync
		(Persistence.DatabaseContext databaseContext, object? selectedValue = null)
	{
		var currentUICultureLcid = Domain.Features
			.Common.CultureEnumHelper.GetCurrentUICultureLcid();

		var list =
			await
			databaseContext.Genders

			.OrderBy(current => current.Ordering)
			.ThenBy(current => current.Code)

			.Select(current => new ViewModels.Shared.KeyValueViewModel
			{
				Id = current.Id,

#pragma warning disable CS8602

				Name = current.LocalizedGenders.FirstOrDefault
					(other => other.Culture != null &&
					other.Culture.Lcid == currentUICultureLcid).Title,

#pragma warning restore CS8602

			})
			.ToListAsync()
			;

		// **************************************************
		var emptyItem =
			new ViewModels.Shared.KeyValueViewModel
			(id: null, name: Resources.DataDictionary.SelectAnItem);

		list.Insert(index: 0, item: emptyItem);
		// **************************************************

		var result =
			new Microsoft.AspNetCore.Mvc.Rendering
			.SelectList(items: list,
			dataValueField: nameof(ViewModels.Shared.KeyValueViewModel.Id),
			dataTextField: nameof(ViewModels.Shared.KeyValueViewModel.Name),
			selectedValue: selectedValue);

		return result;
	}
	#endregion /GetGendersForAdminAsync()

	#region GetPostTypesAsync()
	public static async System.Threading.Tasks.Task
		<Microsoft.AspNetCore.Mvc.Rendering.SelectList> GetPostTypesAsync
		(Persistence.DatabaseContext databaseContext, object? selectedValue = null)
	{
		var currentUICultureLcid = Domain.Features
			.Common.CultureEnumHelper.GetCurrentUICultureLcid();

		var list =
			await
			databaseContext.PostTypes

			.Where(current => current.Culture != null &&
				current.Culture.Lcid == currentUICultureLcid)

			.OrderBy(current => current.Ordering)
			.ThenBy(current => current.Name)

			.Select(current => new ViewModels.Shared.KeyValueViewModel
			{
				Id = current.Id,
				Name = current.DisplayName,
			})
			.ToListAsync()
			;

		// **************************************************
		var emptyItem =
			new ViewModels.Shared.KeyValueViewModel
			(id: null, name: Resources.DataDictionary.SelectAnItem);

		list.Insert(index: 0, item: emptyItem);
		// **************************************************

		var result =
			new Microsoft.AspNetCore.Mvc.Rendering
			.SelectList(items: list,
			dataValueField: nameof(ViewModels.Shared.KeyValueViewModel.Id),
			dataTextField: nameof(ViewModels.Shared.KeyValueViewModel.Name),
			selectedValue: selectedValue);

		return result;
	}
	#endregion /GetPostTypesAsync()

	#region GetPostCategoriesAsync()
	public static async System.Threading.Tasks.Task
		<Microsoft.AspNetCore.Mvc.Rendering.SelectList> GetPostCategoriesAsync
		(Persistence.DatabaseContext databaseContext, object? selectedValue = null)
	{
		var currentUICultureLcid = Domain.Features
			.Common.CultureEnumHelper.GetCurrentUICultureLcid();

		var list =
			await
			databaseContext.PostCategories

			.Where(current => current.Culture != null &&
				current.Culture.Lcid == currentUICultureLcid)

			.OrderBy(current => current.Ordering)
			.ThenBy(current => current.Name)

			.Select(current => new ViewModels.Shared.KeyValueViewModel
			{
				Id = current.Id,
				Name = current.DisplayName,
			})
			.ToListAsync()
			;

		// **************************************************
		var emptyItem =
			new ViewModels.Shared.KeyValueViewModel
			(id: null, name: Resources.DataDictionary.SelectAnItem);

		list.Insert(index: 0, item: emptyItem);
		// **************************************************

		var result =
			new Microsoft.AspNetCore.Mvc.Rendering
			.SelectList(items: list,
			dataValueField: nameof(ViewModels.Shared.KeyValueViewModel.Id),
			dataTextField: nameof(ViewModels.Shared.KeyValueViewModel.Name),
			selectedValue: selectedValue);

		return result;
	}
	#endregion /GetPostCategoriesAsync()
}
