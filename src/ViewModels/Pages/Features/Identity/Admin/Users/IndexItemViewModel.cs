namespace ViewModels.Pages.Features.Identity.Admin.Users;

public class IndexItemViewModel : object
{
	#region Constructor
	public IndexItemViewModel() : base()
	{
		EmailAddress = string.Empty;
	}
	#endregion /Constructor

	#region Properties

	public System.Guid Id { get; set; }



	public int? Hits { get; set; }
	public int Ordering { get; set; }



	public System.Guid? RoleId { get; set; }
	public string? RoleTitle { get; set; }



	public System.Guid? GenderId { get; set; }
	public string? GenderPrefix { get; set; }



	public bool IsActive { get; set; }
	public bool IsDeleted { get; set; }
	public bool IsVerified { get; set; }
	public bool IsUndeletable { get; set; }
	public bool IsEmailAddressVerified { get; set; }
	public bool IsNationalCodeVerified { get; set; }
	public bool IsVisibleInContactUsPage { get; set; }
	public bool IsCellPhoneNumberVerified { get; set; }



	public string? LastName { get; set; }
	public string? FirstName { get; set; }
	public string? Username { get; set; }
	public string EmailAddress { get; set; }
	public string? NationalCode { get; set; }
	public string? CellPhoneNumber { get; set; }



	public System.DateTimeOffset InsertDateTime { get; set; }
	public System.DateTimeOffset UpdateDateTime { get; set; }
	public System.DateTimeOffset? DeleteDateTime { get; set; }
	public System.DateTimeOffset? LastLoginDateTime { get; set; }

	#endregion /Properties

	#region Read Only Properties

	public int DisplayHits
	{
		get
		{
			var result = 0;

			if (Hits.HasValue)
			{
				result = Hits.Value;
			}

			return result;
		}
	}

	public string? FullName
	{
		get
		{
			string? result = null;

			if (string.IsNullOrWhiteSpace(value: GenderPrefix) == false)
			{
				result = GenderPrefix;
			}

			if (string.IsNullOrWhiteSpace(value: FirstName) == false)
			{
				if (string.IsNullOrWhiteSpace(value: result) == false)
				{
					result += " ";
				}

				result += FirstName;
			}

			if (string.IsNullOrWhiteSpace(value: LastName) == false)
			{
				if (string.IsNullOrWhiteSpace(value: result) == false)
				{
					result += " ";
				}

				result += LastName;
			}

			return result;
		}
	}

	#endregion /Read Only Properties
}
