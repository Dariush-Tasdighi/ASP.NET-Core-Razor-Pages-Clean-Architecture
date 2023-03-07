namespace ViewModels.Pages.Features.Identity;

public class UserItemViewModel : object
{
	#region Constructor
	public UserItemViewModel()
	{
	}
	#endregion /Constructor

	#region Properties

	public int Score { get; set; }

	public string? RoleTitle { get; set; }
	public string? GenderPrefix { get; set; }

	public string? ImageUrl { get; set; }
	public string? LastName { get; set; }
	public string? Username { get; set; }
	public string? FirstName { get; set; }
	public string? EmailAddress { get; set; }
	public string? CellPhoneNumber { get; set; }

	public string? DisplayName
	{
		get
		{
			string? result = null;

			if (string.IsNullOrWhiteSpace(GenderPrefix) == false)
			{
				result = GenderPrefix;
			}

			if (string.IsNullOrWhiteSpace(FirstName) == false)
			{
				if (string.IsNullOrWhiteSpace(result) == false)
				{
					result += " ";
				}

				result += FirstName;
			}

			if (string.IsNullOrWhiteSpace(LastName) == false)
			{
				if (string.IsNullOrWhiteSpace(result) == false)
				{
					result += " ";
				}

				result += LastName;
			}

			return result;
		}
	}

	#endregion /Properties
}
